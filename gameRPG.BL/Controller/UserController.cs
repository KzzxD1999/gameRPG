using gameRPG.BL.Controller;
using gameRPG.BL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace gameRPG.BL
{
    public class UserController : BaseContoller
    {
        public List<User> Users { get; set; }
        public User CurrentUser { get; set; }

        public const string FILE_NAME = "users.dat";
        public double CurrentWeight { get; set; }
        public bool IsNew { get; set; } = false;

        public UserController(string name)
        {
            Users = GetUsers();

            CurrentUser = Users.SingleOrDefault(x => x.Name == name);

          

            if (CurrentUser == null)
            {
                CurrentUser = new User(name);
                Users.Add(CurrentUser);
                IsNew = true;
                Save();

            }
            CurrentWeight = CurrentUser.Weight;
        }

        public void Save()
        {
            Save(FILE_NAME, Users);
          
        }

        private List<User> GetUsers()
        {
            return Load<List<User>>(FILE_NAME) ?? new List<User>();
            
        }

        public void SetNewUserData(int age, string genderName, List<Item> items = null)
        {
            ItemController itemController = new ItemController(CurrentUser);
            CurrentUser.Age = age;
            CurrentUser.Gender = new Gender(RenameGender(genderName));
            items = itemController.DefaultItems(CurrentUser.Name);
            CurrentUser.Items = items;
            itemController.SaveItems();
            Save();
           
        }

        private string RenameGender(string name)
        {
            if (name.Equals("M"))
            {
                return "Чолоiчий";
            }else if (name.Equals("W"))
            {
                return "Жiночий";
            }
            else
            {
                return "Не визначено";
            }
        }

        public void AddMoney()
        {
            Random random = new Random();
            double money = random.Next(10, 100);
            CurrentUser.Money += money;
            Console.WriteLine($"Додано: {money}");
            Save();
        }
    }
}
