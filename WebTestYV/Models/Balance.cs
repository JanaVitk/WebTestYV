using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebTestYV.Models
{
    public class Balance
    {
        [JsonPropertyName("account_id")]
        public long Account_id { set; get; }

        [JsonPropertyName("period")]
        public int Period { set; get; }

        [JsonPropertyName("in_balance")]
        public float In_balance { set; get; }

        [JsonPropertyName("calculation")]
        public float Calculation { set; get; }

        [JsonIgnore]
        public int Year => Period / 100;
        [JsonIgnore]
        public int Month => Period % 100;
    }
    public class BalanceAll
    {
        [JsonPropertyName("balance")]
        public List<Balance> Balance { set; get; }

    }
}
