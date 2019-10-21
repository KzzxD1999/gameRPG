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
        public List<Skill> SkillsInShop { get; set; }   
        public bool IsNew { get; set; } = false;
        public UserController(string name)
        {
            Users = GetUsers();

            CurrentUser = Users.SingleOrDefault(x => x.Name == name);
            if (CurrentUser == null)
            {
                
     
                CurrentUser = new User(name);
               
                Users.Add(CurrentUser);
                CurrentUser.Rase = new Rase();
                CurrentUser.Skills = new List<Skill>();
                IsNew = true;
                Save();

            }
         
            CurrentWeight = CurrentUser.Weight;
        }

        public void Save()
        {
            LvlUp();
            Save(FILE_NAME, Users);
          
        }

        private List<User> GetUsers()
        {
            return Load<List<User>>(FILE_NAME) ?? new List<User>();
            
        }

        public void SetNewUserData(int age, string genderName, int raceId, List<Skill> skills = null, List<Item> items = null, List<Item>itemsInShop =null, List<Skill> skillsInShop=null,  List<Boss> bosses = null)
        {
            ItemController itemController = new ItemController(CurrentUser);
            BossController bossController = new BossController(CurrentUser);
            SkillController skillController = new SkillController(CurrentUser);
            ShopController shopController = new ShopController(CurrentUser);
            CurrentUser.Age = age;
            CurrentUser.Gender = new Gender(RenameGender(genderName));
            items = itemController.DefaultItems(CurrentUser.Name, raceId);
            skills = skillController.AddSkills(raceId);
            CurrentUser.Items = items;
            CurrentUser.Rase = new Rase(raceId);
            CurrentUser.Skills = skills;
            itemsInShop = shopController.AddItem(raceId);
            skillsInShop = shopController.AddSkills(raceId);
            SetValueForRace(raceId);
            bosses = bossController.AddBosess();
            CurrentUser.Bosses = bosses;
            itemController.SaveItems();
            bossController.Save();
            Save();

        }

        private void SetValueForRace(int raceId)
        {
            switch (raceId)
            {

                case 1:
                    CurrentUser.Attack = 11;
                    CurrentUser.Defence = 14;
                    CurrentUser.MagicAttack = 5;
                    CurrentUser.MagicDef = 9;
                    CurrentUser.ManaPoint = 100;
                    break;
                case 2:
                    CurrentUser.MagicAttack = 12;
                    CurrentUser.MagicDef = 8;
                    CurrentUser.Attack = 5;
                    CurrentUser.Defence = 6;
                    CurrentUser.ManaPoint = 200;
                    break;
                case 3:
                    CurrentUser.Attack = 17;
                    CurrentUser.Defence = 7;
                    CurrentUser.MagicAttack = 5;
                    CurrentUser.MagicDef = 4;
                    CurrentUser.ManaPoint = 100;
                    break;
            }
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
        public void LvlUp()
        {
            //TODO: Придумати, як піднімати стати персонажу, після підвищення рівня.
      
            if(CurrentUser.Experience >= CurrentUser.ExpToUp)
            {

                double remainderOfExp = CurrentUser.Experience % CurrentUser.ExpToUp;
                CurrentUser.Level += 1;
                switch (CurrentUser.Rase.Id)
                {
                    case 1:
                        CurrentUser.HitPoint += Math.Round((CurrentUser.HitPoint * CurrentUser.Level) / (8.6 + CurrentUser.Level));
                        CurrentUser.Attack += Math.Round((CurrentUser.Attack * CurrentUser.Level) / (11.8 + CurrentUser.Level));
                        CurrentUser.Defence += Math.Round((CurrentUser.Defence * CurrentUser.Level) / (9.9 + CurrentUser.Level));
                        CurrentUser.MagicAttack += Math.Round((CurrentUser.MagicAttack * CurrentUser.Level) / (13.8 + CurrentUser.Level));
                        CurrentUser.MagicDef += Math.Round((CurrentUser.MagicDef * CurrentUser.Level) / (9.9 + CurrentUser.Level));
                        CurrentUser.ManaPoint += Math.Round((CurrentUser.ManaPoint * CurrentUser.Level) / (8.8 + CurrentUser.Level));
                        break;
                    case 2:
                        CurrentUser.HitPoint += Math.Round((CurrentUser.HitPoint * CurrentUser.Level) / (11.8 + CurrentUser.Level));
                        CurrentUser.Attack += Math.Round((CurrentUser.Attack * CurrentUser.Level) / (13.8 + CurrentUser.Level));
                        CurrentUser.Defence += Math.Round((CurrentUser.Defence * CurrentUser.Level) / (10.9 + CurrentUser.Level));
                        CurrentUser.MagicAttack += Math.Round((CurrentUser.MagicAttack * CurrentUser.Level) / (7.4 + CurrentUser.Level));
                        CurrentUser.MagicDef += Math.Round((CurrentUser.MagicDef * CurrentUser.Level) / (6.9 + CurrentUser.Level));
                        CurrentUser.ManaPoint += Math.Round((CurrentUser.ManaPoint * CurrentUser.Level) / (4.8 + CurrentUser.Level));
                        break;
                    case 3:
                        CurrentUser.HitPoint += Math.Round((CurrentUser.HitPoint * CurrentUser.Level) / (4.7 + CurrentUser.Level));
                        CurrentUser.Attack += Math.Round((CurrentUser.Attack * CurrentUser.Level) / (6.6 + CurrentUser.Level));
                        CurrentUser.Defence += Math.Round((CurrentUser.Defence * CurrentUser.Level) / (10.1 + CurrentUser.Level));
                        CurrentUser.MagicAttack += Math.Round((CurrentUser.MagicAttack * CurrentUser.Level) / (13.4 + CurrentUser.Level));
                        CurrentUser.MagicDef += Math.Round((CurrentUser.MagicDef * CurrentUser.Level) / (12.9 + CurrentUser.Level));
                        CurrentUser.ManaPoint += Math.Round((CurrentUser.ManaPoint * CurrentUser.Level) / (11.8 + CurrentUser.Level));
                        break;
                    default:
                        break;
                }
               

                CurrentUser.ExpToUp += CurrentUser.ExpToUp * 1.2;
                



                CurrentUser.ExpToUp -= remainderOfExp;
                CurrentUser.Experience = 0;
            }
        }
       
    }
}
