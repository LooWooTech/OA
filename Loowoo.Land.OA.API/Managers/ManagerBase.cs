using Loowoo.Land.OA.API.Models;

namespace Loowoo.Land.OA.API.Managers
{
    public class ManagerBase
    {
        protected OADbContext GetDbContext()
        {
            return new OADbContext();
        }
    }
}