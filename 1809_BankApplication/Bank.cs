namespace _1809_BankApplication {
    class Bank {
        public DatabaseManager DatabaseManager { get; }
        public CustomerManager CustomerManager { get; }
        public TransactionManager TransactionManager { get; }
        public AccountManager AccountManager { get; }

        public Bank() {
            DatabaseManager = new DatabaseManager(this);
            CustomerManager = new CustomerManager(this);
            AccountManager = new AccountManager(this);
            TransactionManager = new TransactionManager(this);
            CustomerManager.LoadCustomers();
            AccountManager.LoadAccounts();
        }
    }
}