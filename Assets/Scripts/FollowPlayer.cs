using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        if (player.transform.position.x < -2.55f) // left side
        {
            // stay camera pos
        } else if (player.transform.position.x >= 2.55f) // right side
        {
            //stay camera pos
        }
        else
        { // follow player with camera
            Vector3 position = transform.position;
            position.x = player.position.x;
            transform.position = position;
        }
    }
}
