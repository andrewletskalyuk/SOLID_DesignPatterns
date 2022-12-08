// See https://aka.ms/new-console-template for more information
using static System.Console;
public class Account
{
    private int Balance  = 0;
    private int limit = 0;
    public void Deposit(int amount)
    {
        Balance += amount;
        WriteLine($"Deposited ${amount}, balance is now {Balance}");
    }
    public bool Withdraw(int amount)
    {
        if (Balance - amount >= limit)
        {
            Balance -= amount;
            WriteLine($"Withdrew ${amount}, balance is now {Balance}");
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"{nameof(Balance)}: {Balance}";
    }
}
public interface ICommander
{
    void Call();
    void Undo();
}
public class Comander : ICommander
{
    private Account account;

    public enum Action
    {
        Deposit,
        Withdraw
    }

    public Action TheAction;
    public int amount;
    public bool Success;

    public Comander(Account account, Action theAction, int amount)
    {
        this.account = account ?? throw new ArgumentNullException(paramName: nameof(account));
        TheAction = theAction;
        this.amount = amount;
    }

    public void Call()
    {
        switch (TheAction)
        {
            case Action.Deposit:
                account.Deposit(amount);
                Success = true;
                break;
            case Action.Withdraw:
                Success = account.Withdraw(amount);
                break;
            default:
                break;
        }
    }

    public void Undo()
    {
        if (!Success) return;
        switch (TheAction)
        {
            case Action.Deposit:
                account.Withdraw(amount);
                break;
            case Action.Withdraw:
                account.Deposit(amount);
                break;
            default:
                break;
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        var a = new Account();
        var commands = new List<Comander> { 
            new Comander(a, Comander.Action.Deposit, 777),
            new Comander(a, Comander.Action.Withdraw, 1)
        };
        WriteLine(a);

        foreach (var c in commands)
            c.Call();
    }
}