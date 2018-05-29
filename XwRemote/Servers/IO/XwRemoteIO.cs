using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using FluentFTP;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Core.Util;
using Microsoft.WindowsAzure.Storage.File;
using Renci.SshNet;
using Renci.SshNet.Async;
using XwMaxLib.Extensions;

namespace XwRemote.Servers.IO
{
    public class XwRemoteIO : IDisposable
    {
        //**********************************************************************************************
        enum Engine
        {
            FTP = 1,
            SFTP = 2,
            S3 = 3,
            AZUREFILE = 4
        }

        public enum XwFileAction : int
        {
            Ask,
            Overwrite,
            Resume,
            Skip
        }

        public struct XwRemoteIOItem
        {
            public bool IsDirectory;
            public bool IsSymlink;
            public string Name;
            public string FullName;
            public long Size;
            public DateTime Modified;
        }

        public class XwRemoteIOResult
        {
            public bool Success = false;
            public string Message = "";
            public Exception Exception = null;
            public List<XwRemoteIOItem> Items = null;
            public long Size;
            public DateTime Modified;
        }

        //**********************************************************************************************
        private string Hostname = "";
        private int Port = 0;
        private string Username = "";
        private string Password = "";
        private Engine engine;
        private FtpClient ftp = null;
        private SftpClient sftp = null;
        private AmazonS3Client s3 = null;
        private CloudStorageAccount azureAccount = null;
        private CloudFileClient azure = null;
        private bool ThrowExceptions = false;

        //**********************************************************************************************
        public bool IsConnected
        {
            get
            {
                switch (engine)
                {
                    case Engine.FTP:
                        {
                            if (ftp == null)
                                return false;

                            return ftp.IsConnected;
                        }
                    case Engine.SFTP:
                        {
                            if (sftp == null)
                                return false;

                            return sftp.IsConnected;
                        }
                    case Engine.S3:
                        {
                            if (s3 == null)
                                return false;
                            return true;
                        }
                    case Engine.AZUREFILE:
                        {
                            if (azure == null)
                                return false;
                            return true;
                        }
                    default:
                        throw new Exception("IsConnected not implemented for this engine");
                }
            }
        }
        
        //**********************************************************************************************
        public async Task<XwRemoteIOResult> ConnectToFTP(string Hostname, int Port, string Username, string Password)
        {
            XwRemoteIOResult result = new XwRemoteIOResult();
            try
            {
                engine = Engine.FTP;
                this.Hostname = Hostname;
                this.Port = Port;
                this.Username = Username;
                this.Password = Password;

                ftp = new FtpClient(Hostname, Port, Username, Password);
                await ftp.ConnectAsync();
                result.Success = true;
                result.Message = $"OK   : Connect: {Hostname}:{Port}";
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = $"ERROR: Connect {Hostname}:{Port} => {ex.Message}";
                if (ThrowExceptions)
                    throw;
            }
            return result;
        }

        //**********************************************************************************************
        public async Task<XwRemoteIOResult> ConnectToSFTP(string Hostname, int Port, string Username, string Password)
        {
            XwRemoteIOResult result = new XwRemoteIOResult();
            try
            {
                engine = Engine.SFTP;
                this.Hostname = Hostname;
                this.Port = Port;
                this.Username = Username;
                this.Password = Password;

                await Task.Run(() =>
                {
                    sftp = new SftpClient(Hostname, Port, Username, Password);
                    sftp.Connect();
                });

                result.Success = true;
                result.Message = $"OK   : Connect: {Hostname}:{Port}";
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = $"ERROR: Connect {Hostname}:{Port} => {ex.Message}";
                if (ThrowExceptions)
                    throw;
            }
            return result;
        }

