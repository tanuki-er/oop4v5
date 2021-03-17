using System;
using System.Collections.Generic;
using System.Linq;
using Problem5v13.Account.AccountType;
using Problem5v13.Transactions;

namespace Problem5v13.Account.Bank
{
    public class Bank : BankFrame
    {
        private static Bank _bank;

        private Dictionary<Client, List<Account.AccountType.Account>> Clients;

        //private Map<Client, ArrayList<Account>> clients;
        private Guid BankId { get; }
        private string BankName { get; set; }
        private double Percent { get; set; }//= 0.05;
        private double Commission { get; set; }//= 0.03;
        private double CreditLimit { get; set; } //= 50000.0;
        //private double SusWithdrawLimit = 10000.0;
        //private double SusRemittanceLimit = 5000.0;

        public Bank(string name, double percent, double commission, double creditLimit)
        {
            BankId = Guid.NewGuid();
            BankName = name;
            Percent = percent;
            Commission = commission;
            CreditLimit = creditLimit;
            
            Clients = new Dictionary<Client, List<Account.AccountType.Account>>();
        }

        public static Bank GetBank(string name, double percent, double commission, double creditLimit)
        {
            if (_bank == null)
            {
                _bank = new Bank(name, percent, commission, creditLimit);
                AccountList.Add(Bank.GetBank(name, percent, commission, creditLimit));
            }

            return _bank;
        }

        public override Guid CreateClient(Guid id) => CreateNewClient(id, Clients);
        
        public override Client GetClient(Guid id) => Clients.Keys.FirstOrDefault(client => client.GetId().Equals(id));

        
        public override Guid AddAddress(Guid id, string address) => AddClientAddress(id, address, Clients);

        public override Guid AddPassport(Guid id, string passport) => AddClientPassport(id, passport, Clients);

        
        
        //_______________________________________________________
        public override Guid AddCreditAccount(Guid id) => NewCredit(id, BankId, Clients, CreditLimit, Commission);
        
        public override Guid AddDebitAccount(Guid id) => NewDebit(id, BankId, Clients, Percent);

        public override Guid AddDepositAccount(Guid id, double sum, int tau) => NewDeposit(id, BankId, Clients, sum, tau);


        
        public override Guid GetId()
        {
            return BankId;
        }

        public override double GetStatus(Guid id)
        {
            foreach (Client client in Clients.Keys)
            {
                foreach (AccountType.Account account in Clients[client] )
                {
                    if (account.GetId().Equals(id)) return account.GetStatus();
                }
            }

            return 0;
        }
        
        
        public override bool Withdraw(double sum, Guid id)
        {
            if (WithDraw(id, sum, Clients)) Added.Add(new Transaction(sum, id, id));
            return WithDraw(id, sum, Clients);
        }
        

        public override bool Deposit(double sum, Guid id)
        {
            if (Deposit(id, sum, Clients)) Added.Add(new Transaction(sum, id, id));
            return Deposit(id, sum, Clients);
        }
        

        public override bool Remittance(Guid idFrom, Guid idTo, double sum)
        {
            if (Remittance(idFrom, idTo, sum, Clients)) Added.Add(new Transaction(sum, idFrom, idTo));
            return Remittance(idFrom, idTo, sum, Clients);
        }
//...........................................................................
        
        public override bool Cancel(Guid id, int type) => Cancel(id, BankId, Clients);

        public override double GetCommission() => Commission;

        
        public override void Iterator()
        {
            foreach (var client in Clients.Keys)
            {
                foreach (var account in Clients[client])
                {
                    account.Percent();
                }
            }
        }
    }
}
