using System.Collections.Generic;

namespace ApplicationService.InvestmentProjection
{
    public static class YearlyBalanceMapper
    {
        public static List<YearlyBalance> Map(Dictionary<int, decimal> yearlyBalances)
        {
            List<YearlyBalance> yearlyBalanceList = new List<YearlyBalance>();

            foreach (KeyValuePair<int, decimal> yearlyBalance in yearlyBalances)
            {
                yearlyBalanceList.Add(new YearlyBalance(yearlyBalance.Key, yearlyBalance.Value));
            }

            return yearlyBalanceList;
        }
    }
}
