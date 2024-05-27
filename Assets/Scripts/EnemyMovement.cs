using System.Collections;
using UnityEngine;
using BayatGames.SaveGameFree;

public class EnemyMovement : MonoBehaviour
{
    private float moveSpeed = -2.25f;
    public float maxHealth;
    private float currentHealth;

    [SerializeField] private Animator animator;

    [SerializeField] private GameObject PopupHealText;

    bool dead = false;

    [HideInInspector] public string FlyingEnemyName = "FlyingEnemy(Clone)";
    [HideInInspector] public string GoblinEnemyName = "GoblinEnemyParent(Clone)";
    [HideInInspector] public string SkeletonEnemyName = "SkeletonEnemyParent(Clone)";
    [HideInInspector] public string MushroomEnemyName = "MushroomEnemyParent(Clone)";
    [HideInInspector] public string Boss1EnemyName = "Boss1EnemyParent(Clone)";
    [HideInInspector] public string Boss2EnemyName = "Boss2EnemyParent(Clone)";

    private void Start()
    {
        if (transform.parent.name == FlyingEnemyName)
        {
            maxHealth = FindObjectOfType<GameManager>().EnemyHPMultiplier_FlyingEye;
        }
        else if (transform.parent.name == GoblinEnemyName)
        {
            maxHealth = FindObjectOfType<GameManager>().EnemyHPMultiplier_Goblin;
        }
        else if (transform.parent.name == SkeletonEnemyName)
        {
            maxHealth = FindObjectOfType<GameManager>().EnemyHPMultiplier_Skeleton;
        }
        else if (transform.parent.name == MushroomEnemyName)
        {
            maxHealth = FindObjectOfType<GameManager>().EnemyHPMultiplier_Mushroom;
        }
        else if (transform.parent.name == Boss1EnemyName)
        {
            maxHealth = FindObjectOfType<GameManager>().EnemyHPMultiplier_Boss1;
        }
        else if (transform.parent.name == Boss2EnemyName)
        {
            maxHealth = FindObjectOfType<GameManager>().EnemyHPMultiplier_Boss2;
        }

        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        SoundManager.PlaySFX("PlayerDied", false, 0, .3f); // SOUND ITEMBOUGHT
        if (transform.parent.tag == "Boss") FindObjectOfType<GameManager>().enemyBossSpawnAmount--;

        if (SaveGame.Exists("KillsAmount")) SaveGame.Save<int>("KillsAmount", SaveGame.Load<int>("KillsAmount", 0) + 1);

        FindObjectOfType<GameManager>().enemiesKilled++;
        FindObjectOfType<GameManager>().CheckEnemiesKilled();

        GameObject Popup = Instantiate(PopupHealText, FindObjectOfType<Player_Controller>().transform.position, Quaternion.Euler(0, 0, -13.37f)); // make a gameobject popup to get specific textpopup for the text
        Popup.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + (FindObjectOfType<EnemyFollow>().EnemyAttackDamage * 2).ToString("0");

        if (FindObjectOfType<Player_Controller>().currentHealth < FindObjectOfType<Player_Controller>().maxHealth) // heal if hp is less then maxhealth
        {
            FindObjectOfType<Player_Controller>().currentHealth = FindObjectOfType<Player_Controller>().currentHealth + FindObjectOfType<EnemyFollow>().EnemyAttackDamage * 2;
        }

        Vector3 trajectory = Random.insideUnitSphere * Random.Range(250f, 350f);

        if (transform.parent.tag == "Boss")
        {
            for (int i = 0; i <= Random.Range(10, 15); i++)
            {
                GameObject coin = Instantiate(FindObjectOfType<GameManager>().Coin, transform.position, Quaternion.identity);
                coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, 300f) + trajectory.x, 300f + trajectory.y));
            }
        }
        else
        {
            for (int i = 0; i <= Random.Range(2, 7); i++)
            {
                GameObject coin = Instantiate(FindObjectOfType<GameManager>().Coin, transform.position, Quaternion.identity);
                coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, 300f) + trajectory.x, 300f + trajectory.y));
            }
        }
        animator.SetBool("isDead", true);
        StartCoroutine(MoveToGround());
        GetComponentInParent<EnemyFollow>().enabled = false; // maybe delete
        this.GetComponent<Collider2D>().enabled = false; // maybe delete
        Invoke("DestroyGameObject", 3.5f); // destroy enemy in 3.5f sec
    }

    private void DestroyGameObject()
    {
        Destroy(transform.parent.gameObject);
    }
    private IEnumerator MoveToGround()
    {
        var targetPos = new Vector2();
        // heights for
        if (transform.parent.name == FlyingEnemyName) targetPos = new Vector2(transform.position.x, -.5f);
        else if (transform.parent.name == GoblinEnemyName) targetPos = new Vector2(transform.position.x, -.75f);
        else if (transform.parent.name == SkeletonEnemyName) targetPos = new Vector2(transform.position.x, -.55f);
        else if (transform.parent.name == MushroomEnemyName) targetPos = new Vector2(transform.position.x, -.25f);
        else if (transform.parent.name == Boss1EnemyName) targetPos = new Vector2(transform.position.x, -.25f);
        else if (transform.parent.name == Boss2EnemyName) targetPos = new Vector2(transform.position.x, -.25f);

        for (int i = 0; i < 100; i++)
        {
            if (transform.position.y <= targetPos.y) break;

            transform.position = Vector2.MoveTowards(transform.position, targetPos, 5 * Time.deltaTime);
            yield return new WaitForSeconds(.0125f);
        }
    }
}