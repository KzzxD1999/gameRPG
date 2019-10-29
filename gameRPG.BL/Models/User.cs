﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameRPG.BL.Models
{
    [Serializable]
    public class User
    {
      
       //TODO: Критичний удар
        public string Name { get; set; }
        public int Age { get; set; }
        public double HitPoint { get; set; } = 100;
        public double ManaPoint { get; set; }
        public double Attack { get; set; }
        public double MagicAttack { get; set; }
        public double MagicDef { get; set; }
        public Gender Gender { get; set; }
        public double Defence { get; set; }
        public double Money { get; set; } = 0;
        public Item Item { get; set; }
        public List<Item> Items { get; set; }
        public List<Boss> Bosses { get; set; }
        public double Weight { get; set; } = 0;
        public double MaxWeight { get; set; } = 1000.0;
        public double InvetoryPrice { get; set; } = 250.0;
        public int Level { get; set; } = 1;
        public double Experience { get; set; } = 0;
        public double ExpToUp { get; set; } = 50;
        public int Win { get; set; } = 0;
        public int Loss { get; set; } = 0;
        public Rase Rase { get; set; }
        public List<Skill> Skills { get; set; }
        public double ChanceCriticalAttack { get; set; }
        public double CriticalAttack { get; set; }
        public User(string name)
        {
            Name = name;
        }

        public User(string name, int age, Gender gender, double hitPoint, double manaPoint, double attack, double defence, double chanceCriticalAttack, double criticalAttack, List<Item> items)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (gender is null)
            {
                throw new ArgumentNullException(nameof(gender));
            }

            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            Name = name;
            Age = age;
            Gender = gender;
            HitPoint = hitPoint;
            ManaPoint = manaPoint;
            Attack = attack;
            Defence = defence;
            ChanceCriticalAttack = chanceCriticalAttack;
            CriticalAttack = criticalAttack;
            Items = items;
        }
        

    }
}
