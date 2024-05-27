using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using UnityEngine.UI;

public class ItemWeapons : MonoBehaviour
{
    [SerializeField] private List<GameObject> ItemsPage1Weapons = new List<GameObject>();

    private string[] ItemPage1WeaponsStrings = new string[]
{
        "ItemWeapon0Bought",
        "ItemWeapon1Bought",
        "ItemWeapon2Bought",
        "ItemWeapon3Bought",
        "ItemWeapon4Bought",
        "ItemWeapon5Bought"
};

    private void Start()
    {
        if (PlayerPrefs.GetInt(ItemPage1WeaponsStrings[0], 0) == 0) ItemWeapon0(false);
        else ItemWeapon0(true);
        if (PlayerPrefs.GetInt(ItemPage1WeaponsStrings[1], 0) == 0) ItemWeapon1(false);
        else ItemWeapon1(true);
        if (PlayerPrefs.GetInt(ItemPage1WeaponsStrings[2], 0) == 0) ItemWeapon2(false);
        else ItemWeapon2(true);
        if (PlayerPrefs.GetInt(ItemPage1WeaponsStrings[3], 0) == 0) ItemWeapon3(false);
        else ItemWeapon3(true);
        if (PlayerPrefs.GetInt(ItemPage1WeaponsStrings[4], 0) == 0) ItemWeapon4(false);
        else ItemWeapon4(true);
        if (PlayerPrefs.GetInt(ItemPage1WeaponsStrings[5], 0) == 0) ItemWeapon5(false);
        else ItemWeapon5(true);
    }

