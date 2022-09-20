using System;
using System.Text.Json.Serialization;

namespace WebTestYV.Models
{
    public class Payment
    {
        [JsonPropertyName("account_id")]
        public long Account_id { set; get; }

        [JsonPropertyName("date")]
        public string DateStr { set; get; }
        [JsonIgnore]
        public DateTime Date => DateTime.Parse(DateStr);

        [JsonPropertyName("sum")]
        public float Sum { set; get; }

        [JsonPropertyName("payment_guid")]
        public Guid Payment_guid { set; get; }
    }
}
