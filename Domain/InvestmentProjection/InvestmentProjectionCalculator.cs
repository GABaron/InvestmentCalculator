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

        public Dictionary<int, decimal> CalculateInvestmentProjection(DomainInvestmentProjectionQuery investmentProjectionQuery)
        {
            var interestRate = this.GetInterestRate(investmentProjectionQuery.RiskLevel);

            Dictionary<int, decimal> yearlyProjectionFigures = new Dictionary<int, decimal>();

            var currentBalance = Math.Round(investmentProjectionQuery.LumpSumInvestment, 2, MidpointRounding.ToEven);
            var numberOfMonthsInTimeScale = GetNumberOfMonthsForTimeScale(investmentProjectionQuery.TimeScale);            
            var currentYear = 0;

            for (int count = 0; count <= numberOfMonthsInTimeScale; count++)
            {
                if (count == 0)
                {                    
                    yearlyProjectionFigures.Add(DateTime.Now.Year, currentBalance);
                    currentYear++;
                    continue;
                }                                                
                
                currentBalance += (interestRate / 100) * currentBalance;                
                currentBalance += Math.Round(investmentProjectionQuery.MonthlyInvestment, 2, MidpointRounding.ToEven);                

                if (count != 0 && count % 12 == 0 || count == numberOfMonthsInTimeScale)
                {
                    currentBalance = Math.Round(currentBalance, 2, MidpointRounding.ToEven);
                    var yearForCalculation = DateTime.Now.AddYears(currentYear).Year;                                  
                    yearlyProjectionFigures.Add(yearForCalculation, currentBalance);
                    currentYear++;                    
                }               
            }

            return yearlyProjectionFigures;
        }     
        
        private decimal GetInterestRate(string riskLevel)
        {
            var riskLevelUpperCase = riskLevel.ToUpper();

            if (riskLevelUpperCase == LowRisk)
            {
                return LowRiskPercentage;
            }

            if (riskLevelUpperCase == MediumRisk)
            {
                return MediumRiskPercentage;
            }

            if (riskLevelUpperCase == HighRisk)
            {
                return HighRiskPercentage;
            }

            throw new Exception("Could not calculate risk level. Operaton aborted.");
        }


        private int GetNumberOfMonthsForTimeScale(int years)
        {
            return years * 12;
        }
    }
}
