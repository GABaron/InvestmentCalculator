namespace ApplicationService.InvestmentProjection
{
    public class InvestmentProjectionQuery
    {
        public InvestmentProjectionQuery(
            decimal lumpSumInvestment,
            decimal monthlyInvestment,
            decimal targetValue,
            int timeScale,
            string riskLevel)
        {
            this.LumpSumInvestment = lumpSumInvestment;
            this.MonthlyInvestment = monthlyInvestment;
            this.TargetValue = targetValue;
            this.TimeScale = timeScale;
            this.RiskLevel = riskLevel;
        }

        public decimal LumpSumInvestment { get; }

        public decimal MonthlyInvestment { get; }

        public decimal TargetValue { get; }

        public int TimeScale { get; }

        public string RiskLevel { get; }
    }
}