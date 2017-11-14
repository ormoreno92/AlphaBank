using Logic;
using Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaBank
{
    public class Data : IData
    {
        private readonly GetData _data = new GetData();

        public bool TestConnection()
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<DummyObj> GetCustomerInfo(string customerId)
        {
            return await _data.GetCustomerInfo(customerId);
        }

        private string GetVoucherNumber(int length = 10)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }
    }
}
