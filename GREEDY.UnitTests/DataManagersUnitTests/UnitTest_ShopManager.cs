using GREEDY.DataManagers;
using Xunit;

namespace GREEDY.UnitTests.DataManagersUnitTests
{
    public class UnitTest_ShopManager
    {
        [Fact]
        public void ShopManager_GetExistingShop()
        {
            var shopManager = new ShopManager(DatabaseMock.GetDataBaseMock().Object);
            var shops = shopManager.GetExistingShop();
            Assert.Equal(2, shops.Count);
        }
    }
}
