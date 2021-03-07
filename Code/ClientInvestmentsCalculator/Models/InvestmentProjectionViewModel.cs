using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClientInvestmentsCalculator.Models
{
    public class InvestmentProjectionViewModel
    {
        [Required]       
        [DisplayName("Lump Sum Investment (£)")]
        public decimal LumpSumInvestment { get; set; }

        [Required]
        [DisplayName("Monthly Investment (£)")]
        public decimal MonthlyInvestment { get; set; }

        [Required]
        [DisplayName("Target Value (£)")]
        public decimal TargetValue { get; set; }

        [Required]
        [DisplayName("Time Scale (years)")]
        public int TimeScale { get; set; }

        [Required]
        [DisplayName("Risk Level")]
        public string RiskLevel { get; set; }
    }
}
