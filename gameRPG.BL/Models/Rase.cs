using System;
namespace gameRPG.BL.Models
{
    [Serializable]
    public class Rase
    {
        public string Name { get; set; }

        public string[] NameArr { get; set; } = { "Воїн", "Маг", "Розбійник" };

   
        public Rase(int number)
        {

            Name = NameArr[number];
        }
        public Rase()
        {

        }
    }
}