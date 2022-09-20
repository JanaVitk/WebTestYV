using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using WebTestYV.Models;

namespace WebTestYV.Data
{
    public static class DataJson
    {
        private const string _fileNameBalance = "Data//balance.json";
        private const string _fileNamePayment = "Data//payment.json";

        public static async Task<List<Balance>> GetBalances()
        {
            try
            {
                using FileStream fs = File.OpenRead(_fileNameBalance);

                return (await JsonSerializer
                    .DeserializeAsync<BalanceAll>(fs, new JsonSerializerOptions { IncludeFields = true }))
                    .Balance;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<List<Payment>> GetPayment()
        {
            try
            {
                using FileStream fs = File.OpenRead(_fileNamePayment);

                return await JsonSerializer
                    .DeserializeAsync<List<Payment>>(fs, new JsonSerializerOptions { IncludeFields = true });
            }
            catch
            {
                return null;
            }
        }

    }
}
