using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;
public class UpdateCurrencies : MonoBehaviour
{
    Text TextCoinsAmount;
    [HideInInspector] public int CoinsValue = 0;
    private bool recall = false;

    private void Start()
    {
        CoinsValue = SaveGame.Load<int>("CoinsAmount", 0);

        TextCoinsAmount = transform.Find("Grid_Softcurrencies/Resource_Coins/Panel_Bar/Text_CoinsAmount").GetComponent<Text>();
        if (TextCoinsAmount.text != CoinsValue.ToString())
        {
            TextCoinsAmount.text = CoinsValue.ToString();
        }
    }
    private void Update()
    {
        if (TextCoinsAmount.text != CoinsValue.ToString("0"))
        {
            TextCoinsAmount.text = CoinsValue.ToString("0");
        }
        
        if (FindObjectOfType<Player_Controller>().playerDead && !recall)
        {
            SaveGame.Save<int>("CoinsAmount", CoinsValue);
            recall = true;
        }
    }
    private void OnApplicationQuit()
    {
        SaveGame.Save<int>("CoinsAmount", CoinsValue);
    }
}
