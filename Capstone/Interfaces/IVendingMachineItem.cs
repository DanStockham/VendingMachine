using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    //Could also act as an interface to our child items
    public interface IVendingMachineItem
    {
        string Name
        {
            get;
            
        }
        int Quantity
        {
            get;set;
            
        }

        decimal Price
        {
            get;
        }

        string ToString();
    }
}
