using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loowoo.Land.OA.Models;
using Loowoo.Common;

namespace Loowoo.Land.OA.Service.Missive
{
    public class JSWJWebService : IMissiveWebService
    {
        private Managers.ManagerCore Core = Managers.ManagerCore.Instance;

        public bool Report(MissiveServiceLog log)
        {
            var model = Core.MissiveManager.GetModel(log.MissiveId);
            var files = Core.FileManager.GetList(new Parameters.FileParameter { InfoId = log.MissiveId });

            var result = "false";
            using (var client = new WebReference.JSWJ())
            {
                foreach (var file in files)
                {
                    result = client.wj_fj(log.Uid, file.FileName, $"uploadFile\\{log.Uid}\\{file.FileName}", file.Inline ? "zw" : "fj");
                    if (result == "true")
                    {
                        try
                        {
                            var uploadDir = AppSettings.Get("UploadDir");
                            var filePath = System.IO.Path.Combine(uploadDir, file.SavePath);
                            using (var fs = System.IO.File.OpenRead(filePath))
                            {
                                var fileData = new byte[fs.Length];
                                fs.Read(fileData, 0, fileData.Length);
                                client.getFile(fileData, file.FileName, log.Uid);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogWriter.Instance.WriteLog($"[{DateTime.Now}]\t{ex.Message}");
                        }
                    }
                }

                result = client.js_wj2(log.Uid,
                    mmjb: model.WJ_MJ == WJMJ.Normal ? "普件" : "保密",
                    jjcd: model.JJ_DJ == JJDJ.Normal ? "普件" : "紧急",
                    lwbh: model.WJ_ZH,
                    lsh: null,
                    bt: model.WJ_BT,
                    wjzl: null,
                    lwdw: "定海区国土资源局",
                    ztc: model.ZTC,
                    zy: null,
                    cjrid: null,
                    dwid: "dw001",
                    qsrq: null,
                    lylb: "fw",
                    fromXxid: model.ID.ToString(),
                    fromWebServicePath: "#/"
                    );
            }
            log.Result = result == "true";
            Core.MissiveManager.UpdateMissiveServiceLog(log);

            return log.Result.Value;

        }
    }
}
