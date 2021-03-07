using Domain.InvestmentProjection;
using System;

namespace ApplicationService.InvestmentProjection
{
    public class InvestmentProjectionProcessor : IInvestmentProjectionProcessor
    {
        private InvestmentProjectionCalculator investmentProjectionCalcualtor;

        public InvestmentProjectionProcessor()
        {
            this.investmentProjectionCalcualtor = new InvestmentProjectionCalculator();
        }

        public InvestmentProjectionProcessorResult Process(InvestmentProjectionQuery investProjectionQuery)
        {           
            try
            {
                var domainInvestmentProjectionQuery = InvestmentProjectionQueryMapper.Map(investProjectionQuery);
                var projectionFigures = this.investmentProjectionCalcualtor.CalculateInvestmentProjection(domainInvestmentProjectionQuery);

                var mappedYearlyBalances = YearlyBalanceMapper.Map(projectionFigures);

                return InvestmentProjectionProcessorResult.Ok(mappedYearlyBalances);
            }
            catch (Exception ex)
            {
                return InvestmentProjectionProcessorResult.Failure(ex.Message);
            }                                  
        }
    }
}
