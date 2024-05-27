using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenButtons : MonoBehaviour
{
    public GameObject OptionsMenu;

    private void Start()
    {
        Time.timeScale = 1; // if player comes from ingame to the StartScreen

        #region Load Sound Stats
        if (PlayerPrefs.GetInt("Music", 1) == 1)
        {
            if (SoundManager.IsMusicMuted()) // IF MUTED
            {
                SoundManager.MuteMusic();
            }
        }
        else if (PlayerPrefs.GetInt("Music", 1) == 0)
        {
            if (!SoundManager.IsMusicMuted()) // IF NOT MUTED
            {
                SoundManager.MuteMusic();
            }
        }

        if (PlayerPrefs.GetInt("SFX", 1) == 1)
        {
            if (SoundManager.IsSFXMuted()) // IF MUTED
            {
                SoundManager.MuteSFX();
            }
        }
        else if (PlayerPrefs.GetInt("SFX", 1) == 0)
        {
            if (!SoundManager.IsSFXMuted()) // IF NOT MUTED
            {
                SoundManager.MuteSFX();
            }
        }
        #endregion
    }

    public void StartGame()
    {
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
        GameObject.Find("Canvas/Buttons/Grid_Buttons/Button_Start").GetComponent<Animator>().SetTrigger("UI Start");
        SceneManager.LoadScene("Game");
    }
    public void ShopLoad()
    {
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
        GameObject.Find("Canvas/Buttons/Grid_Buttons/Button_Shop").GetComponent<Animator>().SetTrigger("UI Shop");
        SceneManager.LoadScene("Shop");
    }
    public void Options()
    {
        if (!OptionsMenu.activeSelf)
        {
            GameObject.Find("Canvas/Buttons/Button_Settings").GetComponent<Button>().interactable = false;
            SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
            OptionsMenu.SetActive(true);
            GameObject.Find("Canvas/Buttons/Button_Settings").GetComponent<Animator>().SetTrigger("UI Options");
        }
    }
}
