
using System.Threading;

namespace XwRemote.Servers
{
    struct DiskItem
    {
        public bool IsDirectory;
        public bool IsSymlink;
        public string path;
        public string name;
        public long size;

        public DiskItem(bool IsDir, bool IsLink, string DirPath, string Name, long Size = 0)
        {
            IsDirectory = IsDir;
            IsSymlink = IsLink;
            path = DirPath;
            size = Size;
            name = Name;
        }
    }

    public enum QueueStatus
    {
        Queue,
        Progress,
        Stopped,
        Paused,
        Error
    }

    public struct QueueItem
    {
        public int ImageIndex;
        public bool Download;
        public bool SourceIsdir;
        public string SourcePath;
        public string DestinationPath;
        public string Name;
        public QueueStatus Status;
        public string TransferID;
        public long Size;
        public long Transferred;
        public CancellationTokenSource CancelTokenSource;
    }

    struct LoadRemotePath
    {
        public IOForm form;
        public string path;

        public LoadRemotePath(IOForm form, string path)
        {
            this.form = form;
            this.path = path;
        }
    }
}