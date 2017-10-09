using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Service.Missive
{
    public class MissiveReceiveService
    {
        private Managers.ManagerCore Core = Managers.ManagerCore.Instance;

        public void Start()
        {

        }



        private void UploadMissive(Models.Missive model)
        {
            var files = Core.FileManager.GetList(new Parameters.FileParameter { InfoId = model.ID });
            using (var client = new WebReference.JSWJ())
            {
                //创建附件

                //client.getFile()
            }
        }

        public void Stop()
        {

        }
    }
}
