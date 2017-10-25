using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Loowoo.Land.OA.API
{
    /// <summary>
    /// Summary description for JSWJ
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class JSWJ : System.Web.Services.WebService
    {
        private readonly Managers.ManagerCore Core = Managers.ManagerCore.Instance;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 接收文件
        /// </summary>
        /// <param name="id">唯一标识，String值，取Guid作为唯一识别号，长度36位</param>
        /// <param name="mmjb">秘密级别，可以为空，String值，一般填写默认值“普件”</param>
        /// <param name="jjcd">紧急程度，可以为空，String值，一般填写默认值“普件”</param>
        /// <param name="lwbh">来文编号，String值，如果为空可以填写“无”</param>
        /// <param name="lsh">流水号，String值，默认为空，不填写</param>
        /// <param name="bt">标题，String值，必填，不超过300字</param>
        /// <param name="wjzl">文件种类，String值，默认为空</param>
        /// <param name="lwdw">来文单位，String值，必填，不超过200字</param>
        /// <param name="ztc">主题词，String值，默认为空，不填写</param>
        /// <param name="zy">摘要，String值，默认为空，不填写</param>
        /// <param name="cjrid">推送用户id，String值，默认为空，不填写</param>
        /// <param name="dwid">单位ID，String值，必填，默认填写”dw001”</param>
        /// <param name="qsrq">签收日期，String值，默认为空，不填写</param>
        /// <param name="lylb">来源类别，String值，默认填写”fw”</param>
        /// <param name="fromXxid">来源识别号，String值，填写外部OA自己地方的唯一值</param>
        /// <param name="fromWebServicePath">来源地址，String值，填写外部OA自己的地址</param>
        /// <returns></returns>
        [WebMethod]
        public bool js_wj2(
                    string id,
                    string mmjb,
                    string jjcd,
                    string lwbh,
                    string lsh,
                    string bt,
                    string wjzl,
                    string lwdw,
                    string ztc,
                    string zy,
                    string cjrid,
                    string dwid,
                    string qsrq,
                    string lylb,
                    string fromXxid,
                    string fromWebServicePath)
        {

            var info = Core.FormInfoManager.GetModelByUid(fromXxid);
            if (info == null)
            {
                var form = Core.FormManager.GetModel(FormType.ReceiveMissive);
                var poster = Core.UserManager.GetModel(cjrid);
                var posterId = poster == null ? 0 : poster.ID;
                if (posterId == 0)
                {
                    int.TryParse(System.Configuration.ConfigurationManager.AppSettings["DefaultMissivePosterId"], out posterId);
                }
                info = new FormInfo
                {
                    Title = bt,
                    CreateTime = string.IsNullOrEmpty(qsrq) ? DateTime.Now : DateTime.Parse(qsrq),
                    FormId = form.ID,
                    PostUserId = posterId,
                    Uid = fromXxid
                };
                Core.FormInfoManager.Save(info);
            }
            if (info.FlowDataId == 0)
            {
                Core.FlowDataManager.CreateFlowData(info);
            }

            var missive = new Missive
            {
                WJ_MJ = mmjb == "普件" || string.IsNullOrEmpty(mmjb) ? WJMJ.Normal : WJMJ.Secret,
                JJ_DJ = jjcd == "普件" || string.IsNullOrEmpty(jjcd) ? JJDJ.Normal : JJDJ.Fast,
                WJ_ZH = lwbh,
                WJ_BT = bt,
                WJ_LY = lwdw,
                ZTC = ztc,
                WJ_ZY = zy,
            };
            Core.MissiveManager.Save(missive);
            return true;
        }

        /// <summary>
        /// 接收文件数据方法（附件数据）
        /// </summary>
        /// <param name="ftableid">对应基础数据的ID唯一号，String值，36位Guid</param>
        /// <param name="fjmc">文件名称，包含后缀名称，String值</param>
        /// <param name="fjpath">相对文件路径，该路径是服务器地址，String值，该值为固定规则格式：uploadFile\GUID\文件名称 
        /// 这里的GUID就是上面的ftableid字段，文件名称是fjmc字段
        /// </param>
        /// <param name="lytype">附件类型，String值，填写值“zw”或者“fj”，代表正文或者附件，通常正文只有一个</param>
        /// <returns></returns>
        [WebMethod]
        public bool wj_fj(string ftableid, string fjmc, string fjpath, string lytype)
        {
            var info = Core.FormInfoManager.GetModelByUid(ftableid);
            var file = new File
            {
                FileName = fjmc,
                InfoId = info.ID,
                SavePath = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(fjmc),
                Inline = lytype == "zw" ? true : false
            };
            Core.FileManager.Save(file);
            return true;
        }

        /// <summary>
        /// 发送文件方法（传输文件）
        /// </summary>
        /// <param name="file">文件数据，字节数组，需要将文件以流的方式转换为字节数组进行发送</param>
        /// <param name="fjmc">文件名称，String值，需要与js_wjfj方法里面的文件名称对应</param>
        /// <param name="newid">唯一识别值，String值，36位GUID值，与js_wjfj里面的ftableid一致</param>
        /// <returns></returns>
        [WebMethod]
        public bool getFile(byte[] file, string fjmc, string newid)
        {
            var info = Core.FormInfoManager.GetModelByUid(newid);
            var model = Core.FileManager.GetModel(info.ID, fjmc);
            if (model != null)
            {
                try
                {
                    var data = File.Upload(file, fjmc, model.SavePath);
                    model.Size = file.Length;
                    Core.FileManager.Save(model);

                    return true;
                }
                catch (Exception ex)
                {
                    LogWriter.Instance.WriteLog($"[{DateTime.Now}]\t{ex.Message}\r\n");
                }
            }
            return false;
        }

        /// <summary>
        /// 发送文件方法（传输文件）
        /// </summary>
        /// <param name="file">文件数据，字节数组，需要将文件以流的方式转换为字节数组进行发送</param>
        /// <param name="fjmc">文件名称，String值，需要与js_wjfj方法里面的文件名称对应</param>
        /// <param name="newid">唯一识别值，String值，36位GUID值，与js_wjfj里面的ftableid一致</param>
        /// <param name="ifCreate">判断追加还是新建，string值，填写true表示新建，填写false表示追加</param>
        /// <returns></returns>
        [WebMethod]
        public bool getFile_DWJ(byte[] file, string fjmc, string newid, bool ifCreate = true)
        {
            if (ifCreate) return getFile(file, fjmc, newid);

            var info = Core.FormInfoManager.GetModelByUid(newid);
            var model = Core.FileManager.GetModel(info.ID, fjmc);
            if (model != null)
            {
                try
                {
                    model.Size = File.Append(file, model.SavePath);
                    Core.FileManager.Save(model);
                    return true;
                }
                catch (Exception ex)
                {
                    LogWriter.Instance.WriteLog($"[{DateTime.Now}]\t{ex.Message}\r\n");
                }
            }
            return false;
        }
    }
}
