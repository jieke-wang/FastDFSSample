using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using FastDFS.Client;

namespace FastDFS_Console_Sample
{
    class Demo2
    {
        const string GroupName = "group2";
        internal static async Task RunAsync()
        {
            try
            {
                {
                    var fileInfo = await UploadFileAsync();
                    //await GetFileInfoAsync(fileInfo);
                    //await GetFileAsync(fileInfo);
                    await GetFileV2Async(fileInfo);

                    await RemoveFileAsync(fileInfo);
                    await GetFileInfoAsync(fileInfo);
                }

                {
                    var fileInfo = await UploadAppenderFileAsync();
                    await GetFileInfoAsync(fileInfo);
                    //await GetFileAsync(fileInfo);
                    await GetFileV2Async(fileInfo);

                    await AppendFileAsync(fileInfo);
                    await GetFileInfoAsync(fileInfo);
                    //await GetFileAsync(fileInfo);
                    await GetFileV2Async(fileInfo);

                    await RemoveFileAsync(fileInfo);
                    await GetFileInfoAsync(fileInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task<(string GroupName, string filename)> UploadAppenderFileAsync()
        {
            Console.WriteLine("UploadAppenderFile");
            StorageNode storageNode = await FastDFSClient.GetStorageNodeAsync(GroupName);
            Console.WriteLine($"{nameof(storageNode.EndPoint)}: {storageNode.EndPoint}");
            Console.WriteLine($"{nameof(storageNode.GroupName)}: {storageNode.GroupName}");
            Console.WriteLine($"{nameof(storageNode.StorePathIndex)}: {storageNode.StorePathIndex}");

            byte[] data = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            string filename = await FastDFSClient.UploadAppenderFileAsync(storageNode, data, ".txt");
            Console.WriteLine($"{filename}\n");

            await GetFileInfoAsync(storageNode, filename);

            return (storageNode.GroupName, filename);
        }

        private static async Task RemoveFileAsync((string GroupName, string Filename) fileInfo)
        {
            Console.WriteLine("RemoveFile\n");
            await FastDFSClient.RemoveFileAsync(fileInfo.GroupName, fileInfo.Filename);
        }

        private static async Task AppendFileAsync((string GroupName, string Filename) fileInfo)
        {
            Console.WriteLine("AppendFile\n");
            //StorageNode storageNode = await FastDFSClient.GetStorageNodeAsync(GroupName);
            //Console.WriteLine($"{nameof(storageNode.EndPoint)}: {storageNode.EndPoint}{storageNode.GroupName}{storageNode.StorePathIndex}");
            //Console.WriteLine($"{nameof(storageNode.GroupName)}: {storageNode.GroupName}");
            //Console.WriteLine($"{nameof(storageNode.StorePathIndex)}: {storageNode.StorePathIndex}");

            byte[] data = Encoding.UTF8.GetBytes($"\n{DateTime.Now}");
            await FastDFSClient.AppendFileAsync(fileInfo.GroupName, fileInfo.Filename, data);
        }

        private static async Task GetFileAsync((string GroupName, string Filename) fileInfo)
        {
            Console.WriteLine("GetFile");
            StorageNode storageNode = await FastDFSClient.GetStorageNodeAsync(fileInfo.GroupName);
            Console.WriteLine($"{nameof(storageNode.EndPoint)}: {storageNode.EndPoint}");
            Console.WriteLine($"{nameof(storageNode.GroupName)}: {storageNode.GroupName}");
            Console.WriteLine($"{nameof(storageNode.StorePathIndex)}: {storageNode.StorePathIndex}");

            byte[] fileContent = await FastDFSClient.DownloadFileAsync(storageNode, fileInfo.Filename);
            Console.WriteLine($"{Encoding.UTF8.GetString(fileContent)}\n");
        }

        private static async Task GetFileV2Async((string GroupName, string Filename) fileInfo)
        {
            Console.WriteLine("GetFile");
            StorageNode storageNode = await FastDFSClient.GetStorageNodeAsync(fileInfo.GroupName);
            Console.WriteLine($"{nameof(storageNode.EndPoint)}: {storageNode.EndPoint}");
            Console.WriteLine($"{nameof(storageNode.GroupName)}: {storageNode.GroupName}");
            Console.WriteLine($"{nameof(storageNode.StorePathIndex)}: {storageNode.StorePathIndex}");

            //byte[] fileContent = await FastDFSClient.DownloadFileAsync(storageNode, fileInfo.Filename);

            using HttpClient httpClient = new HttpClient();
            byte[] fileContent = await httpClient.GetByteArrayAsync($"http://{storageNode.EndPoint.Address}:7003/{fileInfo.GroupName}/{fileInfo.Filename}");

            Console.WriteLine($"{Encoding.UTF8.GetString(fileContent)}\n");
        }

        private static async Task GetFileInfoAsync((string GroupName, string Filename) fileInfo)
        {
            Console.WriteLine("GetFileInfo");
            StorageNode storageNode = await FastDFSClient.GetStorageNodeAsync(fileInfo.GroupName); Console.WriteLine($"{nameof(storageNode.EndPoint)}: {storageNode.EndPoint}");
            Console.WriteLine($"{nameof(storageNode.GroupName)}: {storageNode.GroupName}");
            Console.WriteLine($"{nameof(storageNode.StorePathIndex)}: {storageNode.StorePathIndex}");

            FDFSFileInfo fdfsFileInfo = await FastDFSClient.GetFileInfoAsync(storageNode, fileInfo.Filename);
            if (fdfsFileInfo != null)
            {
                Console.WriteLine($"{nameof(fdfsFileInfo.FileSize)}: {fdfsFileInfo.FileSize}");
                Console.WriteLine($"{nameof(fdfsFileInfo.CreateTime)}: {fdfsFileInfo.CreateTime}");
                Console.WriteLine($"{nameof(fdfsFileInfo.Crc32)}: {fdfsFileInfo.Crc32}\n");
            }
            else
            {
                Console.WriteLine("文件已删除或不存在\n");
            }
        }

        private static async Task GetFileInfoAsync(StorageNode storageNode, string filename)
        {
            Console.WriteLine("GetFileInfo");

            FDFSFileInfo fdfsFileInfo = await FastDFSClient.GetFileInfoAsync(storageNode, filename);
            if (fdfsFileInfo != null)
            {
                Console.WriteLine($"{nameof(fdfsFileInfo.FileSize)}: {fdfsFileInfo.FileSize}");
                Console.WriteLine($"{nameof(fdfsFileInfo.CreateTime)}: {fdfsFileInfo.CreateTime}");
                Console.WriteLine($"{nameof(fdfsFileInfo.Crc32)}: {fdfsFileInfo.Crc32}\n");
            }
            else
            {
                Console.WriteLine("文件已删除或不存在\n");
            }
        }

        static async Task<(string GroupName, string Filename)> UploadFileAsync()
        {
            Console.WriteLine("UploadFile");
            StorageNode storageNode = await FastDFSClient.GetStorageNodeAsync(GroupName);
            Console.WriteLine($"{nameof(storageNode.EndPoint)}: {storageNode.EndPoint}");
            Console.WriteLine($"{nameof(storageNode.GroupName)}: {storageNode.GroupName}");
            Console.WriteLine($"{nameof(storageNode.StorePathIndex)}: {storageNode.StorePathIndex}");

            byte[] data = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            string filename = await FastDFSClient.UploadFileAsync(storageNode, data, ".txt");
            Console.WriteLine($"{filename}\n");

            await GetFileInfoAsync(storageNode, filename);

            return (storageNode.GroupName, filename);
        }

        internal static async Task AddFileAsync()
        {
            Console.WriteLine("AddFile");
            StorageNode storageNode = await FastDFSClient.GetStorageNodeAsync(GroupName);
            Console.WriteLine($"{nameof(storageNode.EndPoint)}: {storageNode.EndPoint}");
            Console.WriteLine($"{nameof(storageNode.GroupName)}: {storageNode.GroupName}");
            Console.WriteLine($"{nameof(storageNode.StorePathIndex)}: {storageNode.StorePathIndex}");

            byte[] data = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            string filename = await FastDFSClient.UploadFileAsync(storageNode, data, ".txt");
            Console.WriteLine($"{filename}\n");

            await GetFileInfoAsync(storageNode, filename);
        }
    }
}
