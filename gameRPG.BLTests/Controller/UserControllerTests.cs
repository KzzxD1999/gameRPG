using Microsoft.VisualStudio.TestTools.UnitTesting;
using gameRPG.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gameRPG.BL.Models;

namespace gameRPG.BL.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
 
        [TestMethod()]
        public void SaveTest()
        {
            string userName = Guid.NewGuid().ToString();

            UserController userController = new UserController(userName);

            Assert.AreEqual(userName, userController.CurrentUser.Name);
        }

        [TestMethod()]
        public void SetNewUserDataTest()
        {
            Random random = new Random();
            string userName = Guid.NewGuid().ToString();
            string genderName = Guid.NewGuid().ToString();
            int age = random.Next(100);
            UserController userController = new UserController(userName);
            Gender gender = new Gender(genderName);
           
            userController.SetNewUserData(age, gender.Name);
            Assert.AreEqual(userName, userController.CurrentUser.Name);

            Assert.AreEqual(genderName, gender.Name);
            Assert.AreEqual(age, userController.CurrentUser.Age);
        }

        [TestMethod()]
        public void AddMoneyTest()
        {
            string userName = Guid.NewGuid().ToString();
            Random random = new Random();
            int money = random.Next(10, 100);
            UserController userController = new UserController(userName);
            userController.CurrentUser.Money += money;


            Assert.AreEqual(money, userController.CurrentUser.Money);
            Assert.IsTrue(money == userController.CurrentUser.Money);
        }
    }
}