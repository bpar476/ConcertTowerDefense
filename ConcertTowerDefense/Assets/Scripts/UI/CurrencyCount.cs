using UnityEngine;
using UnityEngine.UI;

public class CurrencyCount : MonoBehaviour
{

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = TowerCurrency.Instance.Currency.ToString();
    }

}
