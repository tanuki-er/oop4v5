using System;
using System.Collections.Generic;

namespace Problem5v13.Account.Bank
{
    public abstract class ABank
    {
        public abstract Client GetClient(Guid id);
        public abstract Guid CreateClient(Guid id);
        //
        //public Guid AddName();
        public abstract Guid AddAddress(Guid id, string address);
        public abstract Guid AddPassport(Guid id, string passport);
        //
        public abstract Guid AddCreditAccount(Guid id);
        public abstract Guid AddDebitAccount(Guid id);
        public abstract Guid AddDepositAccount(Guid id, double sum, int tau);
        //
        public abstract Guid GetId();
        //
        public abstract double GetStatus(Guid id);
        //
        public abstract bool Withdraw(double sum, Guid id); //вывод средств

        public abstract bool Deposit(double sum, Guid id); //
        public abstract bool Remittance(Guid idFrom, Guid idTo, double sum); // перевод средств
        public abstract bool Cancel(Guid id, int type);   // отмена перевода
        //
        public abstract double GetCommission();
        //
        public abstract void Iterator();


    }
}
