using Loowoo.Land.OA.API.Models;

namespace Loowoo.Land.OA.API.Managers
{
    public class ManagerBase
    {
        protected ManagerCore Core { get { return ManagerCore.Instance; } }
        protected OADbContext GetDbContext()
        {
            return new OADbContext();
        }
    }
}