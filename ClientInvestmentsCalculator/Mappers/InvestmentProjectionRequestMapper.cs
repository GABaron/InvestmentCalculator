using ApplicationService.InvestmentProjection;

namespace ClientInvestmentsCalculator.Mappers
{
    public static class InvestmentProjectionRequestMapper
    {
        public static InvestmentProjectionQuery Map(decimal lumpSumInvestment, decimal monthlyInvestment, decimal targetValue, int timeScale, string riskLevel)
        {
            return new InvestmentProjectionQuery(
                lumpSumInvestment,
                monthlyInvestment,
                targetValue,
                timeScale,
                riskLevel);
        }
    }
}
