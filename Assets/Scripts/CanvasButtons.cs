using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;
using UnityEngine.UI;

public class CanvasButtons : MonoBehaviour
{
    [SerializeField] private GameObject PauseCanvas, 
        TextWaveAnnonce; // IF PAUSE WHILE WAVE IS COMING
    public GameObject DeadCanvas;

    private void Start()
    {
        if (DeadCanvas.activeSelf) DeadCanvas.SetActive(false);
        if (PauseCanvas.activeSelf) PauseCanvas.SetActive(false);
    }

    public void Pause()
    {
        if (!PauseCanvas.activeSelf)
        {
            GameObject.Find("Canvas/HomeScreen/Button_Pause").GetComponent<Button>().interactable = false;
            SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
            SaveGame.Save<int>("CoinsAmount", FindObjectOfType<UpdateCurrencies>().CoinsValue); // SAVE VALUES
            GameObject.Find("Canvas/HomeScreen/Button_Pause").GetComponent<Animator>().SetTrigger("pauseButton");
            TextWaveAnnonce.SetActive(false);
            PauseCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Continue()
    {
        GameObject.Find("Canvas/HomeScreen/Button_Pause").GetComponent<Button>().interactable = true;
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
        StartCoroutine(delayWindow(.417f));
        TextWaveAnnonce.SetActive(true);
        TextWaveAnnonce.GetComponent<Animator>().SetTrigger("Show");
        Time.timeScale = 1;
    }
    private IEnumerator delayWindow(float delay)
    {
        GameObject.Find("CanvasPause/WindowPopup/Grid_LoginForm/Button_Continue").GetComponent<Animator>().SetTrigger("UI Continue"); // button
        GameObject.Find("CanvasPause/WindowPopup").GetComponent<Animator>().SetTrigger("CloseUIPause"); // window close
        yield return new WaitForSeconds(delay); // wait delay to disable window
        PauseCanvas.SetActive(false);
    }
    public void Restart()
    {
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
        if (PauseCanvas.activeSelf) GameObject.Find("CanvasPause/WindowPopup/Grid_LoginForm/Button_Restart").GetComponent<Animator>().SetTrigger("UI Restart");

        else if (DeadCanvas.activeSelf) GameObject.Find("CanvasDead/WindowPopup/Grid_LoginForm/Button_Restart").GetComponent<Animator>().SetTrigger("UI Restart");

        SceneManager.LoadScene("Game");
    }
    public void ShopLoad()
    {
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
        if (PauseCanvas.activeSelf) GameObject.Find("CanvasPause/WindowPopup/Grid_LoginForm/Button_Shop").GetComponent<Animator>().SetTrigger("UI Shop");

        else if (DeadCanvas.activeSelf) GameObject.Find("CanvasDead/WindowPopup/Grid_LoginForm/Button_Shop").GetComponent<Animator>().SetTrigger("UI Shop");

        SceneManager.LoadScene("Shop");
    }
    public void Quit()
    {
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND BUTTON
        if (PauseCanvas.activeSelf) GameObject.Find("CanvasPause/WindowPopup/Grid_LoginForm/Button_Quit").GetComponent<Animator>().SetTrigger("UI Quit");

        else if (DeadCanvas.activeSelf) GameObject.Find("CanvasDead/WindowPopup/Grid_LoginForm/Button_Quit").GetComponent<Animator>().SetTrigger("UI Quit");

        SceneManager.LoadScene("StartScreen");
    }
}
