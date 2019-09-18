using Microsoft.VisualStudio.TestTools.UnitTesting;
using gameRPG.BL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameRPG.BL.Controller.Tests
{
    [TestClass()]
    public class ShopControllerTests
    {
       
        [TestMethod()]
        public void IncreaseInventoryTest()
        {
            double IncreaseInventory = 500;
   
            string userName = Guid.NewGuid().ToString();
            UserController userController = new UserController(userName);
            int mon = 10000;
            userController.CurrentUser.Money = mon;
            var allMoney = userController.CurrentUser.Money;
            allMoney -= userController.CurrentUser.InvetoryPrice;
            var som = userController.CurrentUser.MaxWeight += IncreaseInventory;
            var inc2 = IncreaseInventory * 2;
            Assert.AreNotEqual(IncreaseInventory, inc2);
            Assert.AreEqual(userController.CurrentUser.Money, mon);
            Assert.AreNotEqual(userController.CurrentUser.Money, allMoney);
            Assert.AreEqual(som, userController.CurrentUser.MaxWeight);
            Assert.IsTrue(!(userController.CurrentUser.MaxWeight >= 10000) && userController.CurrentUser.Money > userController.CurrentUser.InvetoryPrice);
        }

        [TestMethod()]
        public void BuyItemTest()
        {
            Assert.Fail();
        }
    }
}