using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Renci.SshNet.Sftp;

namespace Renci.SshNet.Async
{
    public static class SshNetExtensions
    {
        /// <summary>
        /// Asynchronously retrieve list of files in remote directory
        /// </summary>
        /// <param name="client">The <see cref="SftpClient"/> instance</param>
        /// <param name="path">The path.</param>
        /// <param name="listCallback">The list callback.</param>
        /// <param name="factory">The <see cref="System.Threading.Tasks.TaskFactory">TaskFactory</see> used to create the Task</param>
        /// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the
        /// created <see cref="T:System.Threading.Tasks.Task">Task</see>.</param>
        /// <param name="scheduler">The <see cref="System.Threading.Tasks.TaskScheduler">TaskScheduler</see>
        /// that is used to schedule the task that executes the end method.</param>
        /// <returns>List of directory entries</returns>
        public static Task<IEnumerable<SftpFile>> ListDirectoryAsync(this SftpClient client,
            string path, Action<int> listCallback = null,
            TaskFactory<IEnumerable<SftpFile>> factory = null,
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = null)
        {
            return (factory = factory ?? Task<IEnumerable<SftpFile>>.Factory).FromAsync(
                client.BeginListDirectory(path, null, null, listCallback),
                client.EndListDirectory,
                creationOptions, scheduler ?? factory.Scheduler ?? TaskScheduler.Current);
        }

        /// <summary>
        /// Asynchronously download the file into the stream.
        /// </summary>
        /// <param name="client">The <see cref="SftpClient"/> instance</param>
        /// <param name="path">Remote file path.</param>
        /// <param name="output">Data output stream.</param>
        /// <param name="factory">The <see cref="System.Threading.Tasks.TaskFactory">TaskFactory</see> used to create the Task</param>
        /// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the
        /// created <see cref="T:System.Threading.Tasks.Task">Task</see>.</param>
        /// <param name="scheduler">The <see cref="System.Threading.Tasks.TaskScheduler">TaskScheduler</see>
        /// that is used to schedule the task that executes the end method.</param>
        /// <returns></returns>
        public static Task DownloadAsync(this SftpClient client,
            string path, Stream output,
            TaskFactory factory = null,
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = null)
        {
            return (factory = factory ?? Task.Factory).FromAsync(
                client.BeginDownloadFile(path, output),
                client.EndDownloadFile,
                creationOptions, scheduler ?? factory.Scheduler ?? TaskScheduler.Current);
        }

        /// <summary>
        /// Asynchronously download the file into the stream.
        /// </summary>
        /// <param name="client">The <see cref="SftpClient"/> instance</param>
        /// <param name="path">Remote file path.</param>
        /// <param name="output">Data output stream.</param>
        /// <param name="downloadCallback">The download callback.</param>
        /// <param name="factory">The <see cref="System.Threading.Tasks.TaskFactory">TaskFactory</see> used to create the Task</param>
        /// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the
        /// created <see cref="T:System.Threading.Tasks.Task">Task</see>.</param>
        /// <param name="scheduler">The <see cref="System.Threading.Tasks.TaskScheduler">TaskScheduler</see>
        /// that is used to schedule the task that executes the end method.</param>
        /// <returns></returns>
        public static Task DownloadAsync(this SftpClient client,
            string path, Stream output, Action<ulong> downloadCallback,
            TaskFactory factory = null,
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = null)
        {
            return (factory = factory ?? Task.Factory).FromAsync(
                client.BeginDownloadFile(path, output, null, null, downloadCallback),
                client.EndDownloadFile,
                creationOptions, scheduler ?? factory.Scheduler ?? TaskScheduler.Current);
        }

        /// <summary>
        /// Asynchronously upload the stream into the remote file.
        /// </summary>
        /// <param name="client">The <see cref="SftpClient"/> instance</param>
        /// <param name="input">Data input stream.</param>
        /// <param name="path">Remote file path.</param>
        /// <param name="uploadCallback">The upload callback.</param>
        /// <param name="factory">The <see cref="System.Threading.Tasks.TaskFactory">TaskFactory</see> used to create the Task</param>
        /// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the
        /// created <see cref="T:System.Threading.Tasks.Task">Task</see>.</param>
        /// <param name="scheduler">The <see cref="System.Threading.Tasks.TaskScheduler">TaskScheduler</see>
        /// that is used to schedule the task that executes the end method.</param>
        /// <returns></returns>
        public static Task UploadAsync(this SftpClient client,
            Stream input, string path, Action<ulong> uploadCallback = null,
            TaskFactory factory = null,
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = null)
        {
            return (factory = factory ?? Task.Factory).FromAsync(
                client.BeginUploadFile(input, path, null, null, uploadCallback),
                client.EndUploadFile,
                creationOptions, scheduler ?? factory.Scheduler ?? TaskScheduler.Current);
        }

        /// <summary>
        /// Asynchronously upload the stream into the remote file.
        /// </summary>
        /// <param name="client">The <see cref="SftpClient"/> instance</param>
        /// <param name="input">Data input stream.</param>
        /// <param name="path">Remote file path.</param>
        /// <param name="canOverride">if set to <c>true</c> then existing file will be overwritten.</param>
        /// <param name="uploadCallback">The upload callback.</param>
        /// <param name="factory">The <see cref="System.Threading.Tasks.TaskFactory">TaskFactory</see> used to create the Task</param>
        /// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the
        /// created <see cref="T:System.Threading.Tasks.Task">Task</see>.</param>
        /// <param name="scheduler">The <see cref="System.Threading.Tasks.TaskScheduler">TaskScheduler</see>
        /// that is used to schedule the task that executes the end method.</param>
        /// <returns></returns>
        public static Task UploadAsync(this SftpClient client,
            Stream input, string path, bool canOverride, Action<ulong> uploadCallback = null,
            TaskFactory factory = null,
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = null)
        {
            return (factory = factory ?? Task.Factory).FromAsync(
                client.BeginUploadFile(input, path, canOverride, null, null, uploadCallback),
                client.EndUploadFile,
                creationOptions, scheduler ?? factory.Scheduler ?? TaskScheduler.Current);
        }

        /// <summary>
        /// Asynchronously synchronizes the directories.
        /// </summary>
        /// <param name="client">The <see cref="SftpClient"/> instance</param>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="factory">The <see cref="System.Threading.Tasks.TaskFactory">TaskFactory</see> used to create the Task</param>
        /// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the
        /// created <see cref="T:System.Threading.Tasks.Task">Task</see>.</param>
        /// <param name="scheduler">The <see cref="System.Threading.Tasks.TaskScheduler">TaskScheduler</see>
        /// that is used to schedule the task that executes the end method.</param>
        /// <returns>List of uploaded files.</returns>
        public static Task<IEnumerable<FileInfo>> SynchronizeDirectoriesAsync(this SftpClient client,
            string sourcePath, string destinationPath, string searchPattern,
            TaskFactory<IEnumerable<FileInfo>> factory = null,
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = null)
        {
            return (factory = factory ?? Task<IEnumerable<FileInfo>>.Factory).FromAsync(
                client.BeginSynchronizeDirectories(sourcePath, destinationPath, searchPattern, null, null),
                client.EndSynchronizeDirectories,
                creationOptions, scheduler ?? factory.Scheduler ?? TaskScheduler.Current);
        }
    }
}
