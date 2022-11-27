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
        private const string _ikkeLoggetInn = "";

        private readonly Mock<ITransactionRepository> mockRep = new Mock<ITransactionRepository>();
        private readonly Mock<ILogger<TransactionController>> mockLog = new Mock<ILogger<TransactionController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        [Fact]
        public async Task BuyStockLoggetinnOK()
        {
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(k => k.BuyStock("GOOGL", 1)).ReturnsAsync(true);
            var transactionController = new TransactionController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            transactionController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await transactionController.BuyStock("GOOGL", 1) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resualt.StatusCode);
            Assert.Equal("User buy stock", resualt.Value);
        }

        [Fact]
        public async Task BuystockLoggetinnIkkeOK()
        {
            mockRep.Setup(k => k.BuyStock("appl", 1)).ReturnsAsync(false);

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
        public async Task LagreLoggetInnFeilModel()
        {

            mockRep.Setup(k => k.BuyStock("app7e", 1)).ReturnsAsync(true);

            var transactionController = new TransactionController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            transactionController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await transactionController.BuyStock("app7e", 1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }
        [Fact]
        public async Task SellStockLoggetinnOK()
        {
            var mock = new Mock<ITransactionRepository>();
            mock.Setup(k => k.SellStock("appl", 1)).ReturnsAsync(true);
            var transactionController = new TransactionController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            transactionController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await transactionController.SellStock("appl", 1) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resualt.StatusCode);
            Assert.Equal("User sold stock", resualt.Value);
        }
        [Fact]
        public async Task SellstockLoggetinnIkkeOK()
        {
            mockRep.Setup(k => k.SellStock("appl", 1)).ReturnsAsync(false);

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
        public async Task SellStockLoggetInnFeilModel()
        {

            mockRep.Setup(k => k.SellStock("app7e", 1)).ReturnsAsync(true);

            var transactionController = new TransactionController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            transactionController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await transactionController.SellStock("app7e", 1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);

        }
        [Fact]
        public async Task HentAlleLoggetInnOK()
            //Fullføres når loginn er innført
        {
            // Arrange
            var transaction1 = new Transaction
            {
                id = 1,
                ticker = "apple",
                price = 50,
                quantity = 1,
            };
            var transaction2 = new Transaction
            {
                id = 2,
                ticker = "apple",
                price = 50,
                quantity = 1,
            };
            var transaction3 = new Transaction
            {
                id = 3,
                ticker = "apple",
                price = 50,
                quantity = 1,
            };

            var transactionListe = new List<Transaction>();
            transactionListe.Add(transaction1);
            transactionListe.Add(transaction2);
            transactionListe.Add(transaction3);

            mockRep.Setup(k => k.ListAll()).ReturnsAsync(transactionListe);

            var transactionController = new TransactionController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            transactionController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            //var resultat = await transactionController.ListAll() as OkObjectResult;

            // Assert 
           // Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            //Assert.Equal<List<Transaction>>((List<Transaction>)resultat.Value, transactionListe);
        }
    }
}
