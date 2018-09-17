using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    class Customer {
        public int ID { get; private set; }
        public int OrgNumber { get; private set; }
        public string Name { get; private set; }
        public string Adress { get; private set; }
        public string City { get; private set; }
        public string Region { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }
        public string PhoneNumber { get; private set; }

        private List<Account> accounts;

        public List<Account> Accounts {
            get { return accounts; }
            set { accounts = value; }
        }
        public Customer(int _id, int _orgNumber, string _name, string _adress, string _city, string _region, string _postalCode, string _country, string _phoneNumber) {
            ID = _id;
            OrgNumber = _orgNumber;
            Name = _name;
            Adress = _adress;
            City = _city;
            Region = _region;
            PostalCode = _postalCode;
            Country = _country;
            PhoneNumber = _phoneNumber;
        }

    }
}
