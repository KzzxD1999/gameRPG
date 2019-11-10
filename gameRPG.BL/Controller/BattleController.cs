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
        public bool IsExit { get; set; } = false;
        public bool IsWin { get; set; } = false;
        public Skill CurrentSkill { get; set; } = null;
        public List<Skill> Skills { get; set; }
        
        public BattleController(User user, Boss boss)
        {
            CurrentUser = user;
            CurrentBoss = boss;

            Skills = CurrentUser.Skills.ToList();


            //TODO: Як враховувати броню ?? перевірка значень тому що вилітає програма
            BossHpAndDef = Math.Round(CurrentBoss.HitPoint + (CurrentBoss.Defence * 0.1), 2);
            UserHpAndDef = Math.Round(CurrentUser.HitPoint + (CurrentUser.Defence * 0.9), 2);
           
        }
       

  
        //TODO: Додати користувачу по замовчуванню декілька аптечок, для можливості відхілу
        public void Attack()
        {

            Random random = new Random();
            var forUser = random.NextDouble();
            var forBoss = random.NextDouble();
            bool isUserCritical = false;
            bool isBossCritical = false;

            if (CurrentUser.ChanceCriticalAttack > forUser)
            {
                BossHpAndDef -= CurrentUser.CriticalAttack;
                UserHpAndDef -= CurrentBoss.Attack;
                Messages("Кріт урон", true);
                isUserCritical = true;
            }
            if (CurrentBoss.CriticalChance > forBoss)
            {
                BossHpAndDef -= CurrentUser.Attack;
                if (!CurrentBoss.IsStun)
                {

                    UserHpAndDef -= CurrentBoss.CriticalAttack;
                    Messages("Кріт урон від боса", false);
                }
                else
                {
                    CurrentBoss.StunningTime -= 1;
                    if (CurrentBoss.StunningTime == 0)
                    {
                        CurrentBoss.IsStun = false;
                    }
                }
                isBossCritical = true;
            }

            if (!isUserCritical)
            {
                BossHpAndDef -= CurrentUser.Attack;
            }
            if (!isBossCritical)
            {
                if (!CurrentBoss.IsStun)
                {
                    UserHpAndDef -= CurrentBoss.Attack;
                }
                else
                {
                    CurrentBoss.StunningTime -= 1;
                    if (CurrentBoss.StunningTime == 0)
                    {
                        CurrentBoss.IsStun = false;
                    }
                }
            }
            Math.Round(UserHpAndDef, 2);
            Math.Round(BossHpAndDef, 2);
            if (CurrentSkill != null && CurrentSkill.IsRecharge)
            {
                CurrentSkill.Recharge -= 1;
                if (CurrentSkill.Recharge ==0)
                {
                    CurrentSkill.IsRecharge = false;
                }
                UserController userController = new UserController(CurrentUser.Name);
                userController.Save();
            }
            if (UserHpAndDef <= 0)
            {
                UserController userController = new UserController(CurrentUser.Name);
                ItemController itemController = new ItemController(CurrentUser);

                Messages($"Користувач: {CurrentUser.Name} програв", false);
                Messages($"Ви нічого не отримали", false);
                SaveUser(userController, itemController);
                IsExit = true;
               
            }
            else if (BossHpAndDef <= 0)
            {

                Messages($"Бос: {CurrentBoss.Name} програв", true);
                IsWin = true;
                IsExit = true;

                DropedExpAndItem();
                
            }
        }

        public void DropedExpAndItem()
        {
           
            UserController userController = new UserController(CurrentUser.Name);
            ItemController itemController = new ItemController(CurrentUser);
            BossController bossController = new BossController(CurrentBoss);
            Messages($"Ви отримали: {bossController.CurrentBoss.MoneyDrop} - грошей, {CurrentBoss.DropExp} - досвіду", true);

            SaveUser(userController, itemController);
            SaveBoss(bossController);
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
            if (IsWin)
            {
                userController.CurrentUser.Win += 1;
            }
            else
            {
                userController.CurrentUser.Loss += 1;
            }
            if (CurrentBoss.Items != null)
            {
                userController.CurrentUser.Items.AddRange(CurrentBoss.Items);
                itemController.SaveItems();

            }
            userController.Save();
        }

        private void SaveBoss(BossController bossController)
        {
         
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

        public void EarlyCompletion()
        {
            UserController userController = new UserController(CurrentUser.Name);
            userController.CurrentUser.Loss += 1;
            userController.Save();
            Messages($"Користувач {CurrentUser.Name} програв", false);
            

        }

        public void MagicAttack(int id)
        {
            //CurrentUser.Skills = GetSkills();
            CurrentSkill = CurrentUser.Skills.FirstOrDefault(x => x.Id == id);

            UserController userController = new UserController(CurrentUser.Name);
            //TODO: Подумати тут
            Skill skill1 = new Skill(CurrentSkill.CategoryId);
            var mk = skill1.CategoryDict.FirstOrDefault(x => x.Key == CurrentSkill.CategoryId);
            if (!CurrentSkill.IsRecharge)
            {
                switch (mk.Key)
                {
                    case 1:
                        if (CurrentUser.ManaPoint >= CurrentSkill.ManaPoint)
                        {
                            BossHpAndDef -= CurrentSkill.PhysicalDamage;
                            CurrentSkill.IsRecharge = true;
                            
                        }
                        else
                        {
                            Messages("Недостатньо мани", false);
                        }
                        break;
                    case 2:
                        if (CurrentUser.ManaPoint >= CurrentSkill.ManaPoint)
                        {
                            BossHpAndDef -= (CurrentSkill.MagicDamage + CurrentUser.MagicAttack) + (CurrentUser.MagicAttack * 0.2);
                            CurrentUser.ManaPoint -= CurrentSkill.ManaPoint;
                            CurrentSkill.IsRecharge = true;
                            userController.Save();
                        }
                        else
                        {
                            Messages("Недостатньо мани", false);
                        }
                        break;
                    case 3:
                        if (CurrentUser.ManaPoint >= CurrentSkill.ManaPoint)
                        {

                            UserHpAndDef += CurrentSkill.PhysicalDamage;
                            CurrentSkill.IsRecharge = true;
                        }
                        else
                        {
                            Messages("Недостатнь мани", false);
                        }
                        break;
                    case 4:
                        if (CurrentUser.ManaPoint >= CurrentSkill.ManaPoint)
                        {

                            CurrentBoss.IsStun = true;
                            BossHpAndDef -= CurrentSkill.MagicDamage;
                            CurrentSkill.IsRecharge = true;
                            CurrentBoss.StunningTime = 2;
                        }
                        
                        break;
                    case 5:
                        if (CurrentUser.ManaPoint >= CurrentSkill.ManaPoint)
                        {
                            UserHpAndDef += CurrentSkill.HitPoint;
                            CurrentUser.ManaPoint -= CurrentSkill.ManaPoint;
                            CurrentSkill.IsRecharge = true;
                        }
                        break;
                    case 6:
                        if (CurrentUser.ManaPoint >= CurrentSkill.ManaPoint)
                        {
                            UserHpAndDef += CurrentSkill.MagicDefence;
                            CurrentUser.ManaPoint -= CurrentSkill.ManaPoint;
                            CurrentSkill.IsRecharge = true;
                        }
                        else
                        {
                            Messages("Недостатньо мани", false);
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Messages("Перезарядка", false);
            }
        }
    }
}
