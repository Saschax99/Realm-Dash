using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;
public class UpdateCurrenciesForShopAndPause : MonoBehaviour
{
    private Text TextCoinsAmount;
    private void Start()
    {
        TextCoinsAmount = transform.Find("Grid_Softcurrencies/Resource_Coins/Panel_Bar/Text_CoinsAmount").GetComponent<Text>();
    }
    private void Update()
    {
        if (TextCoinsAmount.text != SaveGame.Load<int>("CoinsAmount", 0).ToString("0"))
        {
            TextCoinsAmount.text = SaveGame.Load<int>("CoinsAmount", 0).ToString("0");
        }
    }
}
