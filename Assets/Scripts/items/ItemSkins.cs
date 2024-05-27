using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using UnityEngine.UI;

public class ItemSkins : MonoBehaviour
{
    [SerializeField] private List<GameObject> ItemsPage3Skins = new List<GameObject>();

    private string[] ItemPage3SkinsStrings = new string[]
{
        "ItemSkins0Bought",
        "ItemSkins1Bought",
        "ItemSkins2Bought",
        "ItemSkins3Bought",
};
    private string[] ItemsActiveOrInActive = new string[]
{
        "Item0ActiveOrInactiveSkins",
        "Item1ActiveOrInactiveSkins",
        "Item2ActiveOrInactiveSkins",
        "Item3ActiveOrInactiveSkins",
};
    private void Start()
    {
        ForLoopDisableAllActives(ForLoopCheckWhichActive());
        ItemsPage3Skins[ForLoopCheckWhichActive()].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Active";

        // LOADING ITEM VALUES AND IF ACITVE OR NOT

        if (PlayerPrefs.GetInt(ItemPage3SkinsStrings[0], 0) == 0) ItemSkin0(false); // wenn nicht gekauft
        else ItemSkin0(true); // wenn gekauft
        if (PlayerPrefs.GetInt(ItemPage3SkinsStrings[1], 0) == 0) ItemSkin1(false);
        else ItemSkin1(true);
        if (PlayerPrefs.GetInt(ItemPage3SkinsStrings[2], 0) == 0) ItemSkin2(false);
        else ItemSkin2(true);
        if (PlayerPrefs.GetInt(ItemPage3SkinsStrings[3], 0) == 0) ItemSkin3(false);
        else ItemSkin3(true);
    }
    #region ITEM SKIN 0
    private void ItemSkin0(bool alreadyBought)
    {
        ItemsPage3Skins[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Skin_0); // itemName
        ItemsPage3Skins[0].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Skin_0).ToString(); // ItemCosts

