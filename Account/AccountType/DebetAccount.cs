using System;

namespace Problem5v13.Account.AccountType
{
    public class DebitAccount : Account
    {
        
        private Guid Id { get; }
        private Guid BankId { get;  }
        private double AccAmount { get; set; }
        private double _percent{ get;}
        private double LastMonth{ get; set; }
        private int _days;
        
        public DebitAccount( double accAmount, double percent, Guid bankId) 
        {
            Id = Guid.NewGuid();
            BankId = bankId;
            AccAmount = accAmount;
            _percent = percent;
            LastMonth = 0;
            _days = 30;
        }
        

        public override bool Withdraw(double sum)
        {
            if (AccAmount < sum) return false;
            AccAmount -= sum; 
            return true;
        }
        
        public override void Percent()
        {
            if (_days != 0) {
                LastMonth += AccAmount * _percent;
                _days--;
            } else {
                AccAmount += LastMonth + AccAmount * _percent;
                LastMonth = 0;
                _days = 30;
            }
        }
    }
}
