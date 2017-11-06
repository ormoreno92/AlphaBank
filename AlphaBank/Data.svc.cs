using Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaBank
{
    public class Data : IData
    {
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

        public async Task<DummyObj> GetDummyInfo()
        {
            return await Task<DummyObj>.Factory.StartNew(() =>
            {
                try
                {
                    return new DummyObj
                    {
                        TransactionId = Guid.NewGuid().ToString(),
                        CustomerName = "Pedro",
                        CustomerLastName = "El Escamoso",
                        LocatorId = GetVoucherNumber(),
                        Valid = true,
                        validUntil = DateTime.Now.AddDays(1)
                    };
                }
                catch (Exception)
                {
                    return null;
                }
            });
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
