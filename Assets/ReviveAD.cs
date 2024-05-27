using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class ReviveAD : MonoBehaviour, IUnityAdsListener
{
    private bool alreadyTookRevive = false;
    [SerializeField] private GameObject ReviveButton;

    private string PlacementID = "rewardedVideo";

#if UNITY_IOS
    private string gameId = "3722066";
#elif UNITY_ANDROID
    private string gameId = "3722067";
#endif

    void Start()
    {
        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId);

        // Set interactivity to be dependent on the Placement’s status:
        //ReviveButton.transform.GetChild(0).GetComponent<Button>().interactable = Advertisement.IsReady(PlacementID);
    }

    public void StartReviveAD()
    {
        if (!alreadyTookRevive)
        {
            if (Advertisement.IsReady(PlacementID))
            {
                Advertisement.Show(PlacementID);
            }
            else
            {
                ReviveButton.transform.GetChild(0).GetComponent<Button>().interactable = false;
            }
        }  
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            alreadyTookRevive = true;
            ReviveButton.transform.GetChild(0).GetComponent<Button>().interactable = false;
            Debug.Log("REWARD NOW");
            GameObject.Find("CanvasDead/WindowPopup").GetComponent<Animator>().SetTrigger("CloseUIPause");
            Invoke("DeactivateDelay", .417f);
            FindObjectOfType<Player_Controller>().RevivePlayer();
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("REWARDED NOT COMPLETED SKIPPED");
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
    private void DeactivateDelay()
    {
        GameObject.Find("CanvasDead").SetActive(false);
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == PlacementID)
        {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log(message);
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}