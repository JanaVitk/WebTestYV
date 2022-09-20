using System;
using System.Collections.Generic;
using System.Linq;
using WebTestYV.Models;

namespace WebTestYV.Calc
{
    public static class CalcFunction
    {
        public static DateTime GetKey(int month, int year, InputData.PeriodType period)
        {
            switch (period)
            {
                case InputData.PeriodType.Year:
                    return new DateTime(year, 1, 1).Date;
                case InputData.PeriodType.ThreeMonths:
                    int new_month = month < 4 ? 1 :
                        month < 7 ? 4 :
                        month < 10 ? 7 : 10;
                    return new DateTime(year, new_month, 1).Date;
                case InputData.PeriodType.Month:
                default:
                    return new DateTime(year, month, 1).Date;
            }
        }

        public static DateTime GetKey(DateTime date, InputData.PeriodType period)
        {
            return GetKey(date.Month, date.Year, period);
        }

        public static IEnumerable<ResultData> GetBalance(List<Balance> balances, List<Payment> payments, InputData input)
        {
            var balancesGroup = balances
                .Where(e => e.Account_id == input.AccountId)
                .GroupBy(e => CalcFunction.GetKey(e.Month, e.Year, input.Period),
                   (key, e) => new { key = key, calculation = e.Sum(el => el.Calculation) });
            var paymentsGroup = payments
                .Where(e => e.Account_id == input.AccountId)
                .GroupBy(e => CalcFunction.GetKey(e.Date, input.Period),
                   (key, e) => new { key = key, sum = e.Sum(el => el.Sum) });

            var result = balancesGroup.GroupJoin(
                    paymentsGroup,
                    b => b.key, p => p.key,
                    (b, p) => new ResultData(input.Period)
                    {
                        Start = b.key,
                        Calculation = b.calculation,
                        Payment = p.Sum(el => el.sum),
                        InBalance = 0
                    })
                .OrderBy(el => el.Start).ToList();

            // Формируем балансы, первый берем из данных
            /*float in_balance = balances
                .Where(e => e.Account_id == input.AccountId)
                .OrderBy(e => (new DateTime(e.Year, e.Month, 1)).Date)
                .FirstOrDefault()?.In_balance ?? 0;*/
            float in_balance = 0;
            foreach (var item in result)
            {
                item.InBalance = in_balance;
                in_balance = item.BalanceEnd;
            }

            return result.OrderByDescending(el => el.Start);
        }
    }
}
