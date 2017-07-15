using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Capstone.Classes.ItemClasses;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        private Dictionary<string, IVendingMachineItem> items = new Dictionary<string, IVendingMachineItem>();
        private decimal balance;

        public VendingMachine()
        {
            string directory = Environment.CurrentDirectory;
            string file = "vendingmachine.csv";
            balance = 0.00M;
            try
            {
                using (StreamReader sr = new StreamReader(Path.Combine(directory, file)))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] lines = sr.ReadLine().Split('|');

                        switch (lines[0].Substring(0, 1))
                        {
                            case "A":
                                items[lines[0]] = new ChipItem(lines[1], 5, decimal.Parse(lines[2]));
                                break;
                            case "B":
                                items[lines[0]] = new CandyItem(lines[1], 5, decimal.Parse(lines[2]));
                                break;
                            case "C":
                                items[lines[0]] = new DrinkItem(lines[1], 5, decimal.Parse(lines[2]));
                                break;
                            default:
                                items[lines[0]] = new GumItem(lines[1], 5, decimal.Parse(lines[2]));
                                break;
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error\nInventory file not loaded correctly, please check file directory and try again.\nError");

            }
        }

        public decimal Balance
        {
            get
            {
                return balance;
            }
        }

        public Dictionary<string, IVendingMachineItem> Items
        {
            get
            {
                return items;
            }
        }

     
        public decimal AddMoney(decimal dollarAmount)
        {
            Log("FEED MONEY", dollarAmount);
            balance += dollarAmount;

            return balance;
        }

        public decimal TakeMoney(string slotNum, decimal price)
        {
            Log(Items[slotNum].Name, -price);
            balance -= price;
            
            return balance;
        }

        public List<int> DespenseMoney()
        {
            List<int> change = new List<int>();
            List<int> coins = new List<int>() { 25, 10, 05};
            
            decimal pennies = balance * 100;

            for(int i = 0; i < coins.Count; i++)
            {
                change.Add((int)pennies / coins[i]);
                pennies -=  coins[i] * change[i];
            }

            Log("GIVE CHANGE", balance);
            balance = 0;

            return change;
        }

        public void Log(string transactionType, decimal dollarAmount)
        {
            string dir = Environment.CurrentDirectory;
            string file = "log.txt";
            string filePath = Path.Combine(dir, file);

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine("{0} {1}: ${2,-20:0.00}${3:0.00}", DateTime.UtcNow, transactionType, Balance, Balance + dollarAmount);
            }
        }

        //public static void salesreport
        //foreach item in items subtract current value from 5 for quantity sold and multiply difference by cost for sale amount
        //sum sale amounts for total sales

        public void SalesReport()
        { 
            string dir = Environment.CurrentDirectory;
            string file = $"{DateTime.Now.ToString("yyyyMMddTHHmmss")}-Sales-Report.txt";
            string filePath = Path.Combine(dir, file);
            decimal totalSales = 0M;

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (KeyValuePair<string, IVendingMachineItem> kvp in Items)
                {
                    sw.WriteLine($"{kvp.Value.Name}|{5 - kvp.Value.Quantity}");
                    totalSales += (5 - kvp.Value.Quantity) * kvp.Value.Price;
                }
                sw.WriteLine($"\n**TOTAL SALES** ${totalSales}");
            }
        } 
    }
}