    #region ITEM WEAPON 0
    private void ItemWeapon0(bool alreadybought)
    {
        ItemsPage1Weapons[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Weapon_0);
        ItemsPage1Weapons[0].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Weapon_0).ToString();
        if (alreadybought) forLoopItems(0);
    }
    public void ItemWeapon0Buy()
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Weapon_0) && PlayerPrefs.GetInt(ItemPage1WeaponsStrings[0], 0) == 0)
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Weapon_0));

            SaveGame.Save<int>("Attack", Item.GetDamage(Item.ItemType.Weapon_0));
            WindowAnnonce(Item.GetName(Item.ItemType.Weapon_0));
            PlayerPrefs.SetInt(ItemPage1WeaponsStrings[0], 1);
            forLoopItems(0);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Weapon_0) && PlayerPrefs.GetInt(ItemPage1WeaponsStrings[0], 0) == 0)
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Weapon_0));
        }
    }
    #endregion
    #region ITEM WEAPON 2
    private void ItemWeapon1(bool alreadybought)
    {
        ItemsPage1Weapons[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Weapon_1);
        ItemsPage1Weapons[1].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Weapon_1).ToString();
        if (alreadybought) forLoopItems(1);
    }
    public void ItemWeapon1Buy()
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Weapon_1) && PlayerPrefs.GetInt(ItemPage1WeaponsStrings[1], 0) == 0)
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Weapon_1));

            SaveGame.Save<int>("Attack", Item.GetDamage(Item.ItemType.Weapon_1));
            WindowAnnonce(Item.GetName(Item.ItemType.Weapon_1));
            PlayerPrefs.SetInt(ItemPage1WeaponsStrings[1], 1);
            forLoopItems(1);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Weapon_1) && PlayerPrefs.GetInt(ItemPage1WeaponsStrings[1], 0) == 0)
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Weapon_1));
        }
    }
    #endregion
    #region ITEM WEAPON 2
    private void ItemWeapon2(bool alreadybought)
    {
        ItemsPage1Weapons[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Weapon_2);
        ItemsPage1Weapons[2].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Weapon_2).ToString();
        if (alreadybought) forLoopItems(2);
    }
    public void ItemWeapon2Buy()
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Weapon_2) && PlayerPrefs.GetInt(ItemPage1WeaponsStrings[2], 0) == 0)
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Weapon_2));

            SaveGame.Save<int>("Attack", Item.GetDamage(Item.ItemType.Weapon_2));
            WindowAnnonce(Item.GetName(Item.ItemType.Weapon_2));
            PlayerPrefs.SetInt(ItemPage1WeaponsStrings[2], 1);
            forLoopItems(2);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Weapon_2) && PlayerPrefs.GetInt(ItemPage1WeaponsStrings[2], 0) == 0)
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Weapon_2));
        }
    }
    #endregion
    #region ITEM WEAPON 3
    private void ItemWeapon3(bool alreadybought)
    {
        ItemsPage1Weapons[3].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Weapon_3);
        ItemsPage1Weapons[3].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Weapon_3).ToString();
        if (alreadybought) forLoopItems(3);
    }
    public void ItemWeapon3Buy()
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Weapon_3) && PlayerPrefs.GetInt(ItemPage1WeaponsStrings[3], 0) == 0)
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Weapon_3));

            SaveGame.Save<int>("Attack", Item.GetDamage(Item.ItemType.Weapon_3));
            WindowAnnonce(Item.GetName(Item.ItemType.Weapon_3));
            PlayerPrefs.SetInt(ItemPage1WeaponsStrings[3], 1);
            forLoopItems(3);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Weapon_3) && PlayerPrefs.GetInt(ItemPage1WeaponsStrings[3], 0) == 0)
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Weapon_3));
        }
    }
    #endregion
    #region ITEM WEAPON 4
    private void ItemWeapon4(bool alreadybought)
    {
        ItemsPage1Weapons[4].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Weapon_4);
        ItemsPage1Weapons[4].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Weapon_4).ToString();
        if (alreadybought) forLoopItems(4);
    }
    public void ItemWeapon4Buy()
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Weapon_4) && PlayerPrefs.GetInt(ItemPage1WeaponsStrings[4], 0) == 0)
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Weapon_4));

            SaveGame.Save<int>("Attack", Item.GetDamage(Item.ItemType.Weapon_4));
            WindowAnnonce(Item.GetName(Item.ItemType.Weapon_4));
            PlayerPrefs.SetInt(ItemPage1WeaponsStrings[4], 1);
            forLoopItems(4);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Weapon_4) && PlayerPrefs.GetInt(ItemPage1WeaponsStrings[4], 0) == 0)
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Weapon_4));
        }
    }
    #endregion
    #region ITEM WEAPON 5
    private void ItemWeapon5(bool alreadybought)
    {
        ItemsPage1Weapons[5].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Item.GetName(Item.ItemType.Weapon_5);
        ItemsPage1Weapons[5].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Item.GetCost(Item.ItemType.Weapon_5).ToString();
        if (alreadybought) forLoopItems(5);
    }
    public void ItemWeapon5Buy()
    {
        if (SaveGame.Load<int>("CoinsAmount", 0) >= Item.GetCost(Item.ItemType.Weapon_5) && PlayerPrefs.GetInt(ItemPage1WeaponsStrings[5], 0) == 0)
        {
            SoundManager.PlaySFX("ItemBought", false, 0, .3f); // SOUND ITEMBOUGHT

            SaveGame.Save<int>("CoinsAmount", SaveGame.Load<int>("CoinsAmount") - Item.GetCost(Item.ItemType.Weapon_5));

            SaveGame.Save<int>("Attack", Item.GetDamage(Item.ItemType.Weapon_5));
            WindowAnnonce(Item.GetName(Item.ItemType.Weapon_5));
            PlayerPrefs.SetInt(ItemPage1WeaponsStrings[5], 1);
            forLoopItems(5);
        }
        else if (SaveGame.Load<int>("CoinsAmount", 0) < Item.GetCost(Item.ItemType.Weapon_5) && PlayerPrefs.GetInt(ItemPage1WeaponsStrings[5], 0) == 0)
        {
            WindowAnnonceNotEnoughtMoney(Item.GetName(Item.ItemType.Weapon_5));
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
            ItemsPage1Weapons[i].GetComponent<Button>().interactable = false;
        }
    }
}
