using ApplicationService.InvestmentProjection;
using ClientInvestmentsCalculator.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientInvestmentsCalculator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvestmentsController : ControllerBase
    {
        private readonly IInvestmentProjectionProcessor investmentProjectionProcessor;

        public InvestmentsController(IInvestmentProjectionProcessor investmentProjectionProcessor)
        {
            this.investmentProjectionProcessor = investmentProjectionProcessor;
        }
      
        [HttpGet]
        public ActionResult GetInvestmentProjection(decimal lumpSumInvestment, decimal monthlyInvestment, decimal targetValue, int timeScale, string riskLevel)
        {
            var investmentProjectionQuery = InvestmentProjectionRequestMapper.Map(lumpSumInvestment, monthlyInvestment, targetValue, timeScale, riskLevel);
            var investmentProjectionProcessorResult = this.investmentProjectionProcessor.Process(investmentProjectionQuery);

            if (investmentProjectionProcessorResult.Success)
            {
                return this.Ok(investmentProjectionProcessorResult.ProjectionFigures);
            }

            return this.StatusCode(StatusCodes.Status500InternalServerError, "The request could not be completed due to an internal error.");
        }
    }
}
