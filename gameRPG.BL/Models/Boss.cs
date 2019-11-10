using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameRPG.BL.Models
{
    [Serializable]
    public class Boss
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double HitPoint { get; set; }
        public double ManaPoint { get; set; }
        public double Attack { get; set; }
        public double Defence { get; set; }
        public string UserName { get; set; }
        public int Level { get; set; }
        public double DropExp { get; set; }
        public double CriticalChance { get; set; }
        public double CriticalAttack { get; set; }
        public double MoneyDrop { get; set; }
        public List<Item> Items { get; set; }
        public bool IsStun { get; set; } = false;
        public int StunningTime { get; set; }

        public Boss(int id, string name, string userName, double hitPoint, double manaPoint, double attack, double defence, int level, double dropExp, double moneyDrop, List<Item> items, double criticalChance, double criticalAttack)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("message", nameof(name));
            }


            Id = id;
            Name = name;
            UserName = userName;
            HitPoint = hitPoint;
            ManaPoint = manaPoint;
            Attack = attack;
            Defence = defence;
            Level = level;
            DropExp = dropExp;
            MoneyDrop = moneyDrop;
            Items = items;
            CriticalChance = criticalChance;
            CriticalAttack = criticalAttack;
        }
    }

}