        if (alreadyBought)
        {
            if (ForLoopCheckWhichActive() == 0) ItemsPage3Skins[0].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Active";

            else ItemsPage3Skins[0].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "InActive";
        }
    }
    public void ItemSkin0Buy() //NONE
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Skin_0) && PlayerPrefs.GetInt(ItemPage3SkinsStrings[0], 0) == 0) // IF ITEM DID NOT BOUGHT AND ENOUGHT MONEY
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Skin_0)); // Remove costs from bank

            //SaveGame.Save<float>("PlayerOutline", Item.GetOutlineSkin(Item.ItemType.Skin_0)); // ITEM EFFECT
            SaveGame.Save<string>("PlayerOutline", Item.GetColorName(Item.ItemType.Skin_0)); // ITEM EFFECT
            WindowAnnonce(Item.GetName(Item.ItemType.Skin_0)); // item bought text
            PlayerPrefs.SetInt(ItemPage3SkinsStrings[0], 1);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Skin_0) && PlayerPrefs.GetInt(ItemPage3SkinsStrings[0], 0) == 0)
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Skin_0));
        }

        if (PlayerPrefs.GetInt(ItemPage3SkinsStrings[0], 0) == 1 && !SaveGame.Load<bool>(ItemsActiveOrInActive[0], false))
        {
            SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND ITEMBOUGHT
            SaveGame.Save<bool>(ItemsActiveOrInActive[0], true);
            SaveGame.Save<string>("PlayerOutline", Item.GetColorName(Item.ItemType.Skin_0)); // ITEM EFFECT
            WindowActivate(Item.GetName(Item.ItemType.Skin_0));
            ForLoopDisableAllActives(0);
            ItemsPage3Skins[0].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Active";
        }
    }
    #endregion

    #region ITEM SKIN 1
    private void ItemSkin1(bool alreadyBought)
    {
        ItemsPage3Skins[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Skin_1); // itemName
        ItemsPage3Skins[1].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Skin_1).ToString(); // ItemCosts

        if (alreadyBought)
        {
            if (ForLoopCheckWhichActive() == 1) ItemsPage3Skins[1].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Active";

            else ItemsPage3Skins[1].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "InActive";
        }
    }
    public void ItemSkin1Buy() // RED
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Skin_1) && PlayerPrefs.GetInt(ItemPage3SkinsStrings[1], 0) == 0) // IF ITEM DID NOT BOUGHT AND ENOUGHT MONEY
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Skin_1)); // Remove costs from bank

            //SaveGame.Save<float>("PlayerOutline", Item.GetOutlineSkin(Item.ItemType.Skin_1)); // ITEM EFFECT
            SaveGame.Save<string>("PlayerOutline", Item.GetColorName(Item.ItemType.Skin_1)); // ITEM EFFECT
            WindowAnnonce(Item.GetName(Item.ItemType.Skin_1)); // item bought text
            PlayerPrefs.SetInt(ItemPage3SkinsStrings[1], 1);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Skin_1) && PlayerPrefs.GetInt(ItemPage3SkinsStrings[1], 0) == 0)
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Skin_1));
        }

        if (PlayerPrefs.GetInt(ItemPage3SkinsStrings[1], 0) == 1 && !SaveGame.Load<bool>(ItemsActiveOrInActive[1], false))
        {
            SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND ITEMBOUGHT
            SaveGame.Save<bool>(ItemsActiveOrInActive[1], true);
            SaveGame.Save<string>("PlayerOutline", Item.GetColorName(Item.ItemType.Skin_1)); // ITEM EFFECT
            WindowActivate(Item.GetName(Item.ItemType.Skin_1));
            ForLoopDisableAllActives(1);
            ItemsPage3Skins[1].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Active";
        }
    }
    #endregion

    #region ITEM SKIN 2
    private void ItemSkin2(bool alreadyBought)
    {
        ItemsPage3Skins[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Skin_2); // itemName
        ItemsPage3Skins[2].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Skin_2).ToString(); // ItemCosts

        if (alreadyBought)
        {
            if (ForLoopCheckWhichActive() == 2) ItemsPage3Skins[2].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Active";

            else ItemsPage3Skins[2].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "InActive";
        }
    }
    public void ItemSkin2Buy() // BLUE
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Skin_2) && PlayerPrefs.GetInt(ItemPage3SkinsStrings[2], 0) == 0) // IF ITEM DID NOT BOUGHT AND ENOUGHT MONEY
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Skin_2)); // Remove costs from bank

            //SaveGame.Save<float>("PlayerOutline", Item.GetOutlineSkin(Item.ItemType.Skin_2)); // ITEM EFFECT
            SaveGame.Save<string>("PlayerOutline", Item.GetColorName(Item.ItemType.Skin_2)); // ITEM EFFECT
            WindowAnnonce(Item.GetName(Item.ItemType.Skin_2)); // item bought text
            PlayerPrefs.SetInt(ItemPage3SkinsStrings[2], 1);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Skin_2) && PlayerPrefs.GetInt(ItemPage3SkinsStrings[2], 0) == 0) // IF NOT ENOUGHT MONEY
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Skin_2));
        }

        if (PlayerPrefs.GetInt(ItemPage3SkinsStrings[2], 0) == 1 && !SaveGame.Load<bool>(ItemsActiveOrInActive[2], false)) // IF ALREADY BOUGHT ACTIVATE
        {
            SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND ITEMBOUGHT
            SaveGame.Save<bool>(ItemsActiveOrInActive[2], true);
            SaveGame.Save<string>("PlayerOutline", Item.GetColorName(Item.ItemType.Skin_2)); // ITEM EFFECT
            WindowActivate(Item.GetName(Item.ItemType.Skin_2));
            ForLoopDisableAllActives(2);
            ItemsPage3Skins[2].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Active";
        }
    }
    #endregion
    #region ITEM SKIN 3
    private void ItemSkin3(bool alreadyBought)
    {
        ItemsPage3Skins[3].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Skin_3); // itemName
        ItemsPage3Skins[3].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Skin_3).ToString(); // ItemCosts

        if (alreadyBought)
        {
            if (ForLoopCheckWhichActive() == 3) ItemsPage3Skins[3].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Active";

            else ItemsPage3Skins[3].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "InActive";
        }
    }
    public void ItemSkin3Buy() // BLUE
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Skin_3) && PlayerPrefs.GetInt(ItemPage3SkinsStrings[3], 0) == 0) // IF ITEM DID NOT BOUGHT AND ENOUGHT MONEY
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Skin_3)); // Remove costs from bank

            SaveGame.Save<string>("PlayerOutline", Item.GetColorName(Item.ItemType.Skin_3)); // ITEM EFFECT
            WindowAnnonce(Item.GetName(Item.ItemType.Skin_3)); // item bought text
            PlayerPrefs.SetInt(ItemPage3SkinsStrings[3], 1);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Skin_3) && PlayerPrefs.GetInt(ItemPage3SkinsStrings[3], 0) == 0) // IF NOT ENOUGHT MONEY
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Skin_3));
        }

        if (PlayerPrefs.GetInt(ItemPage3SkinsStrings[3], 0) == 1 && !SaveGame.Load<bool>(ItemsActiveOrInActive[3], false)) // IF ALREADY BOUGHT ACTIVATE
        {
            SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND ITEMBOUGHT
            SaveGame.Save<bool>(ItemsActiveOrInActive[3], true);
            SaveGame.Save<string>("PlayerOutline", Item.GetColorName(Item.ItemType.Skin_3)); // ITEM EFFECT
            WindowActivate(Item.GetName(Item.ItemType.Skin_3));
            ForLoopDisableAllActives(3);
            ItemsPage3Skins[3].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Active";
        }
    }
    #endregion


    private void ForLoopDisableAllActives(int ItemIndex)
    {
        for (int i = 0; i <= ItemsPage3Skins.Count -1; i++)
        {
            if (i != ItemIndex)
            {
                SaveGame.Save<bool>(ItemsActiveOrInActive[i], false);
                ItemsPage3Skins[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "InActive";
            }
        }
    }

    private int ForLoopCheckWhichActive()
    {
        var val = 0;
        for (int i = 0; i < ItemsPage3Skins.Count; i++)
        {
            if (SaveGame.Exists(ItemsActiveOrInActive[i]))
            {
                if (SaveGame.Load<bool>(ItemsActiveOrInActive[i], false))
                {
                    val = i;
                    break;
                }
                else val = 0;
            }
            else val = 0;
        }
        return val;
    }

    private void WindowAnnonce(string itemName)
    {
        var ItemBoughtText = FindObjectOfType<ShopController>().ItemBoughtText;
        ItemBoughtText.GetComponent<Animator>().SetTrigger("Show");
        ItemBoughtText.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "successfully bought " + "\n" + itemName;
    }
    private void WindowActivate(string itemName)
    {
        var ItemBoughtText = FindObjectOfType<ShopController>().ItemBoughtText;
        ItemBoughtText.GetComponent<Animator>().SetTrigger("Show");
        ItemBoughtText.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "Activated " + "\n" + itemName;
    }
    private void WindowAnnonceNotEnoughtMoney(string itemName)
    {
        SoundManager.PlaySFX("NotEnoughtCoins", false, 0, .3f); // SOUND NOTENOUGHTCOINS
        var ItemBoughtText = FindObjectOfType<ShopController>().ItemBoughtText;
        ItemBoughtText.GetComponent<Animator>().SetTrigger("Show");
        ItemBoughtText.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "you have not enough \n Coins to buy " + "\n" + itemName;
    }
}
