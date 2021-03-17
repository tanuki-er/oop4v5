using System;
using System.Collections.Generic;

namespace Problem5v13.Account.AccountType
{
    public class CreditAccount : Account
    {
        
        private Guid Id{ get;  }
        private Guid BankId{ get; }
        private double Status{ get; set; }
        private double Limit{ get; }
        private double Commission{ get; }

        //private List<Account> _accounts;
        
        
        public CreditAccount(double status, double limit, double commission, Guid bankId)
        {
            Id = Guid.NewGuid();
            Limit = limit;
            Commission = commission;
            BankId = bankId;
            Status = status;
        }

        
        public override bool Withdraw(double sum)
        {
            if (Status >= 0 && Status + Limit >= sum) {
                Status -= sum;
                return true;
            }

            if (!(Status < 0) || !((sum + sum * Commission) <= Limit - Math.Abs(Status))) return false;
            Status -= sum + sum * Commission;
            return true;
        }
       
        public override void Percent() { }
    }
}