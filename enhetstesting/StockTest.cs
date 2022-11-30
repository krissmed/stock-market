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
    public class StockTest
    {

        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = null;

        private readonly Mock<IStockRepository> mockRep = new Mock<IStockRepository>();
        private readonly Mock<ILogger<StockController>> mockLog = new Mock<ILogger<StockController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        [Fact]
        public async Task AddStockLoggetinnOK()
        {
            var mock = new Mock<IStockRepository>();
            mock.Setup(k => k.AddStock("GOOGL")).ReturnsAsync(true);
            var stockController = new StockController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stockController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await stockController.AddStock("GOOGL") as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resualt.StatusCode);
            Assert.Equal("User added stock", resualt.Value);
        }

        [Fact]
        public async Task AddStockIkkeOK()
        {
            var mock = new Mock<IStockRepository>();
            mock.Setup(k => k.AddStock("GOOGL")).ReturnsAsync(true);
            var stockController = new StockController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = null;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stockController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await stockController.AddStock("GOOGL") as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resualt.StatusCode);
            Assert.Equal("User is not logged in", resualt.Value);
        }

        [Fact]
        public async Task AddstockLoggetinnIkkeOK()
        {
            mockRep.Setup(k => k.AddStock("appl")).ReturnsAsync(false);

            var stockController = new StockController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stockController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await stockController.AddStock("appl") as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Could not add stock", resultat.Value);
        }

        [Fact]
        public async Task DeleteStockLoggetinnOK()
        {
            var mock = new Mock<IStockRepository>();
            mock.Setup(k => k.DeleteStock("GOOGL")).ReturnsAsync(true);
            var stockController = new StockController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stockController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await stockController.DeleteStock("GOOGL") as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resualt.StatusCode);
            Assert.Equal("User deleted stock", resualt.Value);
        }

        [Fact]
        public async Task DeleteStockIkkeOK()
        {
            var mock = new Mock<IStockRepository>();
            mock.Setup(k => k.DeleteStock("GOOGL")).ReturnsAsync(true);
            var stockController = new StockController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = null;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stockController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await stockController.DeleteStock("GOOGL") as UnauthorizedObjectResult;

            Assert.Equal((int)HttpStatusCode.Unauthorized, resualt.StatusCode);
            Assert.Equal("User is not logged in", resualt.Value);
        }

        [Fact]
        public async Task DeletestockLoggetinnIkkeOK()
        {
            mockRep.Setup(k => k.DeleteStock("appl")).ReturnsAsync(false);

            var stockController = new StockController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stockController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await stockController.DeleteStock("appl") as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Could not delete stock", resultat.Value);
        }

        [Fact]
        public async Task AddStockWrongInputVal()
        {
            
            mockRep.Setup(k => k.AddStock("55")).ReturnsAsync(true);

            var stockController = new StockController(mockRep.Object, mockLog.Object);

            stockController.ModelState.AddModelError("InputValidation", "Fault in InputVal");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stockController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await stockController.AddStock("55") as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Fault in InputVal", resultat.Value);
        }

        [Fact]
        public async Task DeleteStockWrongInputVal()
        {

            mockRep.Setup(k => k.DeleteStock("55")).ReturnsAsync(true);

            var stockController = new StockController(mockRep.Object, mockLog.Object);

            stockController.ModelState.AddModelError("InputValidation", "Fault in InputVal");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            stockController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await stockController.DeleteStock("55") as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Fault in InputVal", resultat.Value);
        }
    }
}

