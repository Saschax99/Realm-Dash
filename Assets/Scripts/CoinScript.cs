using UnityEngine;
using BayatGames.SaveGameFree;

public class CoinScript : MonoBehaviour
{
    private int despawnCounter = 15;
    private bool pickedUp = false;
    private GameObject targetPosCoin, UICoinAnim;
    //private float timeStamp = 0;
    //private bool flyToPlayer = false;

    //private GameObject Player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!pickedUp)
        {
            if (collision.gameObject.tag == "Player") // if player got hit by it
            {
                pickedUp = true; //PickupAnimation();

                SoundManager.PlaySFX("CoinsCollect", false, 0, .3f); // SOUND COINSCOLLECT
                FindObjectOfType<UpdateCurrencies>().CoinsValue++;
                // pickup sound
            }
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.transform.GetComponent<Rigidbody2D>());
            this.transform.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pickedUp)
        {
            if (collision.gameObject.tag == "Player") // after setting gameobject as trigger and remove rb2d
            {
                pickedUp = true;

                SoundManager.PlaySFX("CoinsCollect", false, 0, .3f); // SOUND COINSCOLLECT
                FindObjectOfType<UpdateCurrencies>().CoinsValue++;
                // pickup sound
            }
        }

        //if (collision.gameObject.tag == "CoinsMagnet")
        //{
        //    timeStamp = Time.time;
        //    flyToPlayer = true;
        //}

    }

    private void FixedUpdate()
    {
        if (pickedUp)
        {
            if (Vector3.Distance(this.transform.position, targetPosCoin.transform.position) <= .5f)
            {
                UICoinAnim.GetComponent<Animator>().SetTrigger("pickedCoin");
                Destroy(this.gameObject);
            }
            else if (Vector3.Distance(this.transform.position, targetPosCoin.transform.position) >= .5f)
            {
                this.transform.position = Vector3.MoveTowards(transform.position, targetPosCoin.transform.position, 25 * Time.deltaTime);
            }
        }
    }

    private void Start()
    {
        startTime = Time.time;

        UICoinAnim = GameObject.Find("Canvas/HomeScreen/Resources/Grid_Softcurrencies/Resource_Coins/Panel_Bar/Image_Coin");
        targetPosCoin = GameObject.Find("Main Camera/CoinsInhaler");
    }
    float startTime, elapsedTime;
    private void Update()
    {
        //Player = GameObject.FindGameObjectWithTag("Player"); // search player pos

        elapsedTime = Time.time - startTime;
        if (!pickedUp)
        {
            if (elapsedTime >= despawnCounter - 5) GetComponent<Animator>().SetBool("despawn", true);
            if (elapsedTime >= despawnCounter) Destroy(this.gameObject);
        }


        //if (flyToPlayer && !pickedUp)
        //{
        //    transform.GetComponent<Rigidbody2D>().velocity = new Vector2(Player.transform.position.x, Player.transform.position.y) * (Time.time / timeStamp);
        //}
    }
}