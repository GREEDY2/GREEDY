using GREEDY.DataManagers;
using Xunit;

namespace GREEDY.UnitTests.DataManagersUnitTests
{
    public class UserManagerTest
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void UserManager_FindByUsername_CaseDoesNotMatch(bool logToFb)
        {
            var userManager = new UserManager(DatabaseMock.GetDataBaseMock().Object);
            var user = userManager.FindByUsername("UsErNaMe1", logToFb);
            Assert.NotNull(user);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void UserManager_FindByEmail_CaseDoesNotMatch(bool logToFb)
        {
            var userManager = new UserManager(DatabaseMock.GetDataBaseMock().Object);
            var user = userManager.FindByEmail("EmAil1", logToFb);
            Assert.NotNull(user);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void UserManager_FindByEmail_UserDoesNotExist(bool logToFb)
        {
            var userManager = new UserManager(DatabaseMock.GetDataBaseMock().Object);
            var user = userManager.FindByEmail("Email that doesn't exist", logToFb);
            Assert.Null(user);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void UserManager_FindByUsername_UserDoesNotExist(bool logToFb)
        {
            var userManager = new UserManager(DatabaseMock.GetDataBaseMock().Object);
            var user = userManager.FindByUsername("Username that doesn't exist", logToFb);
            Assert.Null(user);
        }

        [Fact]
        public void UserManager_GetExistingUsers()
        {
            var userManager = new UserManager(DatabaseMock.GetDataBaseMock().Object);
            var users = userManager.GetExistingUsers();
            Assert.Equal(2, users.Count);
        }
    }
}