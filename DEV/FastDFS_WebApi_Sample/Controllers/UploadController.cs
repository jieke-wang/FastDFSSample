using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using FastDFS.Client;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastDFS_WebApi_Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    public class UploadController : ControllerBase
    {
        const string NodeGroup = "group1";

        [HttpPost]
        [Consumes("multipart/form-data")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFileAsync(/*[FromForm] IFormCollection formCollection*/IFormFile file)
        {
            //IFormFile file = formCollection.Files["file"];
            //IFormFile file = HttpContext.Request.Form.Files["file"];
            //StorageNode storageNode = await FastDFSClient.GetStorageNodeAsync(NodeGroup);
            StorageNode storageNode = await FastDFSClient.GetStorageNodeAsync();
            string filename = await FastDFSClient.UploadFileAsync(storageNode, file.OpenReadStream(), Path.GetExtension(file.FileName));

            return Ok(filename);
        }

        [HttpGet("info/{**filename}")]
        public async Task<IActionResult> GetFileInfoAsync([FromRoute] string filename)
        {
            //StorageNode storageNode = await FastDFSClient.GetStorageNodeAsync(NodeGroup);
            StorageNode storageNode = await FastDFSClient.GetStorageNodeAsync();
            FDFSFileInfo fileInfo = await FastDFSClient.GetFileInfoAsync(storageNode, filename);

            return Ok(fileInfo);
        }

        [HttpGet("{**filename}")]
        public async Task<IActionResult> GetFileAsync([FromRoute] string filename)
        {
            //StorageNode storageNode = await FastDFSClient.GetStorageNodeAsync(NodeGroup);
            StorageNode storageNode = await FastDFSClient.GetStorageNodeAsync();
            byte[] fileContent = await FastDFSClient.DownloadFileAsync(storageNode, filename);

            return File(fileContent, "application/octet-stream");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFileAsync([FromQuery] string filename)
        {
            await FastDFSClient.RemoveFileAsync(NodeGroup, filename);

            return Ok();
        }
    }
}
