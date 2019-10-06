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
    public class ItemController : BaseContoller
    {
        public List<Item> Items { get; set; }
        public Item CurrentItem { get; set; }
        public User CurrentUser { get; set; }
        public const string FILE_NAME = "items.dat";
        public ItemController(User user)
        {
            CurrentUser = user;
            Items = GetItems();
          
           
          
            //if (Items.Count <=0 )
            //{
            //    DefaultItems(CurrentUser.Name);
            //    SaveItems();
            //}
            CurrentUser.Items = GetCurrentItems(CurrentUser);



        }
        private List<Item> GetItems()
        {
            return Load<List<Item>>(FILE_NAME) ?? new List<Item>();



        }
        private List<Item> GetCurrentItems(User user)
        {
            
            BinaryFormatter binaryFormater = new BinaryFormatter();

            using (FileStream file = new FileStream(FILE_NAME, FileMode.OpenOrCreate))
            {
                if (file.Length > 0 && binaryFormater.Deserialize(file) is List<Item> items)
                {
                    return items.FindAll(x => x.UserName == user.Name);
                }
                else
                {
                    return new List<Item>();
                }
            }


        }
        public List<Item> DefaultItems(string userName, int raceId)
        {
            Item item = null;
            Item item1 = null;
            switch (raceId)
            {   
                case 1:
                    item = new Item(1, "Меч", "Холодна зброя", userName, 14, 0, 3, 0, 4, 0, false, 136, 84, false);
                    item1 = new Item(2, "Щит", "Оборона", userName, 0, 14, 0, 10, 15, 0, false, 194, 111, false);
                    break;
                case 2:
                    item = new Item(1, "Посох", "Магічна зброя", userName, 4, 0, 15, 4, 4, 0, false, 136, 84, false);
                    item1 = new Item(2, "Магічний щит", "Оборона", userName, 0, 6, 0, 13, 15, 0, false, 194, 111,false);
                    break;
                case 3:
                    item = new Item(1, "Кинжал", "Холодна зброя", userName, 22, 2, 2, 0, 10, 0, false, 136, 84,false);
                    item1 = new Item(2, "Шлем", "Оборона", userName, 0, 14, 0, 5, 15, 0, false, 194, 111,false);
                    break;
                default:
                    break;
            }

            CurrentUser.Weight += item.Weigth;
            CurrentUser.Weight += item1.Weigth;
            Items.Add(item);
            Items.Add(item1);
            return Items; 
        }
        public void SaveItems()
        {
            Save(FILE_NAME, Items);
        }
        public void SellItem(int id)
        {
            CurrentItem = Items.Find(x => x.Id == id && x.UserName == CurrentUser.Name);
            CurrentUser.Item = Items.Find(x => x.Id == id && x.UserName == CurrentUser.Name);
            UserController userController = new UserController(CurrentUser.Name);
            if(CurrentItem !=null)
            {
                if (!CurrentItem.IsEquipped)
                {
                    userController.CurrentUser.Money += CurrentItem.Price;
                    userController.CurrentUser.Weight -= CurrentItem.Weigth;
                    Items.Remove(CurrentItem);
                    userController.Save();
                    SaveItems();

                    Messages($"Пердмет: {CurrentItem.Name} продано", true);
                }
                else
                {
                    Messages($"Предмет: {CurrentItem.Name} не може бути проданим до тих пір поки він одітий!", false);
                }
            }
            else
            {
                Messages($"Предмет не існує", false);

            }
        }
        public void SetItem(int id)
        {
            CurrentItem = Items.Find(x => x.Id == id && x.UserName == CurrentUser.Name);
            CurrentUser.Item = Items.Find(x=>x.Id == id && x.UserName == CurrentUser.Name);
            UserController userController = new UserController(CurrentUser.Name);
            //TODO: Подумати над перевіркою значень
            if (CurrentItem != null && !CurrentItem.IsEquipped)
            {
                CurrentItem.IsEquipped = true;


                CurrentUser.Item.IsEquipped = CurrentItem.IsEquipped;
                userController.CurrentUser.HitPoint += CurrentItem.HitPoint;
                userController.CurrentUser.ManaPoint += CurrentItem.ManaPoint;
                userController.CurrentUser.Attack += CurrentItem.Attack;
                userController.CurrentUser.Defence += CurrentItem.Defence;
                userController.Save();
                SaveItems();
                Messages($"Предмет: {CurrentItem.Name} одіто!", true);
            }
            else if(CurrentItem == null)
            {
               
                Messages("Введіть коректний ID", false);
            }
            else
            {
                
                Messages("Предмет вже одітий!", false);
               
            }
        }
        public void ResetItem(int id)
        {
            CurrentItem = Items.Find(x => x.Id == id && x.UserName == CurrentUser.Name);
            CurrentUser.Item = Items.Find(x => x.Id == id && x.UserName == CurrentUser.Name);
            UserController userController = new UserController(CurrentUser.Name);
            if (CurrentItem != null && CurrentItem.IsEquipped)
            {
                CurrentItem.IsEquipped = false;
                CurrentUser.Item.IsEquipped = CurrentItem.IsEquipped;
                userController.CurrentUser.HitPoint -= CurrentItem.HitPoint;
                userController.CurrentUser.ManaPoint -= CurrentItem.ManaPoint;
                userController.CurrentUser.Attack -= CurrentItem.Attack;
                userController.CurrentUser.Defence -= CurrentItem.Defence;
                userController.Save();
                SaveItems();
                
                Messages($"Предмет: {CurrentItem.Name} скинуто!", true);
            }
            else if (CurrentItem == null)
            {
                Messages("Введіть коректний ID", false);
            }
            else
            {
                Messages("Предмет не одіто!", false);
            }
        }
    }
}
