public interface IBank
{
    public void Deposit(int amount);

    public void Withdraw(int amount);

    public int GetBalance(int amount);
}
