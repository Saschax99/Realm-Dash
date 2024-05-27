using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;
public class ItemUsables : MonoBehaviour
{
    [SerializeField] private List<GameObject> ItemsPage4Usables = new List<GameObject>();

    private string[] ItemPage4UsablesStrings = new string[]
{
        "ItemUsables0Bought",
        //"ItemUsables1Bought",
};
    private void Start()
    {
        //if (PlayerPrefs.GetInt(ItemPage4UsablesStrings[0], 0) < Item.GetHealthMaxStack(Item.ItemType.Health_1_500HP)) ItemUsables0(false);
        //else ItemUsables0(true);

        if (SaveGame.Load<int>("MaxStack500HP",0) < Item.GetHealthMaxStack(Item.ItemType.Health_1_500HP)) ItemUsables0(false);
        else ItemUsables0(true);

        //if (!SaveGame.Load<bool>(ItemPage4UsablesStrings[0], false)) ItemUsables1(false);
        //else ItemUsables1(true);
    }
    #region ITEM USABLES 0
    private void ItemUsables0(bool maximumReached)
    {
        ItemsPage4Usables[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Health_1_500HP);
        ItemsPage4Usables[0].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = Item.GetHealth(Item.ItemType.Health_1_500HP).ToString() + " HP"; // ItemStats
        ItemsPage4Usables[0].transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Health_1_500HP).ToString();
        ItemsPage4Usables[0].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = SaveGame.Load<int>("MaxStack500HP", 0).ToString() + "/5"; // How many pots in inventory

        if (maximumReached) ItemsPage4Usables[0].GetComponent<Button>().interactable = false;
    }
    public void ItemUsables0Buy()
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Health_1_500HP) && SaveGame.Load<int>("MaxStack500HP", 0) < Item.GetHealthMaxStack(Item.ItemType.Health_1_500HP))
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Health_1_500HP));
            SaveGame.Save<int>("MaxStack500HP", SaveGame.Load<int>("MaxStack500HP", 0) + 1); // add 1 pot
            ItemsPage4Usables[0].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = SaveGame.Load<int>("MaxStack500HP", 0).ToString() + "/5"; // How many pots in inventory
            //SaveGame.Save<int>("Attack", Item.GetDamage(Item.ItemType.Health_1_500HP));
            WindowAnnonce(Item.GetName(Item.ItemType.Health_1_500HP));
            PlayerPrefs.SetInt(ItemPage4UsablesStrings[0], 1);
            if (SaveGame.Load<int>("MaxStack500HP", 0) >= Item.GetHealthMaxStack(Item.ItemType.Health_1_500HP))
            {
                ItemsPage4Usables[0].GetComponent<Button>().interactable = false;
            }
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Health_1_500HP) && PlayerPrefs.GetInt(ItemPage4UsablesStrings[0], 0) == 0)
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Health_1_500HP));
        }
        //else if (SaveGame.Load<int>("MaxStack500HP", 0) >= Item.GetHealthMaxStack(Item.ItemType.Health_1_500HP))
        //{
        //    ItemsPage4Usables[0].GetComponent<Button>().interactable = false;
        //    WindowAnnonceMaxReached(Item.GetName(Item.ItemType.Health_1_500HP));
        //}
    }
    #endregion

    //#region ITEM USABLES 1
    //private void ItemUsables1(bool alreadybought)
    //{
    //    ItemsPage4Usables[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Coins_Magnet);
    //    ItemsPage4Usables[1].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Coins_Magnet).ToString();

    //    if (alreadybought) ItemsPage4Usables[1].GetComponent<Button>().interactable = false;
    //}
    //public void ItemUsables1Buy()
    //{
    //    if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Coins_Magnet) && !SaveGame.Load<bool>(ItemPage4UsablesStrings[1], false))
    //    {
    //        SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

    //        SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Coins_Magnet));

    //        SaveGame.Save<int>("CoinsMagnet", Item.GetMagnetValue(Item.ItemType.Coins_Magnet));

    //        WindowAnnonce(Item.GetName(Item.ItemType.Coins_Magnet));
    //        SaveGame.Save<bool>(ItemPage4UsablesStrings[1], true);

    //        ItemsPage4Usables[1].GetComponent<Button>().interactable = false;
    //    }
    //    else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Coins_Magnet) && !SaveGame.Load<bool>(ItemPage4UsablesStrings[1], false))
    //    {
    //        WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Coins_Magnet));
    //    }
    //}
    //#endregion

    private void WindowAnnonce(string itemName)
    {
        var ItemBoughtText = FindObjectOfType<ShopController>().ItemBoughtText;
        ItemBoughtText.GetComponent<Animator>().SetTrigger("Show");
        ItemBoughtText.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "successfully bought " + "\n" + itemName;

    }
    private void WindowAnnonceMaxReached(string itemName)
    {
        var ItemBoughtText = FindObjectOfType<ShopController>().ItemBoughtText;
        ItemBoughtText.GetComponent<Animator>().SetTrigger("Show");
        ItemBoughtText.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "already reached max " + "\n" + itemName;

    }
    private void WindowAnnonceNotEnoughtMoney(string itemName)
    {
        SoundManager.PlaySFX("NotEnoughtCoins", false, 0, .3f); // SOUND NOTENOUGHTCOINS
        var ItemBoughtText = FindObjectOfType<ShopController>().ItemBoughtText;
        ItemBoughtText.GetComponent<Animator>().SetTrigger("Show");
        ItemBoughtText.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "you have not enough \n Coins to buy " + "\n" + itemName;
    }
}
