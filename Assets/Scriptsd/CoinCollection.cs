using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    private int Coin = 0;
    public bool Upgrade = false;


    public TextMeshProUGUI CoinText;
    private void Update()
    {
        if (Coin >= 40)
        {
            WeaponSwitching.Upgrade = true; 

        }

        if (Coin >= 50)
        {
            WeaponSwitching.Upgrade2 = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.transform.tag == "Coin")
        {
            Coin++;

            CoinText.text = "Coins: " + Coin.ToString();
            Debug.Log(Coin);
        }
    }
}
