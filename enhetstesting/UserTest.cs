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
using Microsoft.VisualBasic;

namespace enhetstesting
{
    public class UserTest
    {

        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = null;
        private const int int1 = 1;

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
        public async Task CreateUsernotLoggetinnOK()
        {

            mockRep.Setup(k => k.DeleteUser(1)).ReturnsAsync(true);

            var userController = new UserController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await userController.DeleteUser() as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("User is not logged in", resultat.Value);

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

            mockSession[_loggetInn] = 1;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await userController.CreateUser(user) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Could not create user", resultat.Value);
        }


        [Fact]
        public async Task DeleteUserLoggetinnOK()
        {

            var mock = new Mock<IUserRepository>();
            mock.Setup(k => k.DeleteUser(1)).ReturnsAsync(true);
            var userController = new UserController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await userController.DeleteUser() as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resualt.StatusCode);
            Assert.Equal("User deleted", resualt.Value);
        }
        [Fact]
        public async Task DeleteUserLoggetinnIkkeOK()
        {
            mockRep.Setup(k => k.DeleteUser(1)).ReturnsAsync(true);

            var userController = new UserController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            //mockSession.SetInt32 = 1;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            //mockHttpContext.Setup(s => s.).Returns(true);



            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await userController.DeleteUser() as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("User deleted", resultat.Value);
        }

        [Fact]
        public async Task EditUserLoggetinnOK()
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(k => k.EditUser(It.IsAny<User>())).ReturnsAsync(true);
            var userController = new UserController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await userController.EditUser(It.IsAny<User>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resualt.StatusCode);
            Assert.Equal("User edit user", resualt.Value);
        }

        [Fact]
        public async Task EditUserLoggetinnIkkeOK()
        {
            mockRep.Setup(k => k.EditUser(It.IsAny<User>())).ReturnsAsync(false);

            var userController = new UserController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await userController.EditUser(It.IsAny<User>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Could not edit user", resultat.Value);
        }

        [Fact]
        public async Task EditUserBalanceLoggetinnOK()
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(k => k.EditUserBalance(It.IsAny<User>())).ReturnsAsync(true);
            var userController = new UserController(mock.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resualt = await userController.EditUserBalance(It.IsAny<User>()) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, resualt.StatusCode);
            Assert.Equal("User edited userbal", resualt.Value);
        }
        [Fact]
        public async Task EditUserBalanceLoggetinnIkkeOK()
        {
            mockRep.Setup(k => k.EditUserBalance(It.IsAny<User>())).ReturnsAsync(false);

            var userController = new UserController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await userController.EditUserBalance(It.IsAny<User>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Could not edit userbal", resultat.Value);
        }

        [Fact]
        public async Task CreateUserWrongInputVal()
        {
            var user = new User
            {
                id = 2,
                first_name = "Ol3",
                last_name = "Normann",
                curr_balance = 10,
                curr_balance_liquid = 5,
                curr_balance_stock = 5
            };

            mockRep.Setup(k => k.CreateUser(user)).ReturnsAsync(true);

            var userController = new UserController(mockRep.Object, mockLog.Object);

            userController.ModelState.AddModelError("InputValidation", "Fault in InputVal");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await userController.CreateUser(user) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Fault in InputVal", resultat.Value);
        }
        [Fact]
        public async Task EditUserWrongInputVal()
        {
            var user = new User
            {
                id = 2,
                first_name = "Ole",
                last_name = "Norm4nn",
                curr_balance = 10,
                curr_balance_liquid = 5,
                curr_balance_stock = 5
            };

            mockRep.Setup(k => k.EditUser(user)).ReturnsAsync(true);

            var userController = new UserController(mockRep.Object, mockLog.Object);

            userController.ModelState.AddModelError("InputValidation", "Fault in InputVal");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await userController.EditUser(user) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Fault in InputVal", resultat.Value);
        }
        [Fact]
        public async Task EditUserBalWrongInputVal()
        {
            var user = new User
            {
                id = 2,
                first_name = "Ole",
                last_name = "Normann",
                curr_balance = 0,
                curr_balance_liquid = 5,
                curr_balance_stock = 5
            };

            mockRep.Setup(k => k.EditUserBalance(user)).ReturnsAsync(true);

            var userController = new UserController(mockRep.Object, mockLog.Object);

            userController.ModelState.AddModelError("InputValidation", "Fault in InputVal");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await userController.EditUserBalance(user) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Fault in InputVal", resultat.Value);
        }
    }
}

