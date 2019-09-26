using gameRPG.BL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace gameRPG.BL.Controller
{
    public class ShopController : BaseContoller
    {

        public List<Item> Items { get; set; }
        public User CurrentUser { get; set; }
        public Item CurrentItem { get; set; }
        public const string FILE_NAME = "shop.dat";
        public double IncreaseInvetory { get; set; } = 500;
        public ShopController(User user)
        {
            Items = GetItemsInShop();
            CurrentUser = user;
            if (Items.Count <= 0)
            {
                AddItem();
            }
        }

        private List<Item> GetItemsInShop()
        {
            return Load<List<Item>>(FILE_NAME) ?? new List<Item>();
           
        }
        public void IncreaseInventory()
        {
            UserController userController = new UserController(CurrentUser.Name);
            if(!(userController.CurrentUser.MaxWeight >= 10000) && userController.CurrentUser.Money > userController.CurrentUser.InvetoryPrice)
                {
                InvreaseInvetoryWeight(userController);
       
                Messages($"Інвентар успішно розширений і його максимальна вага становить: {userController.CurrentUser.MaxWeight}", true);
            }else
            {
                Messages($"Недостатнь коштів на рахунку: {CurrentUser.Money} , " +
                         $"або у вас вже максимальна вага: {CurrentUser.MaxWeight} !", false);
            }
            
        }

        private void InvreaseInvetoryWeight(UserController userController)
        {
            userController.CurrentUser.Money -= userController.CurrentUser.InvetoryPrice;
            userController.CurrentUser.MaxWeight += IncreaseInvetory;
            userController.CurrentUser.InvetoryPrice *= 2;
            userController.Save();
        }

        public void BuyItem(int id)
        {
            UserController userController = new UserController(CurrentUser.Name);
            ItemController itemController = new ItemController(CurrentUser);
            while (true)
            {
                CurrentItem = Items.Find(x => x.Id == id);
     
                if(CurrentItem != null)
                {
                    CurrentItem.UserName = CurrentUser.Name;
                    if (CurrentItem.Price > CurrentUser.Money)
                    {

                        Messages($"Недостатнь коштів на рахунку: {CurrentUser.Money}", false);
                    }
                    else
                    {
                        CurrentUser.Weight += CurrentItem.Weigth;
                        if ( !(CurrentUser.Weight >= userController.CurrentUser.MaxWeight))
                        {
                            userController.CurrentUser.Money -= CurrentItem.Price;
                            userController.CurrentUser.Weight += CurrentItem.Weigth;
                            itemController.Items.Add(CurrentItem);
                            itemController.SaveItems();
                            userController.Save();
                            Messages($"Ви успішно купили: {CurrentItem.Name}",true);
                            break;
                        }
                        else
                        {
                            Messages("Вага предметів в вашому інветарі більша або рівна за вашу норму, " +
                                     "ви не можете купити предмет!", false);
                            break;
                        }
                        
                    }
                }
            }
        }
        private void Save()
        {
            Save(FILE_NAME, Items);
            
        }

        //TODO: Зробити продаж вмінь.
        private void AddItem()
        {
            //TODO: Додати предмети для всіх класів + хіли.
            List<Item> items = new List<Item>() {
                new Item(3,"Стальна вуаль", "Шлем", CurrentUser.Name, 86, 0, 2, 8, 18, 98, false, 164, 183),
                new Item(4, "Корона леорiка", "Шлем", CurrentUser.Name, 111, 0, 0, 15, 34, 135, false, 450, 289),
                new Item(5, "Зазубренный меч Гризвольда", "Меч", CurrentUser.Name, 98, 32, 18, 2, 0, 78, false, 220, 211)
            };
            Items.AddRange(items);
            Save();
        }

    }
}
