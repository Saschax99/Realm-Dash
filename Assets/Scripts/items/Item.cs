using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Armor_0,
        Armor_1,
        Armor_2,
        Armor_3,
        Armor_4,
        Armor_5,

        Weapon_0,
        Weapon_1,
        Weapon_2,
        Weapon_3,
        Weapon_4,
        Weapon_5,

        Health_1_500HP,
        //Coins_Magnet,

        Skin_0,
        Skin_1,
        Skin_2,
        Skin_3,

    }
    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Armor_0:          return 250;
            case ItemType.Armor_1:          return 500;
            case ItemType.Armor_2:          return 750;
            case ItemType.Armor_3:          return 1000;
            case ItemType.Armor_4:          return 1500;
            case ItemType.Armor_5:          return 4000;

            case ItemType.Weapon_0:         return 250;
            case ItemType.Weapon_1:         return 500;
            case ItemType.Weapon_2:         return 750;
            case ItemType.Weapon_3:         return 1000;
            case ItemType.Weapon_4:         return 1500;
            case ItemType.Weapon_5:         return 4000;

            case ItemType.Health_1_500HP:   return 50;
            //case ItemType.Coins_Magnet:     return 1250;
                

            case ItemType.Skin_0:           return 0;
            case ItemType.Skin_1:           return 1000;
            case ItemType.Skin_2:           return 2000;
            case ItemType.Skin_3:           return 3500;
        }
    }

    public static string GetName(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Armor_0:          return "damage reduction \n 95%";
            case ItemType.Armor_1:          return "damage reduction \n 90%";
            case ItemType.Armor_2:          return "damage reduction \n 85%";
            case ItemType.Armor_3:          return "damage reduction \n 80%";
            case ItemType.Armor_4:          return "damage reduction \n 75%";
            case ItemType.Armor_5:          return "damage reduction \n 60%";

            case ItemType.Weapon_0:         return "55 Attack \n Damage";
            case ItemType.Weapon_1:         return "65 Attack \n Damage";
            case ItemType.Weapon_2:         return "75 Attack \n Damage";
            case ItemType.Weapon_3:         return "80 Attack \n Damage";
            case ItemType.Weapon_4:         return "85 Attack \n Damage";
            case ItemType.Weapon_5:         return "100 Attack \n Damage";

            case ItemType.Health_1_500HP:   return "Health Pot medium";
           //case ItemType.Coins_Magnet:     return "Coins Magnet";

            case ItemType.Skin_0:           return "none";
            case ItemType.Skin_1:           return "red";
            case ItemType.Skin_2:           return "blue";
            case ItemType.Skin_3:           return "glow";
        }
    }
    public static int GetDamage(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Weapon_0: return 55;
            case ItemType.Weapon_1: return 65;
            case ItemType.Weapon_2: return 75;
            case ItemType.Weapon_3: return 80;
            case ItemType.Weapon_4: return 85;
            case ItemType.Weapon_5: return 100;
        }
    }
    public static float GetDefense(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Armor_0: return .95f;
            case ItemType.Armor_1: return .9f;
            case ItemType.Armor_2: return .85f;
            case ItemType.Armor_3: return .8f;
            case ItemType.Armor_4: return .75f;
            case ItemType.Armor_5: return .6f;
        }
    }
    public static int GetHealth(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Health_1_500HP:   return 500;
        }
    }
    public static int GetHealthMaxStack(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Health_1_500HP: return 5;
        }
    }
    public static string GetColorName(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Skin_0: return "none";
            case ItemType.Skin_1: return "red";
            case ItemType.Skin_2: return "blue";
            case ItemType.Skin_3: return "glow";
        }
    }
//    public static int GetMagnetValue(ItemType itemType)
//    {
//        switch (itemType)
//        {
//            default:
//            //case ItemType.Coins_Magnet: return 1;
//        }
//    }
}