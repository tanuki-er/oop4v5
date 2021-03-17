using System;
using Problem5v13.Account;
using Problem5v13.Account.Bank;


namespace Problem5v13
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            
            Bank fBank = new Bank("Tinkoff", 0.2, 12, 4000);
            

            Client fClient = new Client("user1");//, "AGTU", "1234 451236");
            fClient.AddAddress("address"); // добавляет адрес и паспорт, выводится корректно + проверка верификации
            fClient.AddPassport("passport");


            
            
            Bank sBank = new Bank("SPB", 0.35, 10, 5000);
            Client sClient = new Client("user2", "address2", "passport2");
            
            Console.WriteLine(sClient);

            fBank.AddDebitAccount(fClient.GetId());
            
            fBank.CreateClient(sClient.GetId());
            
            
            fBank.Deposit(4000, fClient.GetId());

            
            
            //sBank.AddDepositAccount(sClient.GetId(), 5000,70); // создали депозит-счет на второго перса во втором банке

            sBank.AddDebitAccount(sClient.GetId());
            sBank.Deposit(2000, sClient.GetId());
            
            Console.WriteLine("first amount: " + fBank.GetStatus(fClient.GetId()));
            Console.WriteLine("second amount: " + sBank.GetStatus(sClient.GetId()));
            
            fBank.Remittance(fClient.GetId(), sClient.GetId(), 1000);
            
            Console.WriteLine("first amount: " + fBank.GetStatus(fClient.GetId()));
            Console.WriteLine("second amount: " + sBank.GetStatus(sClient.GetId()));
            
            

            


            /*
            var bank = new Bank("Saint PB Central Bank");
            var account = new 
            
            var client = Client.Builder("name").SetAddress("address").SetPassport("passport").GetClient; // builder
            
            var creditAccount = new CreditAccount(client, 2000, 4000, 2);     // balance, limit, fee
            //var otherAccount = new .. (client, params)
            bank.AddClient(client);
            bank.AddAccountToClient(client, creditAccount);



            bank.WithDraw(creditAccount.id, "sum");
            Console.WriteLine(creditAccount.balance);
            */
        }
    }
}