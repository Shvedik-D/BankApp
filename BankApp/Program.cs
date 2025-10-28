using BankApp.Core;
using BankApp.Core.Interfaces;
using BankAppServices;
using BankApp.Infrastructure;


class Program
{
     static IBankRepository repo = new InMemoryBankRepository();
     static IAccountService service = new AccountService();
    static void Main(string[] args)
    {
        Console.WriteLine("=== Welcome to BankApp ===");

        while (true)
        {
            Console.WriteLine("\nChoose an action:");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Transfer");
            Console.WriteLine("5. Get Statement");
            Console.WriteLine("0. Exit");

            Console.WriteLine("Your choice:");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1": CreateAccount(); break;
                case "2": Deposit(); break;
                case "3": Withdraw(); break;
                case "4": Transfer(); break;
                case "5": GetStatement(); break;
                case "0": return;
                default: Console.WriteLine("Invalid choice.");
                return;
            }
        }
     }
    static void CreateAccount()
    {
        Console.Write("Owner name: ");
        var name = Console.ReadLine();

        Console.Write("Initial deposit: ");
        if (!decimal.TryParse (Console.ReadLine(),out var deposit))
        {
            Console.WriteLine("Invalid amount.");
            return;
        }

        Console.WriteLine("Choose account type: 0 - Checking, 1 - Savings, 2 - Business");
        if (!int.TryParse(Console.ReadLine(), out var typeInt) || typeInt < 0 || typeInt > 2)
        {
            Console.WriteLine("Invalid account type.");
            return;
        }

        var type = (AccountType)typeInt;
        var id = service.CreateAccount(type, name, deposit);
        Console.WriteLine($"Account created. ID: {id}");
    }

    static void Deposit()
    {
        var id = ReadAccountId();
        if (id == null) return;

        Console.Write("Amount to deposit: ");
        if (!decimal.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("Invalid amount.");
            return;
        }

        try
        {
            service.Deposit(id.Value, amount);
            Console.WriteLine("Deposit successful.");
        }
        catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
    }

    static void Withdraw()
    {
        var id = ReadAccountId();
        if (id == null) return;

        Console.Write("Amount to withdraw: ");
        if (!decimal.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("Invalid amount.");
            return;
        }

        try
        {
            service.Withdraw(id.Value, amount);
            Console.WriteLine("Withdrawal successful.");
        }
        catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
    }

    static void Transfer()
    {
        Console.Write("From Account ID: ");
        if (!Guid.TryParse(Console.ReadLine(), out var fromId))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("To Account ID: ");
        if (!Guid.TryParse(Console.ReadLine(), out var toId))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Amount to transfer: ");
        if (!decimal.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("Invalid amount.");
            return;
        }

        try
        {
            service.Transfer(fromId, toId, amount);
            Console.WriteLine("Transfer successful.");
        }
        catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
    }

    static void GetStatement()
    {
        var id = ReadAccountId();
        if (id == null) return;

        try
        {
            var statement = service.GetStatement(id.Value);
            Console.WriteLine("\nTransaction History:");
            foreach (var tx in statement)
            {
                Console.WriteLine($"{tx.Date:u} | {tx.Type} | {tx.Amount} | Balance: {tx.BalanceAfter} | {tx.Description}");
            }
        }
        catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
    }

    static Guid? ReadAccountId()
    {
        Console.Write("Account ID: ");
        if (!Guid.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return null;
        }
        return id;
    }
}

    
        //var accountId = service.CreateAccount(AccountType.Checking, "Dasha", 1000m;
        //Console.WriteLine($"Account created : {accountId}");

        //service.Deposit(accountId, 500m);
        //service.Withdraw(accountId, 200m);
        
        //var statement = service.GetStatement(accountId);
        //Console.WriteLine("/nTransaction History: ");
        //foreach (var tx in statement)
        //{
        //    Console.WriteLine($"{tx.Date:u} |{tx.Amount} | Balance: { tx.BalanceAfter} | { tx.Description}");
        //}
    

