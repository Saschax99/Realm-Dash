using UnityEngine;
using BayatGames.SaveGameFree;
using UnityEngine.SceneManagement;
public class PreStartScreen : MonoBehaviour
{
    private void Start()
    {
        //SaveGame.SavePath = SaveGamePath.PersistentDataPath;
        SaveGame.Encode = true;
        SceneManager.LoadScene(1);
    }
}

