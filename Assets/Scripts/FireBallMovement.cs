using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    private float speed = 35;
    Quaternion FaceDirection;
    Quaternion PlayerRotation;
    private void Start()
    {
        FaceDirection = FindObjectOfType<Player_Controller>().faceLeft;
        PlayerRotation = FindObjectOfType<Player_Controller>().transform.rotation;

        if (PlayerRotation == FaceDirection)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }
    private void Update()
    {
        if (PlayerRotation == FaceDirection)
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, transform.position.y);
        }
        else transform.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, transform.position.y);

    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
