using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    public GameObject ItemBoughtText; // references in itemXXXX Scripts
    private void Awake()
    {
        Time.timeScale = 1; // if player comes from ingame to the shop
    }
    public void BackToMenu()
    {
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
        GameObject.Find("Canvas/Button_Back").GetComponent<Animator>().SetTrigger("UI Back");
        SceneManager.LoadScene("StartScreen");
    }
    public void TabSound()
    {
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
    }
}
