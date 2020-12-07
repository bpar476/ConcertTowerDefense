using UnityEngine;

public class AddCurrencyButton : MonoBehaviour
{
    public void AddCurrency(int amount)
    {
        TowerCurrency.Instance.AddCurrency(amount);
    }
}
