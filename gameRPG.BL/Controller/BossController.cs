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
    public class BossController : BaseContoller
    {
        private const string FILE_NAME = "bosses.dat";
        public Boss CurrentBoss { get; set; }
        public User CurrentUser { get; set; }
        public List<Boss> Bosses { get; set; }
        public BossController(User user)
        {
            CurrentUser = user;
            Bosses = GetBosses();
            //if(GetUserBosses(CurrentUser).Count <= 0)
            //{
            //    AddBosess();
            //}
            
            CurrentUser.Bosses = GetUserBosses(CurrentUser);
             
        }
        public BossController(Boss boss)
        {
            Bosses = GetBosses();
            CurrentBoss = boss;
            
        }
       
        public Boss FindBossById(int id, User user)
        {
          
            Bosses.FirstOrDefault(x => x.Id == id && x.UserName == user.Name);
            if(Bosses.Count < 0)
            {
                Console.WriteLine("Не правильно введено ID");
            }
            else
            {
                return Bosses.FirstOrDefault(x => x.Id == id && x.UserName == user.Name);
            }
            return CurrentBoss;
        }
        private List<Boss> GetUserBosses(User user)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream file = new FileStream(FILE_NAME, FileMode.OpenOrCreate)) 
            {
                if(file.Length>0 && binaryFormatter.Deserialize(file) is List<Boss> bosses)
                {
                    return bosses.FindAll(x => x.UserName == user.Name);
                }
                else
                {
                    return new List<Boss>();
                }
            }
        }

        private List<Boss> GetBosses()
        {
            return Load<List<Boss>>(FILE_NAME) ?? new List<Boss>();
        }

        public List<Boss> AddBosess()
        {
            Random random = new Random();
            int bossMoneyOne = random.Next(150, 220);
            int bossMoneyTwo = random.Next(450, 650);
            int bossMoneyThree = random.Next(850, 990);


            List<Boss> bosses = new List<Boss>()
            {


                new Boss(1,"Дракон", CurrentUser.Name, 150, 500, 8, 24, 5,500, bossMoneyOne, null),

                new Boss(2,"Відьма",CurrentUser.Name, 340, 900, 32, 44, 10 , 1100, bossMoneyTwo, null),

                new Boss(3,"Саурон", CurrentUser.Name, 560, 1500, 58, 450, 18, 2400, bossMoneyThree, null)
            };
            Bosses.AddRange(bosses);
            Save();
            return Bosses;
        }

        public void Save()
        {
            Save(FILE_NAME, Bosses);
        }
        public void Update(Boss boss)
        {
            CurrentBoss = Bosses.FirstOrDefault(x => x.Id == boss.Id && x.UserName == boss.UserName);
            CurrentBoss.Attack = boss.Attack;
            CurrentBoss.Defence = boss.Defence;
            CurrentBoss.DropExp = boss.DropExp;
            CurrentBoss.MoneyDrop = boss.MoneyDrop;
            CurrentBoss.HitPoint = boss.HitPoint;
            Save();
        }
    }
}
