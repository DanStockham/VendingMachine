using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class UserInterface
    {
        private VendingMachine vm;


        public UserInterface(VendingMachine vm)
        {
            this.vm = vm;

        }

        public VendingMachine Vm
        {
            get
            {
                return vm;
            }
        }

        public decimal MoneyFeed()
        {
            Console.WriteLine("How much money in whole dollars would you like to add?");
            decimal dollarAmount = decimal.Parse(Console.ReadLine());
            bool isValid = false;
            while (!isValid)
            {
                if (dollarAmount % 1 == 0)
                {
                    vm.AddMoney(dollarAmount);
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid, whole amount");
                    dollarAmount = decimal.Parse(Console.ReadLine());
                }

            }
            return vm.Balance;
        }

        public bool ProductValidity(string slotNum)
        {
            bool isValid = false;
           
                if (!vm.Items.ContainsKey(slotNum))
                {
                    Console.WriteLine("\nThe item doesn't exist, please select another item or type NO to exit");
                    slotNum = Console.ReadLine().ToUpper();


                }
                else if (vm.Items[slotNum].Quantity == 0)
                {
                    Console.WriteLine("\nThis item is out of stock. Please choose another item or type NO to exit");
                    slotNum = Console.ReadLine().ToUpper();

                }
                else
                {
                    isValid = true;

                }
                if (slotNum == "NO")
                {
                isValid = false;
                  
                }
            return isValid;
        
            
        }

        public void Transaction(decimal currentCredit)
        {
            List<int> change;

            change = vm.DespenseMoney();
            Console.WriteLine($"Your change is: Quarters:{change[0]}, Dimes:{change[1]}, and Nickels:{change[2]}");


        }

        public void Run()
        {

            bool isRunning = true;
            Logo.Display();

            while (isRunning)
            {
                try
                {
                    Console.WriteLine("\n1 - Display Vending Machine Items ");
                    Console.WriteLine("2 - Purchase");
                    Console.WriteLine("3 - Sales Reports");
                    Console.WriteLine("9 - Shutdown");
                    int option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            Console.Clear();

                            Console.WriteLine("Your options are: ");
                            Console.WriteLine(" {0, -5} {1, -22}{2, -15}{3}", "Slot", "Item Name", "$ Price", "Qty.");
                            foreach (KeyValuePair<string, IVendingMachineItem> kvp in vm.Items)
                            {
                                Console.WriteLine(" {0, -5}{1, -25}{2, -14:0.00}{3}", kvp.Key, kvp.Value.Name, kvp.Value.Price, kvp.Value.Quantity);
                            }
                            break;
                        case 2:
                            bool isFinished = false;
                            while (!isFinished)
                            {

                                Console.WriteLine("\nChoose an option below: ");
                                Console.WriteLine();
                                Console.WriteLine("1- Feed Money");
                                Console.WriteLine("2- Select Product(s)");
                                Console.WriteLine("3- Finish Transaction");
                                Console.WriteLine();
                                Console.WriteLine("{0}${1:0.00}", "Your credit balance is: ", vm.Balance);
                                option = int.Parse(Console.ReadLine());

                                if (option == 1)
                                {
                                    MoneyFeed();
                                }
                                else if (option == 2)
                                {
                                    Console.WriteLine("\nPlease enter the slot number of the item you like to purchase: ");
                                    string slotNum = Console.ReadLine().ToUpper();


                                    if (ProductValidity(slotNum))
                                    {
                                        vm.Items[slotNum].Quantity -= 1;
                                    }
                                    else
                                    {
                                        break;
                                    }


                                    if (vm.Balance >= vm.Items[slotNum].Price)
                                    {

                                        vm.TakeMoney(slotNum, vm.Items[slotNum].Price);
                                        Console.WriteLine(vm.Items[slotNum].ToString());

                                    }
                                    else
                                    {
                                        Console.WriteLine("Insignificant funds. Please enter more money");
                                    }
                                }
                                else if (option == 3)
                                {
                                    Transaction(vm.Balance);
                                    isFinished = true;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter valid option: (1,2,3)");
                                }
                            }
                            break;
                        case 3:
                            Console.WriteLine("Sales Report Generated!");
                            vm.SalesReport();
                            break;
                        case 9:
                            isRunning = false;
                            break;
                    }
                }
                catch (FormatException e)
                {

                    Console.WriteLine("\nYour input was invalid. Please try again (1,2,3)");
                    Console.WriteLine();
                }
            }
        }

    }
}