        //**********************************************************************************************
        public async Task<XwRemoteIOResult> ConnectToAWSS3(string Bucket, string AccessKey, string SecretKey)
        {
            XwRemoteIOResult result = new XwRemoteIOResult();
            try
            {
                engine = Engine.S3;
                Hostname = Bucket;
                Username = AccessKey;
                Password = SecretKey;

                await Task.Run(() =>
                {
                    //Get correct Bucket region
                    string region = "";
                    using (var client = new AmazonS3Client(AccessKey, SecretKey, RegionEndpoint.EUWest1))
                    {
                        var response = client.GetBucketLocation(Hostname);
                        region = response.Location.Value;
                    }

                    //Connect to correct region
                    var config = new AmazonS3Config
                    {
                        RegionEndpoint = RegionEndpoint.GetBySystemName(region)
                    };
                    s3 = new AmazonS3Client(AccessKey, SecretKey, config);
                });

                result.Success = true;
                result.Message = $"OK   : Connect: {Hostname}";
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = $"ERROR: Connect {Hostname} => {ex.Message}";
                if (ThrowExceptions)
                    throw;
            }
            return result;
        }

        //**********************************************************************************************
        public async Task<XwRemoteIOResult> ConnectToAZUREFILE(string AccountName, string AccountKey)
        {
            XwRemoteIOResult result = new XwRemoteIOResult();
            try
            {
                engine = Engine.AZUREFILE;
                Username = AccountName;
                Password = AccountKey;

                await Task.Run(() =>
                {
                    string connectionString = $"DefaultEndpointsProtocol=https;AccountName={Username};AccountKey={Password}";
                    azureAccount = CloudStorageAccount.Parse(connectionString);
                    azure = azureAccount.CreateCloudFileClient();
                });

                result.Success = true;
                result.Message = $"OK   : Connect: {Hostname}";
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = $"ERROR: Connect {Hostname} => {ex.Message}";
                if (ThrowExceptions)
                    throw;
            }
            return result;
        }

