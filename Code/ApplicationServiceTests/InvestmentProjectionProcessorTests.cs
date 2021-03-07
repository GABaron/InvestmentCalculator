using ApplicationService.InvestmentProjection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationServiceTests
{
    public class InvestmentProjectionProcessorTests
    {
        private InvestmentProjectionProcessor sut;

        [SetUp]
        public void Setup()
        {
            this.sut = new InvestmentProjectionProcessor();
        }

        [Test]
        public void ValidInvestmentProjectionQueryProvidedWithLowRiskLevel_CorrectProjectionResultReturned()
        {
            var expectedResult = new List<YearlyBalance>
            {
                new YearlyBalance(2021, 10, 10),
                new YearlyBalance(2022, 28.45m, 22),
                new YearlyBalance(2023, 54.75m, 34),
                new YearlyBalance(2024, 92.25m, 46),
                new YearlyBalance(2025, 145.72m, 58),
                new YearlyBalance(2026, 221.95m, 70)
            };

            var investmentProjectionQuery = new InvestmentProjectionQuery(10m, 1m, 20m, 5, "Low");
            var result = this.sut.Process(investmentProjectionQuery);

            Assert.That(result.ProjectionFigures.Select(x => new { x.Balance, x.Year, x.TotalAmountInvested }),
                Is.EquivalentTo(expectedResult.Select(y => new { y.Balance, y.Year, y.TotalAmountInvested })));            
        }

        [Test]
        public void ValidInvestmentProjectionQueryProvidedWithMediumRiskLevel_CorrectProjectionResultReturned()
        {
            var expectedResult = new List<YearlyBalance>
            {
                new YearlyBalance(2021, 10, 10),
                new YearlyBalance(2022, 33.88m, 22),
                new YearlyBalance(2023, 76.76m, 34),
                new YearlyBalance(2024, 153.77m, 46),
                new YearlyBalance(2025, 292.07m, 58),
                new YearlyBalance(2026, 540.43m, 70)
            };

            var investmentProjectionQuery = new InvestmentProjectionQuery(10m, 1m, 100m, 5, "Medium");
            var result = this.sut.Process(investmentProjectionQuery);

            Assert.That(result.ProjectionFigures.Select(x => new { x.Balance, x.Year, x.TotalAmountInvested }),
                Is.EquivalentTo(expectedResult.Select(y => new { y.Balance, y.Year, y.TotalAmountInvested })));
        }

        [Test]
        public void ValidInvestmentProjectionQueryProvidedWithHighRiskLevel_CorrectProjectionResultReturned()
        {
            var expectedResult = new List<YearlyBalance>
            {
                new YearlyBalance(2021, 10, 10),
                new YearlyBalance(2022, 40.41m, 22),
                new YearlyBalance(2023, 108.90m, 34)
            };

            var investmentProjectionQuery = new InvestmentProjectionQuery(10m, 1m, 200m, 2, "High");
            var result = this.sut.Process(investmentProjectionQuery);

            Assert.That(result.ProjectionFigures.Select(x => new { x.Balance, x.Year, x.TotalAmountInvested }),
                Is.EquivalentTo(expectedResult.Select(y => new { y.Balance, y.Year, y.TotalAmountInvested })));
        }
    }
}