using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using stock_market.Controllers;
using stock_market.DAL;
using stock_market.Model;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace enhetstesting
{
    public class PortfolioTest
    {

        //BLIR INNFØRT NÅR LOGIN ER IMPLEMENTERT

        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = null;

        private readonly Mock<IPortfolioRepository> mockRep = new Mock<IPortfolioRepository>();
        private readonly Mock<ILogger<PortfolioController>> mockLog = new Mock<ILogger<PortfolioController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();
        
        [Fact]
        public async Task GetPortfolioNotLoggedOK()
        {
            var mock = new Mock<IPortfolioRepository>();
            mock.Setup(k => k.GetCurrentPortfolio(1)).ReturnsAsync("");
            var portfolioController = new PortfolioController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            portfolioController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await portfolioController.GetCurrentPortfolio() as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resualt.StatusCode);
            Assert.Equal("User is not logged in", resualt.Value);
        }
        [Fact]
        public async Task GetHistoricalPortfolioNotLoggedOK()
        {
            var mock = new Mock<IPortfolioRepository>();
            mock.Setup(k => k.GetHistoricalPortfolios(1)).ReturnsAsync("");
            var portfolioController = new PortfolioController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            portfolioController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await portfolioController.GetHistoricalPortfolios() as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resualt.StatusCode);
            Assert.Equal("User is not logged in", resualt.Value);
        }
    }
}

