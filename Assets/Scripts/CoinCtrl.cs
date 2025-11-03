using UnityEngine;
using UnityEngine.UI;

public class CoinCtrl : MonoBehaviour
{
    public static CoinCtrl I;
    [SerializeField] Text txtCoin;


    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        UpdateCoin();
    }


    public void UpdateCoin()
    {
        txtCoin.text = PlayerPrefData.Coin.ToString();  
    }
}
