using System.Collections.Generic;

namespace ApplicationService.InvestmentProjection
{
    public class InvestmentProjectionProcessorResult
    {
        private InvestmentProjectionProcessorResult(IEnumerable<YearlyBalance> projectionFigures)
        {
            this.ProjectionFigures = projectionFigures;
            this.Success = true;
        }

        private InvestmentProjectionProcessorResult(string errorMessage)
        {
            this.Success = false;
            this.ErrorMessage = errorMessage;
        }

        public IEnumerable<YearlyBalance> ProjectionFigures { get; }

        public bool Success { get; }

        public string ErrorMessage { get; }

        public static InvestmentProjectionProcessorResult Ok(IEnumerable<YearlyBalance> projectionFigures)
        {
            return new InvestmentProjectionProcessorResult(projectionFigures);
        }

        public static InvestmentProjectionProcessorResult Failure(string errorMessage)
        {
            return new InvestmentProjectionProcessorResult(errorMessage);
        }
    }
}