using AutoFixture;
using GREEDY.DataManagers;
using Xunit;

namespace GREEDY.UnitTests.DataManagersUnitTests
{
    public class UnitTest_ShopManager
    {
        Fixture fixture = new Fixture();
        ShopManager shopManager = new ShopManager(DatabaseMock.GetDataBaseMock().Object);

        [Fact]
        public void ShopManager_GetExistingShop()
        {
            var shops = shopManager.GetExistingShops();
            Assert.Equal(2, shops.Count);
        }

        [Fact]
        public void ShopManager_GetAllUserShops()
        {
            var userName = string.Empty;
            var shops = shopManager.GetAllUserShops(userName);
            Assert.Equal(2, shops.Count);
        }

    }
}