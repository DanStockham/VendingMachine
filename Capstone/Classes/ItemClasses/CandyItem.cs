using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes.ItemClasses
{
    class CandyItem:IVendingMachineItem
    {
        private string name;
        private int quantity;
        private decimal price;

        public CandyItem(string name, int quantity, decimal price)
        {
            this.name = name;
            this.quantity = quantity;
            this.price = price;
        }

        public string Name
        {
            get
            {
                return name;
            }
                

        }
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }

        }

        public decimal Price
        {
            get
            {
                return price;
            }
        }

        public override string ToString()
        {
            return "Munch Munch, Yum!";
        }
    }
}
