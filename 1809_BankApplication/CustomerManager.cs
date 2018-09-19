using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    class CustomerManager {
        public List<Customer> Customers { get; } = new List<Customer>();



        public Bank MyBank { get; }
        public CustomerManager (Bank bank) {
            MyBank = bank;
        }
        internal void LoadCustomers() {
            List<string[]> customerInfo = DatabaseManager.LoadCustomers();
            foreach (string[] info in customerInfo) {
                Customers.Add(new Customer() {
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
            foreach (Customer customer in Customers) {
                outputString += $"{ customer.AsString }\n";
            }
            return outputString;
        }

        public Customer GetCustomerById(int inputId) {
            foreach (Customer customer in Customers) {
                if (customer.ID == inputId) {
                    return customer;
                }
            }
            return null;
        }

        public List<Customer> SearchByNameOrCity(string searchString) {
            List<Customer> returnList = new List<Customer>();
            foreach (Customer customer in Customers) {
                if (Regex.IsMatch(customer.Name.ToUpper() + " " + customer.City.ToUpper(), $"{ searchString}")) {
                    returnList.Add(customer);
                }
            }
            return returnList;
        }

        public Customer CreateCustomer()
        {
            Customer newCustomer = new Customer();

            int currentMaxId = 0000;
            foreach (Customer customer in Customers) {
                if (customer.ID > currentMaxId) {
                    currentMaxId = customer.ID;
                }
            }
            newCustomer.ID = currentMaxId + 1;
            Customers.Add(newCustomer);
            return newCustomer;
        }

        internal void DeleteCustomer(int inputDeleteId) {
            var customerAccounts = AccountManager.instance.GetAccountsByCustomerId(inputDeleteId);
            if (customerAccounts.Count == 0) {
                Customers.Remove(GetCustomerById(inputDeleteId));
            }
            else {
                Console.WriteLine("Customer still has accounts - Can not delete customer.");
            }
        }
    }
}
