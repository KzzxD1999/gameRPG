using gameRPG.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameRPG.BL.Controller
{
    public class BattleController : BaseContoller
    {
        public User CurrentUser { get; set; }
        public Boss CurrentBoss { get; set; }
        public double BossHpAndDef { get; set; }
        public double UserHpAndDef { get; set; }
        public BattleController(User user, Boss boss)
        {
            CurrentUser = user;
            CurrentBoss = boss;
            //TODO: Як враховувати броню ??
            BossHpAndDef = CurrentBoss.HitPoint + (CurrentBoss.Defence * 0.2);
            UserHpAndDef = CurrentUser.HitPoint + (CurrentUser.Defence * 0.3);
        }
        public BattleController()
        {

        }

  

        public void Attack()
        {
            BossHpAndDef -= CurrentUser.Attack;
            UserHpAndDef -= CurrentBoss.Attack;
        }

        public void DropedExpAndItem()
        {
            //TODO: ПРИДУМАТИ ЯК ПІДСИЛЮВАТИ БОСА;
            UserController userController = new UserController(CurrentUser.Name);
            ItemController itemController = new ItemController(CurrentUser);
            BossController bossController = new BossController(CurrentBoss);
            
            SaveUser(userController, itemController);
            SaveBoss(bossController);
            Messages($"Ви отримали: {CurrentBoss.MoneyDrop} - грошей, {CurrentBoss.DropExp} - досвіду", true);
            if (CurrentBoss.Items != null)
            {
                foreach (var item in CurrentBoss.Items)
                {
                    Messages($"Предмет: {item.Name} додано до вашого інвентаря", true);
                }
            }
        }

        private void SaveUser(UserController userController, ItemController itemController)
        {
            userController.CurrentUser.Experience += CurrentBoss.DropExp;
            userController.CurrentUser.Money += CurrentBoss.MoneyDrop;
            if (CurrentBoss.Items != null)
            {
                userController.CurrentUser.Items.AddRange(CurrentBoss.Items);
                itemController.SaveItems();

            }
            userController.Save();
        }

        private void SaveBoss(BossController bossController)
        {
            //TODO: Придумати як оновлювати боса
            Random random = new Random();
            int attack = random.Next(2, 6);
            int hitPoint = random.Next(10, 15);
            int defence = random.Next(4, 10);
            int dropExp = random.Next(100, 220);
            int dropMoney = random.Next(150, 250);
            bossController.CurrentBoss.Attack += attack;
            bossController.CurrentBoss.HitPoint += hitPoint;
            bossController.CurrentBoss.Defence += defence;
            bossController.CurrentBoss.DropExp += dropExp;
            bossController.CurrentBoss.MoneyDrop += dropMoney;
            bossController.Update(CurrentBoss);
        }
    }
}
