using System;
using System.Collections.Generic;

namespace gameRPG.BL.Models
{
    [Serializable]
    public class Rase
    {
        public string Name { get; set; }

        //public string[] NameArr { get; set; } = { "Воїн", "Маг", "Розбійник" };

        public Dictionary<int, string> NameArr = new Dictionary<int, string>()
        {
            {1, "Воїн" },
            {2, "Маг" },
            {3, "Розбійник" },

        };


        public Rase(int number)
        {

            Name = NameArr[number];
        }
        public Rase()
        {

        }
    }
}