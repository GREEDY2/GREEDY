using GREEDY.DataManagers;
using Xunit;

namespace GREEDY.UnitTests.DataManagersUnitTests
{
    public class UnitTestUserManager
    {
        [Fact]
        public void UserManager_FindByUsername_CaseDoesNotMatch()
        {
            var userManager=new UserManager(DatabaseMock.GetDataBaseMock().Object);
            var user = userManager.FindByUsername("UsErNaMe1");
            Assert.NotNull(user);
        }
        [Fact]
        public void UserManager_FindByEmail_CaseDoesNotMatch()
        {
            var userManager = new UserManager(DatabaseMock.GetDataBaseMock().Object);
            var user = userManager.FindByEmail("EmAil1");
            Assert.NotNull(user);
        }

        [Fact]
        public void UserManager_GetExistingUsers()
        {
            var userManager = new UserManager(DatabaseMock.GetDataBaseMock().Object);
            var users = userManager.GetExistingUsers();
            Assert.Equal(2, users.Count);
        }

        [Fact]
        public void UserManager_FindByEmail_UserDoesNotExist()
        {
            var userManager = new UserManager(DatabaseMock.GetDataBaseMock().Object);
            var user = userManager.FindByEmail("Email that doesn't exist");
            Assert.Null(user);
        }

        [Fact]
        public void UserManager_FindByUsername_UserDoesNotExist()
        {
            var userManager = new UserManager(DatabaseMock.GetDataBaseMock().Object);
            var user = userManager.FindByUsername("Username that doesn't exist");
            Assert.Null(user);
        }
    }
}
