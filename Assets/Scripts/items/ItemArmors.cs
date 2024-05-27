using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using UnityEngine.UI;
public class ItemArmors : MonoBehaviour
{
    [SerializeField] private List<GameObject> ItemsPage2Armor = new List<GameObject>();

    private string[] ItemPage2ArmorStrings = new string[]
    {
        "ItemArmor0Bought",
        "ItemArmor1Bought",
        "ItemArmor2Bought",
        "ItemArmor3Bought",
        "ItemArmor4Bought",
        "ItemArmor5Bought"
    };
    private void Start()
    {
        //if (SaveGame.Exists(ItemPage2ArmorStrings[0]))
        if (PlayerPrefs.GetInt(ItemPage2ArmorStrings[0], 0) == 0) ItemArmor0(false);
        else ItemArmor0(true);
        if (PlayerPrefs.GetInt(ItemPage2ArmorStrings[1], 0) == 0) ItemArmor1(false);
        else ItemArmor1(true);
        if (PlayerPrefs.GetInt(ItemPage2ArmorStrings[2], 0) == 0) ItemArmor2(false);
        else ItemArmor2(true);
        if (PlayerPrefs.GetInt(ItemPage2ArmorStrings[3], 0) == 0) ItemArmor3(false);
        else ItemArmor3(true);
        if (PlayerPrefs.GetInt(ItemPage2ArmorStrings[4], 0) == 0) ItemArmor4(false);
        else ItemArmor4(true);
        if (PlayerPrefs.GetInt(ItemPage2ArmorStrings[5], 0) == 0) ItemArmor5(false);
        else ItemArmor5(true);
    }
    #region ITEM ARMOR_0
    private void ItemArmor0(bool alreadybought) // LOADING VALUES OF THE ITEM
    {
        ItemsPage2Armor[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Armor_0); // itemName
        ItemsPage2Armor[0].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Armor_0).ToString(); // ItemCosts
        if (alreadybought) forLoopItems(0);
    }
        
    public void ItemArmor0Buy() // BUTTON ACTIVATES THIS FUNCT.
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Armor_0) && PlayerPrefs.GetInt(ItemPage2ArmorStrings[0], 0) == 0) // IF ITEM DID NOT BOUGHT AND ENOUGHT MONEY
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Armor_0)); // Remove costs from bank

            SaveGame.Save<float>("Defense", Item.GetDefense(Item.ItemType.Armor_0)); // ITEM EFFECT
            WindowAnnonce(Item.GetName(Item.ItemType.Armor_0)); // item bought text
            PlayerPrefs.SetInt(ItemPage2ArmorStrings[0], 1);
            forLoopItems(0);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Armor_0) && PlayerPrefs.GetInt(ItemPage2ArmorStrings[0], 0) == 0)
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Armor_0));
        }
    }
    #endregion

    #region ITEM ARMOR_1
    private void ItemArmor1(bool alreadybought)
    {
        ItemsPage2Armor[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Armor_1); // itemName
        ItemsPage2Armor[1].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Armor_1).ToString(); // ItemCosts
        if (alreadybought) forLoopItems(1);
    }
    public void ItemArmor1Buy()
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Armor_1) && PlayerPrefs.GetInt(ItemPage2ArmorStrings[1], 0) == 0)
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Armor_1));

            SaveGame.Save<float>("Defense", Item.GetDefense(Item.ItemType.Armor_1)); // ITEM EFFECT
            WindowAnnonce(Item.GetName(Item.ItemType.Armor_1));
            PlayerPrefs.SetInt(ItemPage2ArmorStrings[1], 1);
            forLoopItems(1);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Armor_1) && PlayerPrefs.GetInt(ItemPage2ArmorStrings[1], 0) == 0)
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Armor_1));
        }
    }
    #endregion
    #region ITEM ARMOR_2
    private void ItemArmor2(bool alreadybought)
    {
        ItemsPage2Armor[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Armor_2); // itemName
        ItemsPage2Armor[2].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Armor_2).ToString(); // ItemCosts
        if (alreadybought) forLoopItems(2);
    }
    public void ItemArmor2Buy()
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Armor_2) && PlayerPrefs.GetInt(ItemPage2ArmorStrings[2], 0) == 0)
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Armor_2));

            SaveGame.Save<float>("Defense", Item.GetDefense(Item.ItemType.Armor_2));
            WindowAnnonce(Item.GetName(Item.ItemType.Armor_2));
            PlayerPrefs.SetInt(ItemPage2ArmorStrings[2], 1);
            forLoopItems(2);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Armor_2) && PlayerPrefs.GetInt(ItemPage2ArmorStrings[2], 0) == 0)
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Armor_2));
        }
    }
    #endregion
    #region ITEM ARMOR_3
    private void ItemArmor3(bool alreadybought)
    {
        ItemsPage2Armor[3].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Armor_3); // itemName
        ItemsPage2Armor[3].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Armor_3).ToString(); // ItemCosts
        if (alreadybought) forLoopItems(3);
    }
    public void ItemArmor3Buy()
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Armor_3) && PlayerPrefs.GetInt(ItemPage2ArmorStrings[3], 0) == 0)
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Armor_3));

            SaveGame.Save<float>("Defense", Item.GetDefense(Item.ItemType.Armor_3));
            WindowAnnonce(Item.GetName(Item.ItemType.Armor_3));
            PlayerPrefs.SetInt(ItemPage2ArmorStrings[3], 1);
            forLoopItems(3);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Armor_3) && PlayerPrefs.GetInt(ItemPage2ArmorStrings[3], 0) == 0)
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Armor_3));
        }
    }
    #endregion
    #region ITEM ARMOR_4
    private void ItemArmor4(bool alreadybought)
    {
        ItemsPage2Armor[4].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Armor_4); // itemName
        ItemsPage2Armor[4].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Armor_4).ToString(); // ItemCosts
        if (alreadybought) forLoopItems(4);
    }
    public void ItemArmor4Buy()
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Armor_4) && PlayerPrefs.GetInt(ItemPage2ArmorStrings[4], 0) == 0)
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Armor_4));

            SaveGame.Save<float>("Defense", Item.GetDefense(Item.ItemType.Armor_4));
            WindowAnnonce(Item.GetName(Item.ItemType.Armor_4));
            PlayerPrefs.SetInt(ItemPage2ArmorStrings[4], 1);
            forLoopItems(4);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Armor_4) && PlayerPrefs.GetInt(ItemPage2ArmorStrings[4], 0) == 0)
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Armor_4));
        }
    }
    #endregion
    #region ITEM ARMOR_5
    private void ItemArmor5(bool alreadybought)
    {
        ItemsPage2Armor[5].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Armor_5); // itemName
        ItemsPage2Armor[5].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Armor_5).ToString(); // ItemCosts
        if (alreadybought) forLoopItems(5);
    }
    public void ItemArmor5Buy()
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Armor_5) && PlayerPrefs.GetInt(ItemPage2ArmorStrings[5], 0) == 0)
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Armor_5));

            SaveGame.Save<float>("Defense", Item.GetDefense(Item.ItemType.Armor_5));
            WindowAnnonce(Item.GetName(Item.ItemType.Armor_5));
            PlayerPrefs.SetInt(ItemPage2ArmorStrings[5], 1);
            forLoopItems(5);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Armor_5) && PlayerPrefs.GetInt(ItemPage2ArmorStrings[5], 0) == 0)
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Armor_5));
        }
    }
    #endregion
    private void WindowAnnonce(string itemName)
    {
        var ItemBoughtText = FindObjectOfType<ShopController>().ItemBoughtText;
        ItemBoughtText.GetComponent<Animator>().SetTrigger("Show");
        ItemBoughtText.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "successfully bought " + "\n" + itemName;
    }
    private void WindowAnnonceNotEnoughtMoney(string itemName)
    {
        SoundManager.PlaySFX("NotEnoughtCoins", false, 0, .3f); // SOUND NOTENOUGHTCOINS
        var ItemBoughtText = FindObjectOfType<ShopController>().ItemBoughtText;
        ItemBoughtText.GetComponent<Animator>().SetTrigger("Show");
        ItemBoughtText.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "you have not enough \n Coins to buy " + "\n" + itemName;
    }
    private void forLoopItems(int ItemIndex)
    {
        for (int i = 0; i <= ItemIndex; i++)
        {
            ItemsPage2Armor[i].GetComponent<Button>().interactable = false;
        }
    }
}
