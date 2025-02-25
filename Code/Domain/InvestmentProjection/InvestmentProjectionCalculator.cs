using System;
using System.Collections.Generic;

namespace Domain.InvestmentProjection
{
    public class InvestmentProjectionCalculator
    {
        private const decimal LowRiskPercentage = 3m;
        private const decimal MediumRiskPercentage = 5m;
        private const decimal HighRiskPercentage = 7m;
        private const string LowRisk = "LOW";
        private const string MediumRisk = "MEDIUM";
        private const string HighRisk = "HIGH";

        public IEnumerable<DomainYearlyBalance> CalculateInvestmentProjection(DomainInvestmentProjectionQuery investmentProjectionQuery)
        {
            var interestRate = this.GetInterestRate(investmentProjectionQuery.RiskLevel);

            List<DomainYearlyBalance> yearlyProjectionFigures = new List<DomainYearlyBalance>();

            var currentBalance = Math.Round(investmentProjectionQuery.LumpSumInvestment, 2, MidpointRounding.ToEven);
            var numberOfMonthsInTimeScale = GetNumberOfMonthsForTimeScale(investmentProjectionQuery.TimeScale);            
            var currentYear = 0;           
            var totalInvestmentToDate = investmentProjectionQuery.LumpSumInvestment;

            for (int count = 0; count <= numberOfMonthsInTimeScale; count++)
            {
                if (count == 0)
                {                    
                    yearlyProjectionFigures.Add(new DomainYearlyBalance(DateTime.Now.Year, currentBalance, totalInvestmentToDate));
                    currentYear++;
                    continue;
                }

                totalInvestmentToDate += Math.Round(investmentProjectionQuery.MonthlyInvestment, 2, MidpointRounding.ToEven);

                currentBalance += (interestRate / 100) * currentBalance;                
                currentBalance += Math.Round(investmentProjectionQuery.MonthlyInvestment, 2, MidpointRounding.ToEven);                

                if (count != 0 && count % 12 == 0 || count == numberOfMonthsInTimeScale)
                {
                    currentBalance = Math.Round(currentBalance, 2, MidpointRounding.ToEven);
                    var yearForCalculation = DateTime.Now.AddYears(currentYear).Year;                                     
                    yearlyProjectionFigures.Add(new DomainYearlyBalance(yearForCalculation, currentBalance, totalInvestmentToDate));                                        
                    currentYear++;                    
                }               
            }

            return yearlyProjectionFigures;
        }     
        
    private decimal GetInterestRate(string riskLevel)
        {
            var riskLevelUpperCase = riskLevel.ToUpper();

            return riskLevelUpperCase switch
            {
                LowRisk => LowRiskPercentage,
                MediumRisk => MediumRiskPercentage,
                HighRisk => HighRiskPercentage,
                _ => throw new Exception("Could not calculate risk level. Operation aborted.")
            };
        }

        private int GetNumberOfMonthsForTimeScale(int years)
        {
            return years * 12;
        }       
    }
}
