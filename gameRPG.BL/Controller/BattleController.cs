﻿using gameRPG.BL.Models;
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
        public BattleController(User user, Boss boss)
        {
            CurrentUser = user;
            CurrentBoss = boss;
            //TODO: Як враховувати броню ?? перевірка значень тому що вилітає програма
            BossHpAndDef = Math.Round(CurrentBoss.HitPoint + (CurrentBoss.Defence * 0.1), 2);
            UserHpAndDef = Math.Round(CurrentUser.HitPoint + (CurrentUser.Defence * 0.9),2);
        }
       

  
        //TODO: Додати користувачу по замовчуванню декілька аптечок, для можливості відхілу
        public void Attack()
        {
            BossHpAndDef -= CurrentUser.Attack;
            UserHpAndDef -= CurrentBoss.Attack;
            Math.Round(UserHpAndDef, 2);
            Math.Round(BossHpAndDef, 2);
            if (UserHpAndDef <= 0)
            {
                
                Messages($"Користувач: {CurrentUser.Name} програв", false);
                Messages($"Ви нічого не отримали", false);
                
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
    }
}