        //**********************************************************************************************
        public async Task<XwRemoteIOResult> Reconnect()
        {
            XwRemoteIOResult result = new XwRemoteIOResult();
            try
            {
                switch (engine)
                {
                    case Engine.FTP:
                        return await ConnectToFTP(Hostname, Port, Username, Password);
                    case Engine.SFTP:
                        return await ConnectToSFTP(Hostname, Port, Username, Password);
                    case Engine.S3:
                        return await ConnectToAWSS3(Hostname, Username, Password);
                    case Engine.AZUREFILE:
                        return await ConnectToAZUREFILE(Username, Password);
                    default:
                        throw new Exception("Reconnect not implemented for this engine");
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = $"ERROR: Reconnect {Hostname}:{Port} => {ex.Message}";
                if (ThrowExceptions)
                    throw;
            }
            return result;
        }
        
        //**********************************************************************************************
        public async Task<XwRemoteIOResult> ListDirectory(string path)
        {
            //do some uniformization
            path = path.Replace("\\", "/");
            
            XwRemoteIOResult result = new XwRemoteIOResult();
            try
            {
                switch (engine)
                {
                    case Engine.FTP:
                        {
                            var items = await ftp.GetListingAsync(path, FtpListOption.AllFiles | FtpListOption.ForceList);
                            result.Items = new List<XwRemoteIOItem>(items.Length);
                            foreach (var item in items)
                            {
                                XwRemoteIOItem i = new XwRemoteIOItem();
                                i.IsDirectory = (item.Type == FtpFileSystemObjectType.Directory);
                                i.IsSymlink = (item.Type == FtpFileSystemObjectType.Link);
                                i.Name = item.Name;
                                i.FullName = item.FullName;
                                i.Size = item.Size;
                                i.Modified = item.Modified;

                                //OH this is so bad...
                                if (i.IsSymlink)
                                {
                                    i.IsDirectory = ftp.DirectoryExists(item.FullName);
                                }
                                
                                result.Items.Add(i);
                            }
                        }
                        break;
                    case Engine.SFTP:
                        {
                            var items = await sftp.ListDirectoryAsync(path);
                            result.Items = new List<XwRemoteIOItem>(items.Count());
                            foreach (var item in items)
                            {
                                if (item.Name == "." || item.Name == "..")
                                    continue;

                                XwRemoteIOItem i = new XwRemoteIOItem();
                                i.IsDirectory = item.IsDirectory;
                                i.IsSymlink = item.IsSymbolicLink;
                                i.Name = item.Name;
                                i.FullName = item.FullName;
                                i.Size = item.Length;
                                i.Modified = item.LastWriteTime;

                                //OH this is so bad...
                                if (i.IsSymlink)
                                {
                                    try
                                    {
                                        sftp.ChangeDirectory(item.FullName);
                                        i.IsDirectory = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        i.IsDirectory = false;
                                    }
                                }
                                
                                result.Items.Add(i);
                            }
                            result.Items.Sort((x, y) => x.Name.CompareTo(y.Name));
                        }
                        break;
                    case Engine.S3:
                        {
                            if (!path.EndsWith("/"))
                                path += "/";
                            if (path.StartsWith("/"))
                                path = path.Remove(0, 1);

                            ListObjectsV2Request request = new ListObjectsV2Request();
                            request.BucketName = Hostname;
                            request.Delimiter = "/";
                            request.Prefix = path;
                            request.MaxKeys = 1000000;
                            var items = await s3.ListObjectsV2Async(request);
                            result.Items = new List<XwRemoteIOItem>(items.CommonPrefixes.Count + items.S3Objects.Count);
                            
                            foreach (var item in items.CommonPrefixes)
                            {
                                string[] names = item.Split("/");
                                string name = names[names.Length - 1];
                                XwRemoteIOItem i = new XwRemoteIOItem();
                                i.IsDirectory = true;
                                i.IsSymlink = false;
                                i.Name = name;
                                i.FullName = item;
                                result.Items.Add(i);
                            }

                            foreach (var item in items.S3Objects)
                            {
                                if (item.Key == path)
                                    continue;

                                string[] names = item.Key.Split("/");
                                string name = names[names.Length - 1];
                                XwRemoteIOItem i = new XwRemoteIOItem();
                                i.IsDirectory = false;
                                i.IsSymlink = false;
                                i.Name = name;
                                i.FullName = item.Key;
                                i.Size = item.Size;
                                i.Modified = item.LastModified;
                                result.Items.Add(i);
                            }
                        }
                        break;
                    case Engine.AZUREFILE:
                        {
                            if (path.StartsWith("/"))
                                path = path.Remove(0, 1);

                            if (path.EndsWith("/"))
                                path += path.Remove(path.Length - 1, 1);

                            string[] segments = path.Split("/");

                            //storage, list shared
                            if (segments.Length == 0)
                            {
                                var shares = azure.ListShares();
                                result.Items = new List<XwRemoteIOItem>(shares.Count());
                                foreach (var share in shares)
                                {
                                    XwRemoteIOItem i = new XwRemoteIOItem();
                                    i.IsDirectory = true;
                                    i.IsSymlink = false;
                                    i.Name = share.Name;
                                    i.FullName = share.Uri.AbsolutePath;
                                    i.Size = 0;
                                    i.Modified = share.Properties.LastModified.Value.DateTime;
                                    result.Items.Add(i);
                                }
                            }
                            else
                            {
                                var share = azure.GetShareReference(segments[0]);
                                var dir = share.GetRootDirectoryReference();
                                for (int i = 1; i < segments.Length; i++)
                                    dir = dir.GetDirectoryReference(segments[i]);
                                
                                var items = dir.ListFilesAndDirectories();
                                result.Items = new List<XwRemoteIOItem>(items.Count());
                                foreach (var item in items)
                                {
                                    XwRemoteIOItem i = new XwRemoteIOItem();
                                    i.IsSymlink = false;
                                    i.FullName = item.Uri.AbsolutePath;

                                    if (item is CloudFileDirectory)
                                    {
                                        CloudFileDirectory directory = ((CloudFileDirectory)item);
                                        i.IsDirectory = true;
                                        i.Name = directory.Name;
                                        await directory.FetchAttributesAsync();
                                        if (directory.Properties.LastModified != null)
                                            i.Modified = directory.Properties.LastModified.Value.DateTime;
                                    }
                                    else if (item is CloudFile)
                                    {
                                        CloudFile file = ((CloudFile)item);
                                        i.IsDirectory = false;
                                        i.Name = file.Name;
                                        await file.FetchAttributesAsync();
                                        if (file.Properties.LastModified != null)
                                            i.Modified = file.Properties.LastModified.Value.DateTime;
                                        i.Size = file.Properties.Length;
                                    }
                                    else
                                        throw new Exception($"Uknown object type");
                                    
                                    result.Items.Add(i);
                                }
                            }
                        }
                        break;
                    default:
                        throw new Exception("ListDirectory not implemented for this engine");
                }

                result.Success = true;
                result.Message = $"OK   : ListDirectory: {path}";
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = $"ERROR: ListDirectory: {path} => {ex.Message}";
                if (ThrowExceptions)
                    throw;
            }

            return result;
        }

        //**********************************************************************************************
        public async Task<XwRemoteIOResult> CreateDirectory(string path)
        {
            path = path.Replace("\\", "/");
            XwRemoteIOResult result = new XwRemoteIOResult();
            try
            {
                switch (engine)
                {
                    case Engine.FTP:
                        await ftp.CreateDirectoryAsync(path, true);
                        break;
                    case Engine.SFTP:
                        await Task.Run(() => { sftp.CreateDirectory(path); });
                        break;
                    case Engine.S3:
                        {
                            if (!path.EndsWith("/"))
                                path += "/";
                            if (path.StartsWith("/"))
                                path = path.Remove(0, 1);

                            var request = new PutObjectRequest();
                            request.BucketName = Hostname;
                            request.Key = path;
                            await s3.PutObjectAsync(request);
                        }
                        break;
                    case Engine.AZUREFILE:
                        {
                            string[] segments = path.Split("/");
                            var share = azure.GetShareReference(segments[0]);
                            var dir = share.GetRootDirectoryReference();
                            if (!dir.Exists())
                                dir.Create();

                            for (int i = 1; i < segments.Length; i++)
                            {
                                dir = dir.GetDirectoryReference(segments[i]);
                                if (!dir.Exists())
                                    dir.Create();
                            }
                        }
                        break;
                    default:
                        throw new Exception("CreateDirectory not implemented for this engine");
                }
                result.Success = true;
                result.Message = $"OK   : CreateDirectory: {path}";
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = $"ERROR: CreateDirectory: {path} => {ex.Message}";
                if (ThrowExceptions)
                    throw;
            }

            return result;
        }

        //**********************************************************************************************
        public async Task<bool> Exists(string path)
        {
            path = path.Replace("\\", "/");
            switch (engine)
            {
                case Engine.FTP:
                    return (await ftp.DirectoryExistsAsync(path) || await ftp.FileExistsAsync(path));
                case Engine.SFTP:
                    return await Task.Run(() => sftp.Exists(path));
                case Engine.S3:
                    {
                        try
                        {
                            var response = await s3.GetObjectMetadataAsync(Hostname, path);
                            return true;
                        }
                        catch (Amazon.S3.AmazonS3Exception ex)
                        {
                            if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                                return false;
                            throw;
                        }
                    }
                case Engine.AZUREFILE:
                    {
                        string[] segments = path.Split("/");
                        var share = azure.GetShareReference(segments[0]);
                        if (share.Uri.AbsolutePath == path && share.Exists())
                            return true;

                        var dir = share.GetRootDirectoryReference();
                        for (int i = 1; i < segments.Length; i++)
                        {
                            var parent = dir;
                            dir = dir.GetDirectoryReference(segments[i]);
                            if (dir.Uri.AbsolutePath == path && dir.Exists())
                                return true;

                            if (dir.Uri.AbsolutePath == path)
                            {
                                var items = parent.ListFilesAndDirectories();
                                foreach (var item in items)
                                {
                                    if (item is CloudFile)
                                    {
                                        if (item.Uri.AbsolutePath == path && ((CloudFile)item).Exists())
                                            return true;
                                    }
                                }
                            }
                        }
                    
                        return false;
                    }
                    break;
                default:
                    throw new Exception("Exists not implemented for this engine");
            }
        }

        //**********************************************************************************************
        public async Task<XwRemoteIOResult> Rename(string oldName, string newName)
        {
            oldName = oldName.Replace("\\", "/");
            newName = newName.Replace("\\", "/");
            XwRemoteIOResult result = new XwRemoteIOResult();
            try
            {
                switch (engine)
                {
                    case Engine.FTP:
                        await ftp.RenameAsync(oldName, newName);
                        break;
                    case Engine.SFTP:
                        await Task.Run(() => { sftp.RenameFile(oldName, newName); });
                        break;
                    case Engine.S3:
                        {
                            if (oldName.EndsWith("/") && !newName.EndsWith("/"))
                                newName += "/";

                            await s3.CopyObjectAsync(Hostname, oldName, Hostname, newName);
                            await DeleteFile(oldName);
                        }
                        break;
                    default:
                        throw new Exception("Rename not implemented for this engine");
                }
                result.Success = true;
                result.Message = $"OK   : Rename: {oldName} to {newName}";
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = $"ERROR: Rename: {oldName} to {newName} => {ex.Message}";
                if (ThrowExceptions)
                    throw;
            }
            return result;
        }
        
        //**********************************************************************************************
        public async Task<XwRemoteIOResult> DeleteDirectory(string path)
        {
            path = path.Replace("\\", "/");
            XwRemoteIOResult result = new XwRemoteIOResult();
            try
            {
                await DeleteFolder(path);
                result.Success = true;
                result.Message = $"OK   : DeleteDirectory: {path}";
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg == "Failure")
                    msg = "Failed, Is it empty?";
                result.Exception = ex;
                result.Message = $"ERROR: DeleteDirectory: {path} => {msg}";
                if (ThrowExceptions)
                    throw;
            }

            return result;
        }

