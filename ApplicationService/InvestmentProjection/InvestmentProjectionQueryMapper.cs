using Domain.InvestmentProjection;

namespace ApplicationService.InvestmentProjection
{
    public static class InvestmentProjectionQueryMapper
    {
        public static DomainInvestmentProjectionQuery Map(InvestmentProjectionQuery investmentProjectionRequest)
        {
            return new DomainInvestmentProjectionQuery(
                investmentProjectionRequest.LumpSumInvestment,
                investmentProjectionRequest.MonthlyInvestment,
                investmentProjectionRequest.TargetValue,
                investmentProjectionRequest.TimeScale,
                investmentProjectionRequest.RiskLevel);
        }
    }
}
