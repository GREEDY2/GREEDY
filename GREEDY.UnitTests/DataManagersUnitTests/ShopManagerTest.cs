using AutoFixture;
using GREEDY.DataManagers;
using Xunit;

namespace GREEDY.UnitTests.DataManagersUnitTests
{
    public class ShopManagerTest
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly ShopManager _shopManager = new ShopManager(DatabaseMock.GetDataBaseMock().Object);

        [Fact]
        public void ShopManager_GetAllNotExistingUserShops()
        {
            var userName = string.Empty;
            var shops = _shopManager.GetAllUserShops(userName);
            Assert.Empty(shops);
        }

        [Fact]
        public void ShopManager_GetAllRealUserShops()
        {
            var shops = _shopManager.GetAllUserShops("username1");
            Assert.Equal(2, shops.Count);
        }

        [Fact]
        public void ShopManager_GetExistingShop()
        {
            var shops = _shopManager.GetExistingShops();
            Assert.Equal(2, shops.Count);
        }
    }
}