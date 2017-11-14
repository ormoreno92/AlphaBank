using DataBase;
using Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Logic
{
    public class GetData
    {
        private readonly AlphaBankEntities _db = new AlphaBankEntities();

        public async Task<DummyObj> GetCustomerInfo(string customerId)
        {
            return await Task<DummyObj>.Factory.StartNew(() =>
            {
                try
                {
                    var query = "exec dbo.sp_searchUser '" + customerId + "'";
                    return _db.Database.SqlQuery<DummyObj>(query).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            });
        }
    }
}
