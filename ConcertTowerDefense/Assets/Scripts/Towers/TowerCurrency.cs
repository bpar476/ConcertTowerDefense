using UnityEngine;

public class TowerCurrency : Singleton<TowerCurrency>
{
    [SerializeField]
    [Range(0, 100)]
    private int initialCurrency;

    public int Currency { get { return currency; } }

    private int currency;

    /// <summary>
    /// Adds the supplied amount of currency to the count for the current level  
    /// </summary>
    /// <param name="value">Amount of currency to add</param>
    public void AddCurrency(int value)
    {
        currency += value;
    }

    /// <summary>
    /// Consumes the supplied amount of currency if it is available. If the player
    /// does not have enough currency, no currency will be consumed.
    /// </summary>
    /// <param name="value">Amount of currency to consume</param>
    /// <returns>True if there is enough currency to be consumed. False otherwise</returns>
    public bool ConsumeCurrency(int value)
    {
        if (currency >= value)
        {
            currency -= value;
            return true;
        }
        return false;
    }

    protected override TowerCurrency Init()
    {
        currency = initialCurrency;
        return this;
    }

    protected override bool ShouldNotDestroyOnLoad()
    {
        return false;
    }

}
