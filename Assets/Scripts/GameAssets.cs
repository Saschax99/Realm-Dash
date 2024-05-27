using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;
    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }
    public Sprite StandardArmor;
    public Sprite Armor_1;
    public Sprite Armor_2;

    public Sprite StandardWeapon;
    public Sprite Weapon_1;
    public Sprite Weapon_2;

}
