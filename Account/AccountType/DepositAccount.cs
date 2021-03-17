using System;

namespace Problem5v13.Account.AccountType
{
    public class DepositAccount : Account
    {
        private Guid Id { get; }
        private Guid BankId{ get; }
        private double Status{ get; set; }
        private double _percent{ get; set; }
        private double LastMonth { get; set; }
        private int Days { get; set; }
        private int Tau { get; set; }
        
        private double GetPercent(double sum) => (sum < 50000) ? 0.03 : (sum < 100000) ? 0.035 : 0.04;

        
        public DepositAccount(double status, int tau, Guid bankId)
        {
            Id = Guid.NewGuid();
            Status = status;
            _percent = GetPercent(status);
            Tau = tau;
            BankId = bankId;
            LastMonth = 0.0;
            Days = 30;
        }

        

        public override bool Withdraw(double sum)
        {
            if (Tau == 0 || !(Status >= sum)) return false;
            Status -= sum;
            
            return true;
        }

        public override void Deposit(double sum)
        {
            Status += sum;
            _percent = GetPercent(Status);
        }

        public override void Percent()
        {
            Tau--;
            if (Days != 0)
            {
                LastMonth += Status * _percent;
                Days--;
            } else {
                Status += LastMonth + Status * _percent;
                LastMonth = 0;
                Days = 30;
            }
        }
        //public int Gettau() => _tau;
    }
}
























