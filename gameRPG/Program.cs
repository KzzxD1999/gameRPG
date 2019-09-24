using gameRPG.BL;
using gameRPG.BL.Controller;
using gameRPG.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameRPG
{
    class Program
    {
        private static UserController userController;
        private static ShopController shopController;
        private static string userName; 
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть iм'я персонажа");
            userName = Console.ReadLine();
            userController = new UserController(userName);
            if (userController.IsNew)
            {
                Console.WriteLine("Введіть стать");
                string genderName = Console.ReadLine();
                Console.WriteLine("Введіть вік");
                int age;
                if(int.TryParse(Console.ReadLine(), out age)) { }
                userController.SetNewUserData(age, genderName);
                Console.WriteLine("Успішно створено");
            }
            ShowMenu();


        }

        private static void ShowMenu()
        {
            Console.WriteLine($"Привiт,{userController.CurrentUser.Name}");
            while (true)
            {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("1)Iнформацiя про персонажа");
                Console.WriteLine("2)Iнвентар");
                Console.WriteLine("3)Битва");
                Console.WriteLine("4)Магазин");
                Console.WriteLine("5)Вихiд");
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        GetUserInformation();
                        break;
                    case ConsoleKey.D2:
                        GetInventoryInformation();
                        break;
                    case ConsoleKey.D3:
                        Battle();
                        break;
                    case ConsoleKey.D4:
                        Shop();
                        break;
                    case ConsoleKey.D5:
                        Exit();
                        break;
                    default:
                        GetUserInformation();
                        break;
                }
            }
        }

        private static void Exit()
        {
            Console.Beep();
            Environment.Exit(0);
        }

        private static void Shop()
        {

            UserController userController1 = new UserController(userName);
            while (true)
            {
                
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("1)Показати всi предмети");
                Console.WriteLine("2)Купити предмет");
                Console.WriteLine("3)Деталi про предмет");
                Console.WriteLine($"4)Розширити інвентар на 500, ціна:{userController1.CurrentUser.InvetoryPrice} ");
                Console.WriteLine("5)Вихiд до головного меню");
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        ShowItemsInShop();
                        break;
                    case ConsoleKey.D2:
                        BuyItems();
                        break;
                    case ConsoleKey.D3:
                        ItemDetails();
                        break;
                    case ConsoleKey.D4:
                        IncreaseInventory();
                        ShowMenu();
                        break;
                    case ConsoleKey.D5:
                        ShowMenu();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Виберiть правильний пункт!");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
        }

        private static void IncreaseInventory()
        {
            shopController = new ShopController(userController.CurrentUser);
            shopController.MessagesEventSuccess += MessagesEventSuccess;
            shopController.MessagesEventFail += MessagesEventFail;
            shopController.IncreaseInventory();

        }

        private static void ItemDetails()
        {
            throw new NotImplementedException();
        }

        private static void BuyItems()
        {
            shopController = new ShopController(userController.CurrentUser);
            Console.WriteLine("Введіть ID предмету, який ви хочете купити");
            shopController.MessagesEventSuccess += MessagesEventSuccess;
            shopController.MessagesEventFail += MessagesEventFail;
            shopController.BuyItem(Actions());
        }

        

        private static void ShowItemsInShop()
        {
            Console.WriteLine("-------------------------------------");
            shopController = new ShopController(userController.CurrentUser);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Кiлькiсть предметiв: {shopController.Items.Count()}");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var item in shopController.Items)
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine(item);
                Console.WriteLine("-----------------------");
            }
        }

        private static void Battle()
        {

            while (true)
            {
                Console.WriteLine("1)Показати всіх босів");
                Console.WriteLine("2)Бій з босом");
                Console.WriteLine("3)Вихід");
                var key = Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.D1:
                        ShowAllBosses();
                        break;
                    case ConsoleKey.D2:
                        BattleWithBoss();
                        break;
                    case ConsoleKey.D3:
                        ShowMenu();
                        break;
                    default:
                        ShowAllBosses();
                        break;
                }
            }

           
        }

        private static void BattleWithBoss()
        {
            Console.WriteLine("Введіть ID боса, з яким ви хочете битись!");
            BossController bossController = new BossController(userController.CurrentUser);
            userController = new UserController(userName);
           
            var boss = bossController.FindBossById(Actions());
            BattleController battleController = new BattleController(userController.CurrentUser, boss);
           
            while (true)
            {
                BattleInformation(battleController);

                Console.WriteLine("1)Атака");
                Console.WriteLine("2)Застосувати магію");
                Console.WriteLine("3)Вилікувати");
                Console.WriteLine("4)Вихід");
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        battleController.Attack();
                        break;
                    case ConsoleKey.D4:
                        //TODO: Придумати що робити у випадку, виходу під час бою.
                        ShowMenu();
                        break;
                    default:
                        break;
                }
            }

        }

        private static void BattleInformation(BattleController battleController)
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("Інформація про бій");
            Console.WriteLine($"Користувач: {userController.CurrentUser.Name}");
            Console.WriteLine($"Атака: {userController.CurrentUser.Attack}");
            Console.WriteLine($"Здоров'я + Захист: {battleController.UserHpAndDef}");
            Console.WriteLine($"Захист: {userController.CurrentUser.Defence}");
            Console.WriteLine($"Мана: {userController.CurrentUser.ManaPoint}");
            Console.WriteLine("\t\tПРОТИ");
            Console.WriteLine($"Бос: {battleController.CurrentBoss.Name}");
            Console.WriteLine($"Атака: {battleController.CurrentBoss.Attack}");
            Console.WriteLine($"Здоров'я + Захист: {battleController.BossHpAndDef}");
            Console.WriteLine($"Захист: {battleController.CurrentBoss.Defence}");
            Console.WriteLine($"Мана: {battleController.CurrentBoss.ManaPoint}");
            Console.WriteLine("-----------------------");
            if(battleController.UserHpAndDef <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Користувач: {battleController.CurrentUser.Name} програв");
                Console.WriteLine($"Ви нічого не отримали");
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.White;
            } else if(battleController.BossHpAndDef <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Бос: {battleController.CurrentBoss.Name} програв");
                battleController.DropedExpAndItem();
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static void ApplyHeal()
        {
            throw new NotImplementedException();
        }

        private static void Magic()
        {
            throw new NotImplementedException();
        }

    
       
        private static void ShowAllBosses()
        {
            BossController bossController = new BossController(userController.CurrentUser);
            
            foreach (var item in bossController.CurrentUser.Bosses)
            {
                Console.WriteLine("-----------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"ID: {item.Id}");
                Console.WriteLine($"Ім'я: {item.Name}");
                Console.WriteLine($"Рівень: {item.Level}");
                Console.WriteLine($"Здоров'я: {item.HitPoint}");
                Console.WriteLine($"Мана: {item.ManaPoint}");
                Console.WriteLine($"Атака: {item.Attack}");
                Console.WriteLine($"Захист: {item.Defence}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("-----------------------");
            }
        }

        private static void GetInventoryInformation()
        {
            while (true)
            {
                Console.WriteLine("Виберіть що ви хочете зробити");
                Console.WriteLine("1)Показати всі предмети");
                Console.WriteLine("2)Продати предмет");
                Console.WriteLine("3)Одіти предмет");
                Console.WriteLine("4)Скинути предмет");
                Console.WriteLine("5)Вихід до головного меню");
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        ShowAllInventory();
                        break;
                    case ConsoleKey.D2:
                        SellItem();
                        break;
                    case ConsoleKey.D3:
                        SetItem();
                        break;
                    case ConsoleKey.D4:
                        ResetItem();
                        break;
                    case ConsoleKey.D5:
                        ShowMenu();
                        break;
                    default:
                        ShowAllInventory();
                        break;
                }
            }
        }

        private static void ResetItem()
        {
            ItemController itemController = new ItemController(userController.CurrentUser);
            Console.WriteLine("Введіть ID предмету який ви хочете одіти");
            itemController.MessagesEventSuccess += MessagesEventSuccess;
            itemController.MessagesEventFail += MessagesEventFail;
            itemController.ResetItem(Actions());
        }

        private static int Actions()
        {
            int id;
            if(int.TryParse(Console.ReadLine(),out id))
            {

            }
            else
            {
                Console.WriteLine("Введіть коректний ID");
            }
            return id;
        }

        private static void SetItem()
        {
            ItemController itemController = new ItemController(userController.CurrentUser);
            Console.WriteLine("Введіть ID предмету який ви хочете одіти");
            itemController.MessagesEventSuccess += MessagesEventSuccess;
            itemController.MessagesEventFail += MessagesEventFail;
            itemController.SetItem(Actions());
        }


    
        
       
        private static void SellItem()
        {
            ItemController itemController = new ItemController(userController.CurrentUser);
            Console.WriteLine("Введіть ID предмету, який ви хочете продати");
            itemController.MessagesEventSuccess += MessagesEventSuccess;
            itemController.MessagesEventFail += MessagesEventFail;
            itemController.SellItem(Actions());
       

        }

        private static void MessagesEventFail(object sender, string e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(sender);
            Console.ForegroundColor = ConsoleColor.White;
            
        }

        private static void MessagesEventSuccess(object sender, string e)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(sender);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void ShowAllInventory()
        {
            ItemController itemController = new ItemController(userController.CurrentUser);
            //userController = new UserController(userName);
            foreach (var item in itemController.CurrentUser.Items )
            {
                //InventoryController inventoryController = new InventoryController(userName);
                Console.WriteLine("-----------------------");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"ID: {item.Id}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Iм'я: {item.Name}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Цiна: {item.Price}");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Здоров'я: {item.HitPoint}");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Мана: {item.ManaPoint}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Урон: {item.Attack}");
                Console.WriteLine($"Захист :{item.Defence}");
                if (item.IsEquipped)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Предмет одітий!");

                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine("-----------------------");
            }
        }

        private static void GetUserInformation()
        {
            UserController userController1 = new UserController(userName);
            ItemController itemController = new ItemController(userController1.CurrentUser);
            Console.WriteLine("-------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Рівень: {userController1.CurrentUser.Level} \tДосвід:{userController1.CurrentUser.Experience}");
            Console.WriteLine($"До наступного рівня: {userController1.CurrentUser.ExpToUp - userController1.CurrentUser.Experience} досвіду");
            Console.WriteLine($"Вiк:{userController1.CurrentUser.Age}");
            Console.WriteLine($"Стать:{userController1.CurrentUser.Gender.Name}");
            Console.WriteLine($"Здоров'я:{userController1.CurrentUser.HitPoint}\tМана:{userController1.CurrentUser.ManaPoint}");
            Console.WriteLine($"Атака:{userController1.CurrentUser.Attack}\tЗахист:{userController1.CurrentUser.Defence}");
            Console.WriteLine($"Поточна вага предметів: {userController1.CurrentWeight} \tМаксимальна вага предметів: {userController1.CurrentUser.MaxWeight}");
            if (itemController.CurrentUser.Items != null)
            {
                Console.WriteLine($"Кiлькiсть предметiв в iнвентарi = {userController1.CurrentUser.Items.FindAll(x=>x.UserName == userName).Count()}");
            }
            else
            {
                Console.WriteLine("Iнвентар пустий :(");
            }
            Console.WriteLine($"Грошi: {userController1.CurrentUser.Money}");
            Console.ForegroundColor = ConsoleColor.White;
        }

      
    }
}