        //**********************************************************************************************
        private async Task DeleteFolder(string path)
        {
            var items = await ListDirectory(path);
            foreach (var item in items.Items)
            {
                if (item.IsSymlink)
                    throw new Exception("Can not delete directory that contain Symlinks");

                if (item.IsDirectory)
                    await DeleteFolder(item.FullName);

                if (!item.IsDirectory)
                    await DeleteFile(item.FullName);
            }

            switch (engine)
            {
                case Engine.FTP:
                    await ftp.DeleteDirectoryAsync(path);
                    break;
                case Engine.SFTP:
                    await Task.Run(() => { sftp.DeleteDirectory(path); });
                    break;
                case Engine.S3:
                    {
                        {
                            if (path.StartsWith("/"))
                                path = path.Remove(0, 1);
                            await s3.DeleteObjectAsync(Hostname, path);
                        }
                    }
                    break;
                case Engine.AZUREFILE:
                    {
                        string[] segments = path.Split("/");
                        var share = azure.GetShareReference(segments[0]);
                        var dir = share.GetRootDirectoryReference();
                        for (int i = 1; i < segments.Length; i++)
                        {
                            dir = dir.GetDirectoryReference(segments[i]);
                            if (dir.Uri.AbsolutePath == path && dir.Exists())
                            {
                                var atoms = dir.ListFilesAndDirectories();
                                foreach (var atom in atoms)
                                {
                                    if (atom is CloudFile)
                                    {
                                        ((CloudFile)atom).Delete();
                                    }

                                    if (atom is CloudFileDirectory)
                                    {
                                        await DeleteDirectory(atom.Uri.AbsolutePath);
                                    }
                                }
                                dir.Delete();
                            }
                        }
                    }
                    break;
                default:
                    throw new Exception("DeleteFolder not implemented for this engine");
            }
        }
        
