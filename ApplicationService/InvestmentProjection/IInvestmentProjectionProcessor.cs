namespace ApplicationService.InvestmentProjection
{
    public interface IInvestmentProjectionProcessor
    {
        InvestmentProjectionProcessorResult Process(InvestmentProjectionQuery investProjectionQuery);
    }
}