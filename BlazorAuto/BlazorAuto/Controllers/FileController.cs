using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Models.NotEntity;
using Models.SystemInfo;
using SqlSugar;

namespace BlazorAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    //[Authorize]
    public class FileController : ControllerBase
    {
        private readonly IHostEnvironment env;
        private readonly ISqlSugarClient db;

        public FileController(IHostEnvironment env, ISqlSugarClient db)
        {
            this.env = env;
            this.db = db;
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileslist"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<string>> Upload(IEnumerable<IFormFile> fileslist)
        {
            Result<string> result = new Result<string>();
            try
            {
                var ip = HttpContext.Connection.RemoteIpAddress;
                var user = HttpContext.User.Identity?.Name;
                long size = fileslist.Sum(f => f.Length);
                if (size > 100 * 1024 * 1024)//100M
                {
                    result.IsSucceeded = false;
                    result.Data = "上传文件过大,最大支持100M";
                    return result;
                }
                foreach (var formFile in fileslist)
                {
                    if (formFile.Length > 0)
                    {
                        var newName = Path.GetRandomFileName();
                        var dic = env.ContentRootPath;
                        var filePath = Path.Combine(dic, "files", newName);
                        if (System.IO.File.Exists(filePath))
                        {
                            result.IsSucceeded = false;
                            result.Data = "运气不好,文件名重复,请再试一次";
                            return result;
                        }
                        UploadFileInfo file = new();
                        file.NewName = newName;
                        file.FileName = formFile.FileName;
                        file.UserName = user;
                        file.ContentType = formFile.ContentType;
                        file.SrcIpAddress = ip?.ToString();
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await formFile.CopyToAsync(stream);
                            result.Data += "成功:" + newName;
                        }
                        await db.Insertable(file).ExecuteCommandAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return result.HasException(ex);
            }
            return result;
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="FilePath">文件相对路径</param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        public IActionResult DownloadFile([FromQuery] string FilePath)
        {
            try
            {
                var dic = env.ContentRootPath;
                var filePath = Path.Combine(dic, "files", FilePath);
                if (!System.IO.File.Exists(filePath))
                {
                    return new EmptyResult();
                }
                string fileName = Path.GetFileName(filePath);
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 4096, true);  //异步读取文件

                return File(fileStream, "application/octet-stream", fileName, true);  //为true时，支持断点续传
            }
            catch (Exception ex)
            {
                return new EmptyResult();
            }
        }

    }
}
