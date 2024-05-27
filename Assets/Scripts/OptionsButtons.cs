using UnityEngine;
using UnityEngine.UI;
public class OptionsButtons : MonoBehaviour
{
    public Sprite Actived, Deactived;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Music", 1) == 1) // THIS COULD BE INTO THE START OF CREATESAVEFILE BUT CANT ACCESS TO THIS SPRITE BECAUSE DISABLED GAMEOBJECT
        {
            GameObject.Find("OptionsMenu/WindowPopup/Window/Button_Music/Text/Image").GetComponent<Image>().sprite = Actived;
        }
        else if (PlayerPrefs.GetInt("Music", 0) == 0)
        {
            GameObject.Find("OptionsMenu/WindowPopup/Window/Button_Music/Text/Image").GetComponent<Image>().sprite = Deactived;
        }

        if (PlayerPrefs.GetInt("SFX", 1) == 1)
        {
            GameObject.Find("OptionsMenu/WindowPopup/Window/Button_SFX/Text/Image").GetComponent<Image>().sprite = Actived;
        }
        else if (PlayerPrefs.GetInt("SFX", 0) == 0)
        {
            GameObject.Find("OptionsMenu/WindowPopup/Window/Button_SFX/Text/Image").GetComponent<Image>().sprite = Deactived;
        }
    }

    public void CloseOptions()
    {
        GameObject.Find("Canvas/Buttons/Button_Settings").GetComponent<Button>().interactable = true;
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
        GameObject.Find("OptionsMenu/WindowPopup/Window").GetComponent<Animator>().SetTrigger("Close");
        Invoke("DeactivateOptions", .583f);
    }
    private void DeactivateOptions()
    {
        FindObjectOfType<StartScreenButtons>().OptionsMenu.SetActive(false);
    }
    public void MusicSwitch()
    {
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON

        if (PlayerPrefs.GetInt("Music", 0) == 1)
        {
            GameObject.Find("OptionsMenu/WindowPopup/Window/Button_Music/Text/Image").GetComponent<Image>().sprite = Deactived;
            SoundManager.MuteMusic();
            PlayerPrefs.SetInt("Music", 0);
        }
        else if(PlayerPrefs.GetInt("Music", 0) == 0)
        {
            GameObject.Find("OptionsMenu/WindowPopup/Window/Button_Music/Text/Image").GetComponent<Image>().sprite = Actived;
            SoundManager.MuteMusic();
            PlayerPrefs.SetInt("Music", 1);
        }
    }
    public void SFXSwitch()
    {
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON

        if (PlayerPrefs.GetInt("SFX", 0) == 1)
        {
            GameObject.Find("OptionsMenu/WindowPopup/Window/Button_SFX/Text/Image").GetComponent<Image>().sprite = Deactived;
            SoundManager.MuteSFX();
            PlayerPrefs.SetInt("SFX", 0);
        }
        else if (PlayerPrefs.GetInt("SFX", 0) == 0)
        {
            GameObject.Find("OptionsMenu/WindowPopup/Window/Button_SFX/Text/Image").GetComponent<Image>().sprite = Actived;
            SoundManager.MuteSFX();
            PlayerPrefs.SetInt("SFX", 1);
        }
    }
    public void Credits()
    {
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
        transform.GetComponent<CreditsMenu>().CreditsGameobject.SetActive(true);
    }
}
