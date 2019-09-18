using System;

namespace gameRPG.BL.Models
{
    [Serializable]
    public class Item
    {
        public Item(int id, string name, string category, string userName, double attack, double defence, double hitPoint, double manaPoint, bool isEquipped, double weigth, double price)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentException("message", nameof(category));
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("message", nameof(userName));
            }

            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Category = category;
            UserName = userName;
            Attack = attack;
            Defence = defence;
            HitPoint = hitPoint;
            ManaPoint = manaPoint;
            IsEquipped = isEquipped;
            Weigth = weigth;
            Price = price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Attack { get; set; }
        public double Defence { get; set; }
        public double HitPoint { get; set; }
        public string UserName { get; set; }
        public double ManaPoint { get; set; }
        public bool IsEquipped { get; set; }
        public double Weigth { get; set; }
        public double Price { get; set; }

        

        

        public override string ToString()
        {
            return $"ID: {Id}\n" +
                   $"Назва: {Name}\n" +
                   $"Категорія: {Category}\n" +
                   $"Аттака: {Attack}\n" +
                   $"Захист: {Defence}\n" +
                   $"Здоров'я: {HitPoint}\n" +
                   $"Мана: {ManaPoint}\n" +
                   $"Вага: {Weigth}\n" +
                   $"Ціна: {Price}";
        }
    }
    
}