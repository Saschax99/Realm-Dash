using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> Pages;

    public void NextPage()
    {
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
        Pages[1].SetActive(true);
        Pages[0].SetActive(false);
    }
    public void PrevPage()
    {
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
        Pages[0].SetActive(true);
        Pages[1].SetActive(false);
    }
}
