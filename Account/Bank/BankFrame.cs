using System;
using System.Collections.Generic;

using System.Linq;
using Problem5v13.Account.AccountType;
using Transaction = Problem5v13.Transactions.Transaction;



namespace Problem5v13.Account.Bank
{
    public abstract class BankFrame : ABank
    {
        public static List<Transaction> Added = new List<Transaction>();
        public static List<ABank> AccountList = new List<ABank>();
        
        //public static Dictionary<Client, List<AccountType.Account>> Accounting;

        private Client GetClient(Guid id, Dictionary<Client, List<AccountType.Account>> clients)
        {
            foreach (Client client in clients.Keys)
            {
                if (client.GetId().Equals(id)) return client;
            }

            return null;
        }

        private AccountType.Account GetAccount(Guid id, Dictionary<Client, List<AccountType.Account>> clients)
        {
            foreach (Client client in clients.Keys)
            {
                foreach (AccountType.Account account in clients[client])
                {
                    if (account.GetId().Equals(id)) return account;
                }
            }

            return null;
        }

        
        
        
        private bool TransWithdraw(Transaction transaction, Dictionary<Client, List<AccountType.Account>> clients)
        {
            foreach (Client client in clients.Keys)
            {
                foreach (var account in clients[client].Where(account => account.GetId().Equals(transaction.GetTo())))
                {
                    account.TransWithdraw(transaction.GetSum());
                    return true;
                }
            }

            return false;
        }

        private bool TransDep(Transaction transaction, Dictionary<Client, List<AccountType.Account>> clients)
        {
            foreach (Client client in clients.Keys)
            {
                foreach (var account in clients[client].Where(account => account.GetId().Equals(transaction.GetFrom())))
                {
                    account.TransWithdraw(transaction.GetSum());
                    return true;
                }
            }

            return false;
        }


        protected Guid CreateNewClient(Guid id, Dictionary<Client, List<AccountType.Account>> clients)
        {
            var newClient = new Client(id);
            clients.Add(newClient, new List<AccountType.Account>());
            return newClient.GetId();
        }   

        protected Guid AddClientAddress(Guid id, string address, Dictionary<Client, List<AccountType.Account>> clients)
        {
            Client client = GetClient(id, clients);
            List<AccountType.Account> accounts = clients[client];
            clients.Remove(client);
            client.AddAddress(address);
            
            clients.Add(client, accounts);
            return client.GetId();
        }

        protected Guid AddClientPassport(Guid id , string passport, Dictionary<Client, List<AccountType.Account>> clients)
        {
            Client client = GetClient(id, clients);
            List<AccountType.Account> accounts = clients[client];
            clients.Remove(client);
            client.AddPassport(passport);
            clients.Add(client, accounts);
            return client.GetId();
        }


        private Guid ReturnAccType(Guid id, Dictionary<Client, List<AccountType.Account>> clients, AccountType.Account account)
        {
            foreach (Client client in clients.Keys)
            {
                if (client.GetId().Equals(id))
                {
                    clients[client].Add(account);        
                }
                //return client;
            }
            //clients[GetClient(id, clients)].Add(account);
            return account.GetId();
        }

        
        protected Guid NewDeposit(Guid id, Guid bankId, Dictionary<Client, List<AccountType.Account>> clients, double status, int tau)
        {
            AccountType.Account deposit = new DepositAccount(status, tau, bankId);
            
            foreach (Client client in clients.Keys)
            {
                if (client.GetId().Equals(id)) 
                    clients[client].Add(deposit);
            }
            return deposit.GetId();
        }

        protected Guid NewDebit(Guid id, Guid bankId, Dictionary<Client, List<AccountType.Account>> clients, double percent)
        {
            AccountType.Account debit = new DebitAccount(0.0, percent / 365, bankId);
            
            foreach (Client client in clients.Keys)
            {
                if (client.GetId().Equals(id)) 
                    clients[client].Add(debit);
            }
            return debit.GetId();
        }

        protected Guid NewCredit(Guid id, Guid bankId, Dictionary<Client, List<AccountType.Account>> clients, double limit, double commission)
        {
            AccountType.Account credit = new CreditAccount(0, limit, commission, bankId);
            var answer = ReturnAccType(id, clients, credit);
            return answer;
        }


        protected bool WithDraw(Guid id, double sum, Dictionary<Client, List<AccountType.Account>> clients)
        {
            return (clients.Keys.SelectMany(client => clients[client],
                (client, account) => (id.Equals(account.GetId()) && !client.ProofVerification())
                    ? account.Withdraw(sum) : (id.Equals(account.GetId()) && client.ProofVerification()) && account.Withdraw(sum))).FirstOrDefault();
        } //вывод

        protected bool Deposit(Guid id, double sum, Dictionary<Client, List<AccountType.Account>> clients)
        {
            foreach (var client in clients.Keys)
            {
                foreach (var account in clients[client])
                {
                    if (account.GetId().Equals(id))
                    {
                        account.Deposit(sum);
                        return true;
                    }
                }
            }

            return false;
        }

        protected bool Remittance(Guid fromId, Guid toId, double sum, Dictionary<Client, List<AccountType.Account>> clients)
        {
            ABank bankFrom;
            ABank bankTo;
            foreach (ABank bank in AccountList)
            {
                if ((bank.GetId().Equals(GetAccount(fromId, clients).GetBankId()))) bankFrom = bank;
                if ((bank.GetId().Equals(GetAccount(toId, clients).GetBankId()))) bankTo = bank;
            }

            //if (GetAccount(fromId, clients).GetStatus() < sum) return false;

            foreach (Client client in clients.Keys)
            {
                foreach (AccountType.Account account in clients[client])
                {
                    if (account.GetId().Equals(fromId)) continue;
                        if (account.GetStatus() < sum) return false;
                }
            }
            
            if (!WithDraw(fromId, sum, clients)) return false;
            Deposit(toId, sum, clients);

            return true;
        }


        protected bool Cancel(Guid id, Guid bankId, Dictionary<Client, List<AccountType.Account>> clients)
        {
            Transaction transed = null;
            foreach (var transaction in Added.Where(transaction => transaction.GetId().Equals(id)))
                transed = transaction;

            return (!transed.GetTo().Equals(bankId) || !transed.GetFrom().Equals(bankId))
                ? false
                : (TransDep(transed, clients)) && TransWithdraw(transed, clients);
            
        }
    }
}