        //**********************************************************************************************
        public async Task<XwRemoteIOResult> DeleteFile(string path)
        {
            path = path.Replace("\\", "/");
            XwRemoteIOResult result = new XwRemoteIOResult();
            try
            {
                switch (engine)
                {
                    case Engine.FTP:
                        await ftp.DeleteFileAsync(path);
                        break;
                    case Engine.SFTP:
                        await Task.Run(() => { sftp.DeleteFile(path); });
                        break;
                    case Engine.S3:
                        {
                            if (path.StartsWith("/"))
                                path = path.Remove(0, 1);
                            await s3.DeleteObjectAsync(Hostname, path);
                        }
                        break;
                    case Engine.AZUREFILE:
                        {
                            string[] segments = path.Split("/");
                            var share = azure.GetShareReference(segments[0]);
                            var dir = share.GetRootDirectoryReference();

                            if (segments.Length == 2)
                            {
                                var atoms = dir.ListFilesAndDirectories();
                                foreach (var atom in atoms)
                                {
                                    if (atom is CloudFile)
                                    {
                                        if (atom.Uri.AbsolutePath == path && ((CloudFile)atom).Exists())
                                        {
                                            ((CloudFile)atom).Delete();
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 1; i < segments.Length - 1; i++)
                                {
                                    dir = dir.GetDirectoryReference(segments[i]);
                                    if (dir.Exists())
                                    {
                                        var atoms = dir.ListFilesAndDirectories();
                                        foreach (var atom in atoms)
                                        {
                                            if (atom is CloudFile)
                                            {
                                                if (atom.Uri.AbsolutePath == path && ((CloudFile)atom).Exists())
                                                {
                                                    ((CloudFile)atom).Delete();
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        throw new Exception("DeleteFile not implemented for this engine");
                }
                result.Success = true;
                result.Message = $"OK   : DeleteFile: {path}";
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = $"ERROR: DeleteFile: {path} => {ex.Message}";
                if (ThrowExceptions)
                    throw;
            }
            return result;
        }
        
        //**********************************************************************************************
        public async Task<XwRemoteIOResult> UploadFile(string local, string remote, string transferID = "", bool resume = false)
        {
            remote = remote.Replace("\\", "/");
            XwRemoteIOResult result = new XwRemoteIOResult();
            try
            {
                switch (engine)
                {
                    case Engine.FTP:
                    {
                        Progress<double> progress = new Progress<double>(x =>
                        {
                            // When in ASCII mode or file size is unavailable, size will be 0 or -1, 
                            // causing progress be Nan, Infinity or minus
                            if (!double.IsNaN(x) && !double.IsInfinity(x) && x >= 0)
                            {
                                XwRemoteIOFileProgressResult prog = new XwRemoteIOFileProgressResult();
                                prog.Percentage = x;
                                prog.TreansferID = transferID;
                                OnFileProgress?.Invoke(prog);
                            }
                        });
                        await ftp.UploadFileAsync(local, remote, (resume)?FtpExists.Append:FtpExists.Overwrite, true, FtpVerify.None, CancellationToken.None, progress);
                    }
                    break;
                    case Engine.SFTP:
                    {
                        using (var localStream = File.OpenRead(local))
                        {
                            long total = localStream.Length;
                            Action<ulong> progress = new Action<ulong>(x =>
                            {
                                XwRemoteIOFileProgressResult prog = new XwRemoteIOFileProgressResult();
                                prog.Percentage = ((double)x * 100f / total);
                                prog.TreansferID = transferID;
                                OnFileProgress?.Invoke(prog);
                            });
    
                            await sftp.UploadAsync(localStream, remote, true, progress);
                        }
                    }
                    break;
                    case Engine.S3:
                        {
                            if (remote.StartsWith("/"))
                                remote = remote.Remove(0, 1);

                            var fileTransferUtility = new TransferUtility(s3);
                            var request = new TransferUtilityUploadRequest
                            {
                                BucketName = Hostname,
                                FilePath = local,
                                Key = remote
                            };
                            
                            request.UploadProgressEvent += (o, e) =>
                            {
                                XwRemoteIOFileProgressResult prog = new XwRemoteIOFileProgressResult();
                                prog.Percentage = ((double)e.TransferredBytes * 100f / e.TotalBytes);
                                prog.TreansferID = transferID;
                                OnFileProgress?.Invoke(prog);
                            };

                            await fileTransferUtility.UploadAsync(request);
                        }
                        break;
                    case Engine.AZUREFILE:
                        {
                            string[] segments = remote.Split("/");
                            var share = azure.GetShareReference(segments[0]);
                            var dir = share.GetRootDirectoryReference();
                            for (int i = 1; i < segments.Length-1; i++)
                                dir = dir.GetDirectoryReference(segments[i]);
                            
                            var file = dir.GetFileReference(segments[segments.Length-1]);
                            using (var localStream = File.OpenRead(local))
                            {
                                long total = localStream.Length;
                                IProgress<StorageProgress> progressHandler = new Progress<StorageProgress>(progress =>
                                {
                                    XwRemoteIOFileProgressResult prog = new XwRemoteIOFileProgressResult();
                                    prog.Percentage = ((double)progress.BytesTransferred * 100f / total);
                                    prog.TreansferID = transferID;
                                    OnFileProgress?.Invoke(prog);
                                });

                                await file.UploadFromFileAsync(local,
                                    default(AccessCondition),
                                    default(FileRequestOptions),
                                    default(OperationContext),
                                    progressHandler,
                                    default(CancellationToken));
                            }
                        }
                        break;
                    default:
                        throw new Exception("UploadFile not implemented for this engine");
                }
                result.Success = true;
                result.Message = $"OK   : UploadFile: {remote}";
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = $"ERROR: UploadFile: {remote} => {ex.Message}";
                if (ThrowExceptions)
                    throw;
            }
            return result;
        }

        //**********************************************************************************************
        public async Task<XwRemoteIOResult> DownloadFile(string local, string remote, string transferID = "", bool overwrite = true)
        {
            remote = remote.Replace("\\", "/");
            XwRemoteIOResult result = new XwRemoteIOResult();
            try
            {
                switch (engine)
                {
                    case Engine.FTP:
                        {
                            Progress<double> progress = new Progress<double>(x =>
                            {
                                // When in ASCII mode or file size is unavailable, size will be 0 or -1, 
                                // causing progress be Nan, Infinity or minus
                                if (!double.IsNaN(x) && !double.IsInfinity(x) && x >= 0)
                                {
                                    XwRemoteIOFileProgressResult prog = new XwRemoteIOFileProgressResult();
                                    prog.Percentage = x;
                                    prog.TreansferID = transferID;
                                    OnFileProgress?.Invoke(prog);
                                }
                            });
                            await ftp.DownloadFileAsync(local, remote, true, FtpVerify.None, CancellationToken.None, progress);
                        }
                        break;
                    case Engine.SFTP:
                        {
                            using (var localStream = File.OpenWrite(local))
                            {
                                long total = sftp.GetAttributes(remote).Size;
                                Action<ulong> progress = new Action<ulong>(x =>
                                {
                                    XwRemoteIOFileProgressResult prog = new XwRemoteIOFileProgressResult();
                                    prog.Percentage = ((double)x * 100f / total);
                                    prog.TreansferID = transferID;
                                    OnFileProgress?.Invoke(prog);
                                });

                                await sftp.DownloadAsync(remote, localStream, progress);
                            }
                        }
                        break;
                    case Engine.S3:
                        {
                            var fileTransferUtility = new TransferUtility(s3);
                            var request = new TransferUtilityDownloadRequest
                            {
                                BucketName = Hostname,
                                FilePath = local,
                                Key = remote
                            };

                            request.WriteObjectProgressEvent += (o, e) =>
                            {
                                XwRemoteIOFileProgressResult prog = new XwRemoteIOFileProgressResult();
                                prog.Percentage = ((double)e.TransferredBytes * 100f / e.TotalBytes);
                                prog.TreansferID = transferID;
                                OnFileProgress?.Invoke(prog);
                            };

                            await fileTransferUtility.DownloadAsync(request);
                        }
                        break;
                    case Engine.AZUREFILE:
                        {
                            string[] segments = remote.Split("/");
                            var share = azure.GetShareReference(segments[0]);
                            var dir = share.GetRootDirectoryReference();
                            for (int i = 1; i < segments.Length - 1; i++)
                                dir = dir.GetDirectoryReference(segments[i]);
                            
                            var file = dir.GetFileReference(segments[segments.Length - 1]);
                            await file.FetchAttributesAsync();
                            long total = file.Properties.Length;
                            IProgress<StorageProgress> progressHandler = new Progress<StorageProgress>(progress =>
                            {
                                XwRemoteIOFileProgressResult prog = new XwRemoteIOFileProgressResult();
                                prog.Percentage = ((double)progress.BytesTransferred * 100f / total);
                                prog.TreansferID = transferID;
                                OnFileProgress?.Invoke(prog);
                            });

                            await file.DownloadToFileAsync(local,
                                FileMode.OpenOrCreate,
                                default(AccessCondition),
                                default(FileRequestOptions),
                                default(OperationContext),
                                progressHandler,
                                default(CancellationToken));
                        }
                        break;
                        break;
                    default:
                        throw new Exception("DownloadFile not implemented for this engine");
                }
                result.Success = true;
                result.Message = $"OK   : DownloadFile: {remote}";
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = $"ERROR: DownloadFile: {remote} => {ex.Message}";
                if (ThrowExceptions)
                    throw;
            }
            return result;
        }

        //**********************************************************************************************
        public async Task<XwRemoteIOResult> GetFileSize(string path)
        {
            path = path.Replace("\\", "/");
            XwRemoteIOResult result = new XwRemoteIOResult();
            try
            {
                switch (engine)
                {
                    case Engine.FTP:
                        {
                            result.Size = await ftp.GetFileSizeAsync(path);
                        }
                        break;
                    case Engine.SFTP:
                        {
                            result.Size = await Task.Run(() => sftp.GetAttributes(path).Size);
                        }
                        break;
                    case Engine.S3:
                        {
                            var response = await s3.GetObjectMetadataAsync(Hostname, path);
                            result.Size = response.ContentLength;
                        }
                        break;
                    case Engine.AZUREFILE:
                        {
                            string[] segments = path.Split("/");
                            var share = azure.GetShareReference(segments[0]);
                            var dir = share.GetRootDirectoryReference();
                            for (int i = 1; i < segments.Length - 1; i++)
                                dir = dir.GetDirectoryReference(segments[i]);

                            var file = dir.GetFileReference(segments[segments.Length - 1]);
                            await file.FetchAttributesAsync();
                            result.Size = file.Properties.Length;
                        }
                        break;
                    default:
                        throw new Exception("GetFileSize not implemented for this engine");
                }
                result.Success = true;
                result.Message = $"OK   : GetFileSize: {path}";
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = $"ERROR: GetFileSize: {path} => {ex.Message}";
                if (ThrowExceptions)
                    throw;
            }
            return result;
        }

        //**********************************************************************************************
        public async Task<XwRemoteIOResult> GetDateModified(string path)
        {
            path = path.Replace("\\", "/");
            XwRemoteIOResult result = new XwRemoteIOResult();
            try
            {
                switch (engine)
                {
                    case Engine.FTP:
                        {
                            result.Modified = await ftp.GetModifiedTimeAsync(path);
                        }
                        break;
                    case Engine.SFTP:
                        {
                            result.Modified = await Task.Run(() => sftp.GetAttributes(path).LastWriteTime);
                        }
                        break;
                    case Engine.S3:
                        {
                            var response = await s3.GetObjectMetadataAsync(Hostname, path);
                            result.Modified = response.LastModified;
                        }
                        break;
                    case Engine.AZUREFILE:
                        {
                            string[] segments = path.Split("/");
                            var share = azure.GetShareReference(segments[0]);
                            var dir = share.GetRootDirectoryReference();
                            for (int i = 1; i < segments.Length - 1; i++)
                                dir = dir.GetDirectoryReference(segments[i]);

                            var file = dir.GetFileReference(segments[segments.Length - 1]);
                            await file.FetchAttributesAsync();
                            result.Modified = file.Properties.LastModified.Value.DateTime;
                        }
                        break;
                    default:
                        throw new Exception("GetModifiedTimeAsync not implemented for this engine");
                }
                result.Success = true;
                result.Message = $"OK   : GetModifiedTimeAsync: {path}";
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = $"ERROR: GetModifiedTimeAsync: {path} => {ex.Message}";
                if (ThrowExceptions)
                    throw;
            }
            return result;
        }

        //**********************************************************************************************
        public event CallBack_OnFileProgress OnFileProgress;
        public delegate void CallBack_OnFileProgress(XwRemoteIOFileProgressResult result);
        public class XwRemoteIOFileProgressResult
        {
            public bool Success = false;
            public double Percentage;
            public string TreansferID;
        }
        
        #region CLEANUP
        //**********************************************************************************************
        ~XwRemoteIO()
        {
            Dispose(false);
        }

        //**********************************************************************************************
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
        //**********************************************************************************************
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    Close();
                }
            }
        }

        //**********************************************************************************************
        public bool IsDisposed { get; protected set; }
        public void Close()
        {
            if (ftp != null)
            {
                if (ftp.IsConnected)
                    ftp.Disconnect();
                if (!ftp.IsDisposed)
                    ftp.Dispose();
                ftp = null;
            }

            if (sftp != null)
            {
                if (sftp.IsConnected)
                    sftp.Disconnect();
                
                sftp.Dispose();
                sftp = null;
            }

            if (s3 != null)
            {
                s3.Dispose();
                s3 = null;
            }

            if (azure != null)
            {
                azure = null;
            }

            if (azureAccount != null)
            {
                azureAccount = null;
            }
            
            IsDisposed = true;
        }
        #endregion
    }

    
    
}
