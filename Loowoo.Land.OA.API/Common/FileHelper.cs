using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Loowoo.Land.OA.API.Common
{
    public static class FileHelper
    {
        private static string _root { get; set; }
        static FileHelper()
        {
            _root = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadFiles");
            if (!Directory.Exists(_root))
            {
                Directory.CreateDirectory(_root);
            }
        }

        public static string GetRoot()
        {
            return _root;
        }

        public static async void Upload(HttpRequestMessage request)
        {
            var provider = new MultipartFormDataStreamProvider(_root);
            var sb = new StringBuilder();
            await request.Content.ReadAsMultipartAsync(provider);
            foreach(var file in provider.FileData)
            {
                var fileName = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                var fileInfo = new FileInfo(file.LocalFileName);
            }
        }
        public static string GetSaveFilePath(string fileName)
        {
            return Path.Combine(_root, string.Format("{0}-{1}{2}", Path.GetFileNameWithoutExtension(fileName), DateTime.Now.ToString("yyyyMMddHHmmssffff"), Path.GetExtension(fileName)));
        }

        public static OA.Models.FileType AnalyzeType(string extension)
        {
            switch (extension)
            {
                case ".doc":
                case ".docx":
                case ".xls":
                case ".xlsx":
                case ".txt":
                case ".pdf":
                    return OA.Models.FileType.Document;
                case ".jpg":
                case ".bnp":
                case ".gif":
                case ".bmp":
                case ".jpeg":
                    return OA.Models.FileType.Image;
                case ".avi":
                case ".wmv":
                case ".mp4":
                case ".rmvb":
                case ".mkv":
                case ".rm":
                case ".mpeg":
                case ".mpg":
                case ".mov":
                    return OA.Models.FileType.Video;
                default:
                    return OA.Models.FileType.Other;


            }
        }
        public static async System.Threading.Tasks.Task<Loowoo.Land.OA.Models.File> Upload2(HttpRequestMessage request,string name)
        {
            var provider = new MultipartMemoryStreamProvider();
            await request.Content.ReadAsMultipartAsync(provider);
            foreach (var file in provider.Contents)
            {
                var fileName = file.Headers.ContentDisposition.FileName;
                if (string.IsNullOrEmpty(fileName))
                {
                    continue;
                }
                if (fileName.ToLower() == name.ToLower())
                {
                    fileName = fileName.Replace("\"", "");
                    var ms = file.ReadAsStreamAsync().Result;
                    using (var br = new BinaryReader(ms))
                    {
                        if (ms.Length <= 0)
                        {
                            break;
                        }
                        var data = br.ReadBytes((int)ms.Length);
                        var info = new FileInfo(fileName);
                        try
                        {
                            var saveFilePath = GetSaveFilePath(fileName);
                            File.WriteAllBytes(saveFilePath, data);
                            return new OA.Models.File
                            {
                                FileName = fileName,
                                SavePath = saveFilePath,
                                Size = info.Length,
                                Type = AnalyzeType(Path.GetExtension(fileName).ToLower())
                            };
                        }
                        catch
                        {

                        }
                    }
                }
             
            }
            return null;
        }

        public static async System.Threading.Tasks.Task<List<Loowoo.Land.OA.Models.File>> Upload2(HttpRequestMessage request)
        {
            var provider = new MultipartMemoryStreamProvider();
            await request.Content.ReadAsMultipartAsync(provider);
            var list = new List<Loowoo.Land.OA.Models.File>();
            foreach(var file in provider.Contents)
            {
                var fileName = file.Headers.ContentDisposition.FileName;
                if (string.IsNullOrEmpty(fileName))
                {
                    continue;
                }
                fileName = fileName.Replace("\"", "");
                var ms = file.ReadAsStreamAsync().Result;
                using (var br=new BinaryReader(ms))
                {
                    if (ms.Length <= 0)
                    {
                        break;
                    }
                    var data = br.ReadBytes((int)ms.Length);
                    var info = new FileInfo(fileName);
                    try
                    {
                        var saveFilePath = GetSaveFilePath(fileName);
                        File.WriteAllBytes(saveFilePath, data);
                        list.Add(new OA.Models.File
                        {
                            FileName = fileName,
                            SavePath = saveFilePath,
                            Size = info.Length,
                            Type = AnalyzeType(Path.GetExtension(fileName).ToLower())
                        });
                    }
                    catch
                    {

                    }
                }
            }
            return list;
        }
    }
}