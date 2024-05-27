using UnityEngine;

public class RestorePurchases : MonoBehaviour
{
    void Start()
    {
        if (Application.platform != RuntimePlatform.IPhonePlayer || Application.platform != RuntimePlatform.OSXPlayer)
        {
            gameObject.SetActive(false);
        }
    }

    public void ClickRestorePurchaseButton()
    {
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
        //IAPManager.instance.RestorePurchases();
    }
}
