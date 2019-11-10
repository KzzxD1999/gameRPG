using Microsoft.VisualStudio.TestTools.UnitTesting;
using gameRPG.BL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gameRPG.BL.Models;

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
            
            Random random = new Random();
            int id = random.Next(1,100);
            string userName = Guid.NewGuid().ToString();
            string itemName = Guid.NewGuid().ToString();
            string itemCategory = Guid.NewGuid().ToString();
            double attack = random.Next(10, 100);
            double defence = random.Next(10, 100);
            double hitPoint = random.Next(10, 100);
            double manaPoint = random.Next(10, 100);
            double weight = random.Next(100, 500);
            double price = random.Next(50, 250);
            UserController userController = new UserController(userName);
            ItemController itemController = new ItemController(userController.CurrentUser);
            Item item = new Item(id, itemName, itemCategory, userName, attack, defence, hitPoint, manaPoint );
            itemController.Items.Add(item);
            itemController.SaveItems();
            userController.CurrentUser.Weight = 500;
            userController.CurrentUser.MaxWeight = 1000;
            var CurrentItem = itemController.Items.Find(x => x.Id == item.Id && x.UserName == userName);
            userController.CurrentUser.Weight += item.Weigth;
            
            Assert.IsFalse(userController.CurrentUser.Weight >= userController.CurrentUser.MaxWeight);
            Assert.AreEqual(item, CurrentItem);


        }
    }
}