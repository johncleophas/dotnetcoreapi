using DotNetCoreDemo.Api.Data;
using DotNetCoreDemo.Api.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Xunit;
using static DotNetCoreDemo.Api.Services.UserService;

namespace DotNetCoreDemo.Api.Tests
{
    public class UserTestFixture
    {

        protected readonly AppSettings appSettings;

        public UserTestFixture()
        {
            this.appSettings = new AppSettings()
            {
                DatabaseLocation = "D:\\Projects\\angular-dotnetcore-api-stack\\dotnetcoreapi\\"
            };
        }

        [Fact]
        public void UserValidationTestPass()
        {
            // Arrange
            var context = new DataContext(this.appSettings);

            Type type = typeof(UserService);
            var userService = Activator.CreateInstance(type, context);

            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Where(x => x.Name == "ValidateUser")
            .First();

            var user = new User()
            {
                EmailAddress = "dd",
                FirstName = "cc",
                LastName = "ff"
            };

            //Act
            var validationResult = (string)method.Invoke(userService, new object[] { user });

            //Assert
            Assert.NotNull(validationResult);
        }

        [Fact]
        public void UserValidationTestFail()
        {
            // Arrange
            var context = new DataContext(this.appSettings);

            Type type = typeof(UserService);
            var userService = Activator.CreateInstance(type, context);

            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Where(x => x.Name == "ValidateUser")
            .First();

            var user = new User()
            {
                EmailAddress = "dd",
                FirstName = "cc",
            };

            //Act
            var validationResult = (string)method.Invoke(userService, new object[] { user });

            //Assert
            Assert.False(string.IsNullOrEmpty(validationResult));
        }


        [Fact]
        public void GenerateHashTestPass()
        {
            // Arrange
            var context = new DataContext(this.appSettings);

            Type type = typeof(UserService);
            var userService = Activator.CreateInstance(type, context);

            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Where(x => x.Name == "GenerateHash")
            .First();

            string password = "verylongpasswordmustgohere";

            //Act
            var validationResult = (HashSalt)method.Invoke(userService, new object[] { password });

            //Assert
            Assert.NotNull(validationResult);
        }

        [Fact]
        public void GenerateHashTestFail()
        {
            // Arrange
            var context = new DataContext(this.appSettings);

            Type type = typeof(UserService);
            var userService = Activator.CreateInstance(type, context);

            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Where(x => x.Name == "GenerateHash")
            .First();

            string password = " ";

            //Act
            var exception = Record.Exception(() => (HashSalt)method.Invoke(userService, new object[] { password }));

            //Assert
            Assert.NotNull(exception);
        }


        [Fact]
        public void CreateUserTestPass()
        {
            // Arrange
            var context = new DataContext(this.appSettings);
            var userService = new UserService(this.appSettings);

            var userName = Guid.NewGuid().ToString();

            var user = new User()
            {
                EmailAddress = "dd",
                FirstName = "ff",
                LastName = "gg",
                UserName = userName,
            };

            //Act
            var createdUser = userService.Create(user, "verylongpasswordmustgohere");

            //Assert
            Assert.True(createdUser.Result.Id > 0);

            //CleanUp
            userService.Delete(createdUser.Result.Id);
        }

        [Fact]
        public void AuthenticateUserTestPass()
        {
            // Arrange
            var context = new DataContext(this.appSettings);
            var userService = new UserService(this.appSettings);

            var userName = Guid.NewGuid().ToString();

            var user = new User()
            {
                EmailAddress = "dd",
                FirstName = "ff",
                LastName = "gg",
                UserName = userName,
            };

            //Act
            var createdUser = userService.Create(user, "verylongpasswordmustgohere");
            var authenticatedUser = userService.Authenticate(createdUser.Result.UserName, "verylongpasswordmustgohere");

            //Assert
            Assert.NotNull(authenticatedUser);

            //CleanUp
            userService.Delete(createdUser.Result.Id);

            //var booksService = new BooksService(context);

            //booksService.GenerateSampleBooks();

        }
    }
}
