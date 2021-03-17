using System;
using System.Collections.Generic;

namespace Problem5v13.Transactions
{
    public class Transaction : ITransaction
    {
        private Guid Id { get; }
        private Guid From { get; }
        private Guid To { get; }
        private double Sum { get; set; }

        public Transaction(double sum, Guid from, Guid to)
        {
            Id = Guid.NewGuid();
            Sum = sum;
            From = from;
            To = to;
        }
        
        public ITransaction GetTransaction() => this;
        
        public double GetSum() => Sum;
        public Guid GetId() => Id;
        public Guid GetFrom() => From;
        public Guid GetTo() => To;
    }

    public class TransMethods
    {
        private readonly List<Transaction> _transactions;

        public TransMethods()
        {
            _transactions = new List<Transaction>();
        }

        public void AddTrans(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public Transaction GetTrans(Guid id)
        {
            foreach (var transaction in _transactions)
            {
                if (transaction.GetId().Equals(id))
                {
                    _transactions.Remove(transaction);
                }
            }
            return null;
        }

        public bool Contained(Guid id)
        {
            foreach (var transaction in _transactions)
            {
                if (transaction.GetId().Equals(id)) return true;
            }
            return false;

        }
        
        
        
        
    }
}



















