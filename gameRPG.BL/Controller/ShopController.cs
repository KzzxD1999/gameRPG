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
        public List<Skill> Skills { get; set; }
        public Skill Skill { get; set; }
        public List<Skill> SkillsInShop { get; set; }
        public User CurrentUser { get; set; }
        public Item CurrentItem { get; set; }
        public const string ITEMS_FILE_NAME = "items_shop.dat";
        public const string SKILLS_FILE_NAME = "skills_shop.dat";
        public double IncreaseInvetory { get; set; } = 500;
        public ShopController(User user)
        {
           
            Items = GetItemsInShop();
            Skills = GetSkillsInShop();

            CurrentUser = user;
            CurrentUser.Skills = Skills.FindAll(x => x.InShop == true && x.UserName == CurrentUser.Name);
            CurrentUser.Items = Items.FindAll(x => x.IsShop == true && x.UserName == CurrentUser.Name);
            

        }

        public List<Skill> AddSkills(int id)
        {
            List<Skill> skills = null;

            switch (id)
            {
                case 1:
                    skills = new List<Skill>()
                        {
                            new Skill("Удар дракона", "Активний",CurrentUser.Name, 0, 22, 0, 0, 0,20,3, true),
                            new Skill("Підпищення здоров'я", "Активний",CurrentUser.Name, 0,0,0,0,50,35,7, true),


                        };
                    break;
                case 2:
                    skills = new List<Skill>()
                        {
                            //TODO: Заморожувати ворога
                            new Skill("Заморожуючий удар", "Активний",CurrentUser.Name, 9,0,0,0,0,66,7,true ),
                            new Skill("Підпищення здоров'я", "Активний",CurrentUser.Name, 0,0,0,0,50,35,7,true),


                        };
                    break;
                case 3:
                    skills = new List<Skill>()
                        {
                            //TODO: Заморожувати ворога
                            new Skill("Знімання броні", "Активний",CurrentUser.Name, 0,0,-5,-8, 0,0,4,true),
                            new Skill("Підпищення здоров'я", "Активний",CurrentUser.Name, 0,0,0,0,38,20,7,true),


                        };
                    break;
                default:
                    break;
            }
        
            Skills.AddRange(skills);
            SaveSkills();
            return Skills;

        }

        private void SaveSkills()
        {
            Save(SKILLS_FILE_NAME, Skills);
        }
        
        private List<Item> GetItemsInShop()
        {
            return Load<List<Item>>(ITEMS_FILE_NAME) ?? new List<Item>();
           
        }
        private List<Skill> GetSkillsInShop()
        {
            return Load<List<Skill>>(SKILLS_FILE_NAME) ?? new List<Skill>();
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
                CurrentItem = Items.Find(x => x.Id == id && x.UserName == CurrentUser.Name);
     
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
        public void Save()
        {
            Save(ITEMS_FILE_NAME, Items);
            
        }

        //TODO: Зробити продаж вмінь.
        public List<Item> AddItem(int id)
        {
            List<Item> items = null;
            switch (id)
            {
                case 1:
                    items = new List<Item>()
                    {
                         new Item(3,"Стальна вуаль", "Шлем", CurrentUser.Name, 86, 0, 2, 8, 18, 98, false, 164, 183, true),
                         new Item(4, "Корона леорiка", "Шлем", CurrentUser.Name, 111, 0, 0, 15, 34, 135, false, 450, 289,true),
                         new Item(5, "Зазубренный меч Гризвольда", "Меч", CurrentUser.Name, 98, 32, 18, 2, 0, 78, false, 220, 211,true)

                    };
                    break;
                case 2:
                    items = new List<Item>()
                    {
                         new Item(3,"Звездный огонь", "Посох", CurrentUser.Name, 8, 0, 33, 2, 11, 120, false, 86, 141, true),
                         new Item(4, "Феска", "Шлем", CurrentUser.Name, 0, 15, 15, 32, 21, 144, false, 148, 250,true),
                         new Item(5, "Грубые сыромятные штаны", "Штани", CurrentUser.Name, 0, 17, 2, 26, 11, 99, false, 114, 111,true)

                    };
                    break;
                case 3:
                    items = new List<Item>()
                    {
                        new Item(3, "Секатор", "Кинжали", CurrentUser.Name, 38, 1, 0, 0, 6, 80, false, 104, 191, true),
                         new Item(4, "Корона нежити", "Шлем", CurrentUser.Name, 0, 18, 0, 14, 18, 64, false, 110, 200, true),
                         new Item(5, "Кожаные штаны", "Штани", CurrentUser.Name, 0, 22, 1, 12, 14, 46, false, 99, 178, true)

                    };
                    break;
            }
            //TODO: Додати предмети для всіх класів + хіли.
            Items.AddRange(items);
            Save();
            return Items;
        }

    }
}
