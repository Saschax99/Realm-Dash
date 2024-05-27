using System.Collections;
using UnityEngine;

public class EnemyFollow : MonoBehaviour // SPLITTED BECAUSE OF ANIMATOR CLITCH wont play animation and move transform pos...
{
    private float speed = 2.5f;
    private float stoppingDistance = 1.25f;

    private Transform playerTarget;
    [SerializeField] private Transform attackPoint;
    private float attackRange = 0.75f;
    public float EnemyAttackDamage;
    private Animator enemyAnimator;
    [SerializeField] private bool followXandY = true;

    private void Start()
    {
        if (transform.name == FindObjectOfType<EnemyMovement>().FlyingEnemyName)
        {
            EnemyAttackDamage = FindObjectOfType<GameManager>().EnemyAttack_Multiplier_FlyingEye;
            speed = 2.5f;
        }
        else if (transform.name == FindObjectOfType<EnemyMovement>().GoblinEnemyName)
        {
            EnemyAttackDamage = FindObjectOfType<GameManager>().EnemyAttack_Multiplier_Goblin;
            speed = 3.5f;
        }
        else if (transform.name == FindObjectOfType<EnemyMovement>().SkeletonEnemyName)
        {
            EnemyAttackDamage = FindObjectOfType<GameManager>().EnemyAttack_Multiplier_Skeleton;
            speed = 2f;
        }
        else if (transform.name == FindObjectOfType<EnemyMovement>().MushroomEnemyName)
        {
            EnemyAttackDamage = FindObjectOfType<GameManager>().EnemyAttack_Multiplier_Mushroom;
            speed = 2.25f;
        }
        else if (transform.name == FindObjectOfType<EnemyMovement>().Boss1EnemyName)
        {
            EnemyAttackDamage = FindObjectOfType<GameManager>().EnemyAttack_Multiplier_Boss1;
            speed = 4f;
        }
        else if (transform.name == FindObjectOfType<EnemyMovement>().Boss2EnemyName)
        {
            EnemyAttackDamage = FindObjectOfType<GameManager>().EnemyAttack_Multiplier_Boss2;
            speed = 4f;
        }

        if (!followXandY) // eventuell noch überarbeiten
        {
            if (gameObject.tag == "Skeleton") transform.position = new Vector3(transform.position.x, .19f, transform.position.z); // pos for skeleton

            else transform.position = transform.position; // otherwise
        }
        else transform.position = transform.position; // otherwise


        enemyAnimator = GetComponentInChildren<Animator>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        Vector3 lookAtPosition = playerTarget.position;
        lookAtPosition.y = transform.position.y;
        transform.LookAt(lookAtPosition);
        if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (Vector2.Distance(transform.position, playerTarget.position) >= stoppingDistance)
            {
                if (!followXandY) enemyAnimator.SetBool("isRunning", true); // set is running for models which can run

                if (followXandY)
                {
                    transform.position = Vector2.MoveTowards(transform.position, playerTarget.transform.position, speed * Time.deltaTime);
                }
                else if (!followXandY)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerTarget.position.x, transform.position.y), speed * Time.deltaTime);
                }
            }
            else if (Vector2.Distance(transform.position, playerTarget.position) <= stoppingDistance)
            {
                if (!followXandY) enemyAnimator.SetBool("isRunning", false);

                if (!AttackBool && !FindObjectOfType<Player_Controller>().playerDead)
                {
                    StartCoroutine(AttackEnum());
                    AttackBool = true; // execute only once
                }
            }
        }
    }

    bool AttackBool = false;
    private IEnumerator AttackEnum()
    {
        if (!AttackBool)
        {
            yield return new WaitForSeconds(.2f); // wait .2f sec that player can react

            if (Vector2.Distance(transform.position, playerTarget.position) <= stoppingDistance)
            {
                AttackBool = true;
                enemyAnimator.SetTrigger("Attack");

                Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
                foreach (Collider2D enemy in hitPlayer)
                {
                    if (enemy.name == "Player")
                    {
                        FindObjectOfType<Player_Controller>().TakeDamage(EnemyAttackDamage);
                    }
                }
                yield return new WaitForSeconds(.3f);
                SoundManager.PlaySFX("EnemyAttack", false, 0, .3f); // SOUND PLAYERATTACK
                yield return new WaitForSeconds(0.45f); // duration of attackAnim
                AttackBool = false;
            }
            else
            {
                AttackBool = false;
                yield break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;


        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}