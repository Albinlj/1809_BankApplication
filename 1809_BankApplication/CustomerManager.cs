using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1809_BankApplication {
    class CustomerManager {
        private List<Customer> customers = new List<Customer>();

        internal void LoadCustomers() {
            customers = FileManager.LoadCustomers();
        }


    }
}
