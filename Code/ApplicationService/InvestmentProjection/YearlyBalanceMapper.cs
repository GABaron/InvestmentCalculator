using Domain.InvestmentProjection;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationService.InvestmentProjection
{
    public static class YearlyBalanceMapper
    {
        public static IEnumerable<YearlyBalance> Map(IEnumerable<DomainYearlyBalance> yearlyBalances)
        {           
            return yearlyBalances.Select(x => new YearlyBalance(x.Year, x.Balance, x.TotalAmountInvested));                      
        }
    }
}
