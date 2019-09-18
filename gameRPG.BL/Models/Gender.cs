using System;

namespace gameRPG.BL.Models
{
    [Serializable]
    public class Gender
    {
        public string Name { get; set; }

        public Gender(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            Name = name;

        }
    }

}