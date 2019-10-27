using System;
using System.Collections.Generic;

namespace gameRPG.BL.Models
{
    [Serializable]
    public class Skill
    {

        public Dictionary<int, string> CategoryDict = new Dictionary<int, string>()
        {
            {1,"Фізичний урон" },
            {2,"Магічний урон" },
            {3, "Фізичний захист" },
            {4, "Дебаф" },
            {5,"Відхіл" },
            {6,"Магічний захист" },
            {7,"Баф" }
        };
        public Skill(int id, string name, string type,string userName, double magicDamage, double physicalDamage, double magicDefence, double phisicalDefence, double hitPoint, double manaPoint, double recharge, bool inShop, int levelToBuy, int price,  int catId )
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            MagicDamage = magicDamage;
            Price = price;
            PhysicalDamage = physicalDamage;
            MagicDefence = magicDefence;
            PhysicalDefence = phisicalDefence;
            HitPoint = hitPoint;
            ManaPoint = manaPoint;
            Recharge = recharge;
            InShop = inShop;
            LevelToBuy = levelToBuy;

            CategoryName = CategoryDict[catId];
            CategoryId = catId;

        }
        public Skill(int id)
        {
            CategoryId = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string UserName { get; set; }
        public double MagicDamage { get; set; }
        public double PhysicalDamage { get; set; }
        public double MagicDefence { get; set; }
        public double PhysicalDefence { get; set; }
        public double HitPoint { get; set; }
        public double ManaPoint { get; set; }
        public double Recharge { get; set; }
        public int Price { get; set; }
        public bool InShop { get; set; }
        public int LevelToBuy { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public bool IsRecharge { get; set; } = false;

    }
}