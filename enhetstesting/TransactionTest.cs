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
    public class TransactionTest
    {

        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = null;

        private readonly Mock<ITransactionRepository> mockRep = new Mock<ITransactionRepository>();
        private readonly Mock<ILogger<TransactionController>> mockLog = new Mock<ILogger<TransactionController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        [Fact]
        public async Task BuyStockLoggetinnOK()
        {
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(k => k.BuyStock("TSLA", 1, 1)).ReturnsAsync(true);
            var transactionController = new TransactionController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            transactionController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await transactionController.BuyStock("TSLA", 1) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resualt.StatusCode);
            Assert.Equal("User bought stock", resualt.Value);
        }
        [Fact]
        public async Task BuyStockNotLoggedOK()
        {
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(k => k.BuyStock("TSLA", 1, 1)).ReturnsAsync(true);
            var transactionController = new TransactionController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            transactionController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await transactionController.BuyStock("TSLA", 1) as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resualt.StatusCode);
            Assert.Equal("User is not logged in", resualt.Value);
        }

        [Fact]
        public async Task BuystockLoggetinnIkkeOK()
        {
            mockRep.Setup(k => k.BuyStock("appl", 1, 1)).ReturnsAsync(false);

            var transactionController = new TransactionController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            transactionController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await transactionController.BuyStock("appl", 1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Could not buy stock", resultat.Value);
        }

        [Fact]
        public async Task BuystockLoggedInIkkeOK()
        {
            mockRep.Setup(k => k.BuyStock("appl", 1, 1)).ReturnsAsync(false);

            var transactionController = new TransactionController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn+_loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            transactionController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await transactionController.BuyStock("appl", 1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Could not buy stock", resultat.Value);
        }


        [Fact]
        public async Task SellStockIkkeOK()
        {
            mockRep.Setup(k => k.SellStock("appl", 1, 1)).ReturnsAsync(false);

            var transactionController = new TransactionController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = null;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            transactionController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await transactionController.SellStock("appl", 1) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("User is not logged in", resultat.Value);
        }


        [Fact]
        public async Task BuyStockWrongInputVal()
        {

            mockRep.Setup(k => k.BuyStock("app7e", 1, 1)).ReturnsAsync(true);

            var transactionController = new TransactionController(mockRep.Object, mockLog.Object);

            transactionController.ModelState.AddModelError("InputValidation", "Fault in InputVal");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            transactionController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await transactionController.BuyStock("app7e", 1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Fault in InputVal", resultat.Value);
        }
        [Fact]
        public async Task SellStockLoggetinnOK()
        {
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(k => k.SellStock("aapl", 1, 1)).ReturnsAsync(true);
            var transactionController = new TransactionController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            transactionController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await transactionController.SellStock("aapl", 1) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resualt.StatusCode);
            Assert.Equal("User sold stock", resualt.Value);
        }

        [Fact]
        public async Task SellStockLoggetInIkkeOK()
        {
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(k => k.SellStock("aapl", 1, 1)).ReturnsAsync(true);
            var transactionController = new TransactionController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn+_loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            transactionController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await transactionController.SellStock("aapl", 1) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resualt.StatusCode);
            Assert.Equal("Could not sell stock", resualt.Value);
        }


        [Fact]
        public async Task SellstockLoggetinnIkkeOK()
        {
            mockRep.Setup(k => k.SellStock("appl", 1, 1)).ReturnsAsync(false);

            var transactionController = new TransactionController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            transactionController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await transactionController.SellStock("appl", 1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Could not sell stock", resultat.Value);
        }
        [Fact]
        public async Task SellStockWrongInputval()
        {

            mockRep.Setup(k => k.SellStock("app7e", 1, 1)).ReturnsAsync(true);

            var transactionController = new TransactionController(mockRep.Object, mockLog.Object);

            transactionController.ModelState.AddModelError("InputValidation", "Fault in InputVal");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            transactionController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await transactionController.SellStock("app7e", 1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);

        }
        
        
    }
}
