using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    class CustomerManager {
        private List<Customer> customers = new List<Customer>();

        internal void LoadCustomers() {
            List<string[]> customerInfo = Database.LoadCustomers();
            foreach (string[] info in customerInfo) {
                customers.Add(new Customer() {
                    ID = int.Parse(info[0]),
                    OrgNumber = long.Parse(Regex.Replace(info[1], @"[^\d]", "")),
                    Name = info[2],
                    Adress = info[3],
                    City = info[4],
                    Region = info[5],
                    PostalCode = info[6],
                    Country = info[7],
                    PhoneNumber = info[8]
                });

            }
        }
        public string CustomersAsString() {
            string outputString = "";
            foreach (Customer customer in customers) {
                outputString += $"{ customer.AsString }\n";
            }
            return outputString;
        }

        public Customer GetCustomerByID(int inputID) {
            foreach (Customer customer in customers) {
                if (customer.ID == inputID) {
                    return customer;
                }
            }
            return null;
        }

        public List<Customer> SearchByNameOrCity(string searchString) {
            List<Customer> returnList = new List<Customer>();
            foreach (Customer customer in customers) {
                if (Regex.IsMatch(customer.Name.ToUpper() + " " + customer.City.ToUpper(), $"{ searchString}")) {
                    returnList.Add(customer);
                }
            }
            return returnList;
        }

        public void CreateCustomerFromQueries() {
            Customer newCustomer = new Customer();

            int currentMaxId = 0000;
            foreach (Customer customer in customers) {
                if (customer.ID > currentMaxId) {
                    currentMaxId = customer.ID;
                }
            }
            newCustomer.ID = currentMaxId + 1;

            Console.WriteLine("Creating new customer.");

            Console.Write("Input name: ");
            newCustomer.Name = Console.ReadLine();
            Console.Write("\nInput organization number: ");
            newCustomer.OrgNumber = long.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            Console.Write("\nInput street and number: ");
            newCustomer.Adress = Console.ReadLine();
            Console.Write("\nInput city: ");
            newCustomer.City = Console.ReadLine();
            Console.Write("\nInput region: ");
            newCustomer.Region = Console.ReadLine();
            Console.Write("\nInput postal Code: ");
            newCustomer.PostalCode = Console.ReadLine();
            Console.Write("\nInput country: ");
            newCustomer.Country = Console.ReadLine();
            Console.Write("\nInput phone number: ");
            newCustomer.PhoneNumber = Console.ReadLine();

            Program.accountManager.AddAccount(newCustomer.ID);

            customers.Add(newCustomer);
        }

        internal void DeleteCustomer(int inputDeleteId) {
            var customerAccounts = AccountManager.instance.GetAccountsByCustomerID(inputDeleteId);
            if (customerAccounts.Count == 0) {
                customers.Remove(GetCustomerByID(inputDeleteId));
            }
            else {
                Console.WriteLine("Customer still has accounts - Can not delete customer.");
            }
        }
    }
}
