using System;
using Problem5v13.Account.Bank;

namespace Problem5v13.Account.AccountType
{
    
    public abstract class Account
    {
        private Guid Id { get; set; }
        private Guid BankId { get; set; }
        private double Status { get; set; }
        
        
        public abstract bool Withdraw(double sum);
        public virtual void Deposit(double sum) => Status += sum;
        public void TransWithdraw(double sum) => Status -= sum;  // вывод средств
        public void TransDeposit(double sum) => Status += sum;  // зачисление средств
        public double GetStatus() => Status;
        public Guid GetId() => Id;
        public Guid GetBankId() => BankId;
        public virtual void Percent(){}
        
        
        
    }
    
    
    
    
}
