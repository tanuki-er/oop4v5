using System;
using System.Text;

namespace Problem5v13.Account
{
    public class Client
    {
        private Guid Id { get; }
        private string Name { get; }
        private string Address { get; set; }
        private string Passport { get; set; }

        private bool _verification;
        private bool Verified() => (Address != null && Passport != null);

        public Client(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            
            Address = null;
            Passport = null;
            _verification = Verified();
        }
        public Client(string name, string address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            
            Passport = null;
            _verification = Verified();
        }
        public Client(string name, string address, string passport)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            Passport = passport;
            
            _verification = Verified();
        }
        
        public void AddAddress(string address)
        {
            Address = address;
            _verification = Verified();
        }
        
        public void AddPassport(string passport) 
        {
            Passport = passport;
            _verification = Verified();
        }
        
        public Guid GetId() => Id;
        
        public bool ProofVerification() => _verification;
        
        public override string ToString() 
            => new StringBuilder()
                .AppendLine(Name)
                .AppendLine(Address)
                .AppendLine(Passport).ToString();
    }
}








