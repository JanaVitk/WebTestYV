using System;

namespace WebTestYV.Models
{
    public class ResultData
    {
        private InputData.PeriodType _type;

        private string _formatPeriod
        {
            get
            {
                switch (_type)
                {
                    case InputData.PeriodType.Year:
                        return "yyyy";
                    case InputData.PeriodType.ThreeMonths:
                        string sk = Start.Month < 4 ? "I" :
                        Start.Month < 7 ? "II" :
                        Start.Month < 10 ? "III" : "IV";
                        return $"{sk}кв. [yyyy]";
                    case InputData.PeriodType.Month:
                    default:
                        return "MM.yyyy";
                }
            }
        }
        public string Period => Start.ToString(_formatPeriod);
        public float InBalance { set; get; }
        public float Calculation { set; get; }
        public float Payment { set; get; }
        public float BalanceEnd => (float)Math.Round((Payment - Calculation + InBalance), 2);
        public DateTime Start { set; get; }

        public ResultData(InputData.PeriodType type)
        {
            _type = type;
        }
    }
}
