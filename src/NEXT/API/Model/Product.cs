using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Model
{
    public class Product
    {
        public string name;
        public int ID;
        public double price;
        public string description;
        public int[] categories;

        public Product(int id, string name, double price, int[] categories)
        {
            this.ID = id;
            this.name = name;
            this.price = price;
            this.categories = categories;
            this.description = "a. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure ";
        }
    }
}
