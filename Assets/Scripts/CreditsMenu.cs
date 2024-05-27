using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public GameObject CreditsGameobject;
    public void CreditMenuClose()
    {
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
        GameObject.Find("CreditsMenu/WindowPopup/Window").GetComponent<Animator>().SetTrigger("Close");
        Invoke("CloseCreditMenu", .563f);
    }
    private void CloseCreditMenu()
    {
        CreditsGameobject.SetActive(false);
    }
}
