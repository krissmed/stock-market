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
using System.Data.OleDb;

namespace enhetstesting
{
    public class UserTest
    {

        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<IUserRepository> mockRep = new Mock<IUserRepository>();
        private readonly Mock<ILogger<UserController>> mockLog = new Mock<ILogger<UserController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        [Fact]
        public async Task CreateUserLoggetinnOK()
        {

            var user = new User
            {
                id = 2,
                first_name = "Ola",
                last_name = "Normann",
                curr_balance = 10,
                curr_balance_liquid = 5,
                curr_balance_stock = 5
            };

            var mock = new Mock<IUserRepository>();
            mock.Setup(k => k.CreateUser(user)).ReturnsAsync(true);
            var userController = new UserController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await userController.CreateUser(user) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resualt.StatusCode);
            Assert.Equal("User created user", resualt.Value);
        }

        [Fact]
        public async Task CreateUserLoggetinnIkkeOK()
        {
            var user = new User
            {
                id = 2,
                first_name = "Ola",
                last_name = "Normann",
                curr_balance = 10,
                curr_balance_liquid = 5,
                curr_balance_stock = 5
            };
            mockRep.Setup(k => k.CreateUser(user)).ReturnsAsync(false);

            var userController = new UserController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await userController.CreateUser(user) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Could not create user", resultat.Value);
        }
    }
}

