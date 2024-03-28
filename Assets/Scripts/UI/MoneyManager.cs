using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private int money;
    public int Money => money;
    private void Start()
    {
        money = 10;
    }
    public void AddMoney(int value)
    {
        money += value;
    }
    public void SubMoney(int value)
    {
        money -= value;
    }
}
