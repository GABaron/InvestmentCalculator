namespace ClientInvestmentsCalculator.Models
{ 
    public class InvestmentProjectionRequest
    {
        public decimal LumpSumInvestment { get; set; }

        public decimal MonthlyInvestment { get; set; }

        public decimal TargetValue { get; set; }

        public int TimeScale { get; set; }

        public string RiskLevel { get; set; }
    }
}
