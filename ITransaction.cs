using System;

namespace Problem5v13.Transactions
{
    public interface ITransaction
    {
        public ITransaction GetTransaction();
        public double GetSum();
        public Guid GetId();
        public Guid GetFrom();
        public Guid GetTo();

    }
}