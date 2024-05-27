using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

public class Player_Controller : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody2D rb;
    private BoxCollider2D bc;

    [SerializeField] private Joystick joystickLeft;
    private Vector2 HorizontalMovement;

    [SerializeField] private float moveSpeed, jumpForce;
    [HideInInspector] public bool playerDead = false;

    private bool isGrounded, isGroundedDJump = false;
    private bool AttackBool = false;

    [SerializeField] private Transform attackPoint;
    private float attackRange = 1f;
    [SerializeField] private LayerMask enemyLayers;

    [HideInInspector] public float PlayerAttackDamage = 40;
    [HideInInspector] public float maxHealth = 1000;
    [HideInInspector] public float currentHealth;

    private float Defense = 1;
    private float MediumHealPots = 0;
    private int passivHeal = 5;

    private float attackRate = 2f;
    private float nextAttackTime, nextAttackTimeCombo = 0f;

    [HideInInspector] public GameObject PopupDamageText;
    bool hittedWallLeft, hittedWallRight = false;
    [SerializeField] private GameObject FireBall;

    private GameObject HealButton;

    [SerializeField] private GameObject HpBar;

    private float multiplierAbility1 = 1.75f;
    [SerializeField] private Image ImageAbility1;
    private float cooldownAbility1 = 5;
    private bool isCooldown1 = false;

    private string PlayerOutline = "none";
    private bool glowSkinEffect = false;
    private float timeleftGlowEffect = 0f;
    private float intensitySkin = 3f;

    [SerializeField] private GameObject WeaponInfo;

    private void Awake()
    {
        ImageAbility1.fillAmount = 0; // reset cooldown anim

        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        // loading stats
        currentHealth = maxHealth;

        Defense = SaveGame.Load<float>("Defense", 1);

        PlayerAttackDamage = SaveGame.Load<int>("Attack", 40);

        HealButton = GameObject.Find("Canvas/HomeScreen/Button_Heal");
        if (SaveGame.Exists("MaxStack500HP"))
        {
            MediumHealPots = SaveGame.Load<int>("MaxStack500HP", 0);
            if (MediumHealPots >= 1)
            {
                HealButton.SetActive(true);
                HealButton.transform.GetChild(1).GetComponent<Text>().text = SaveGame.Load<int>("MaxStack500HP", 0).ToString() + "/5";
            }
            else HealButton.SetActive(false);
        }
        else HealButton.SetActive(false);

        if (SaveGame.Exists("PlayerOutline"))
        {
            PlayerOutline = SaveGame.Load<string>("PlayerOutline", "none");
            switch (PlayerOutline)
            {
                case "red":
                    Debug.Log("red");
                    transform.GetComponent<Renderer>().material.SetFloat("Vector1_F95DEE98",0.01f); // THICKNESS
                    //transform.GetComponent<Renderer>().material.SetVector("Color_60AD3DD4", new Vector4(1,0,0,0)); // COLOR WITH VECTOR 4
                    transform.GetComponent<Renderer>().material.SetColor("Color_60AD3DD4", Color.red * intensitySkin); // COLOR WITH COLOR CLASS
                    break;
                case "blue":
                    Debug.Log("blue");
                    transform.GetComponent<Renderer>().material.SetFloat("Vector1_F95DEE98", 0.01f); // THICKNESS
                    transform.GetComponent<Renderer>().material.SetColor("Color_60AD3DD4", Color.blue * intensitySkin); // COLOR WITH COLOR CLASS
                    break;
                case "glow":
                    Debug.Log("glow");
                    transform.GetComponent<Renderer>().material.SetFloat("Vector1_F95DEE98", 0.02f); // THICKNESS
                    glowSkinEffect = true;
                    break;
                default:
                    Debug.Log("default");
                    transform.GetComponent<Renderer>().material.SetFloat("Vector1_F95DEE98", 0.0051f); // THICKNESS
                    transform.GetComponent<Renderer>().material.SetColor("Color_60AD3DD4", Color.white); // COLOR WITH COLOR CLASS
                    break;
            }
        }

        // LOADING WHICH WEAPON IS ACTIVE
        if (SaveGame.Load<bool>("WeaponCast", false)) WeaponInfo.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Weapon Cast";
        else WeaponInfo.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Weapon Sword";
    }
    float passivHealvalue = 0;
    private void FixedUpdate()
    {
        if (HpBar.transform.GetChild(0).GetComponent<Image>().fillAmount != currentHealth / 1000)
        {
            HpBar.transform.GetChild(0).GetComponent<Image>().fillAmount = currentHealth / 1000;

            if (currentHealth > maxHealth / 2)
            {
                HpBar.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = FindObjectOfType<HpBarAssets>().greenBar;
                if (playerAnimator.GetBool("isBlinking") == true) playerAnimator.SetBool("isBlinking", false);
                //bloom effect and vignette increasing but dont know atm how to access the script --> later
            }

            else if (currentHealth < maxHealth / 2 && currentHealth > maxHealth / 4)
            {
                HpBar.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = FindObjectOfType<HpBarAssets>().orangeBar;
                if (playerAnimator.GetBool("isBlinking") == true) playerAnimator.SetBool("isBlinking", false);
            }

            else if (currentHealth < maxHealth / 4)
            {
                HpBar.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = FindObjectOfType<HpBarAssets>().redBar;
                if (playerAnimator.GetBool("isBlinking") == false) playerAnimator.SetBool("isBlinking", true);

            }
        }
        if (!playerDead)
        {
            if (Time.time >= passivHealvalue)
            {
                if (currentHealth < maxHealth)
                {
                    currentHealth = currentHealth + passivHeal;
                }
                passivHealvalue = Time.time + 2; // every 2 sec heal
            }
        }
    }

    public void TakeDamage(float damage)
    {
        playerAnimator.SetTrigger("Hurt");

        currentHealth -= damage * Defense;
        if (currentHealth <= 0 && !playerDead)
        {
            SoundManager.PlaySFX("PlayerDied", false, 0, .3f); // SOUND PLAYERDIED
            playerAnimator.SetBool("isDead", true);
            playerDead = true;
            Invoke("delayedScreen", 2f);
        }
    }
    private void delayedScreen()
    {
        FindObjectOfType<CanvasButtons>().DeadCanvas.SetActive(true);
    }
    private void Update()
    {
        if (!playerDead)
        {
            #region Movement Horizontal
            // function for not clitching into the borders
            if (!hittedWallLeft && hittedWallRight) // hit right border
            {
                if (joystickLeft.Horizontal <= -.2f)
                {
                    MovePlayer(false); // to the left
                }

                else
                {
                    HorizontalMovement.x = 0f;
                    playerAnimator.SetBool("isRunning", false);
                }
            }
            if (hittedWallLeft && !hittedWallRight) // hit left border
            {
                if (joystickLeft.Horizontal >= .2f)
                {
                    MovePlayer(true); // to the right
                }
                else
                {
                    HorizontalMovement.x = 0f;
                    playerAnimator.SetBool("isRunning", false);
                }
            }
            if (!hittedWallLeft && !hittedWallRight) // hit nothing
            {
                if (joystickLeft.Horizontal >= .2f)
                {
                    MovePlayer(true); // to the right
                }
                else if (joystickLeft.Horizontal <= -.2f)
                {
                    MovePlayer(false); // to the left
                }
                else
                {
                    HorizontalMovement.x = 0f;
                    playerAnimator.SetBool("isRunning", false);
                }
            }
            #endregion

            #region Movement Vertical
            var verticalMove = joystickLeft.Vertical;
            if (verticalMove >= .5f && isGrounded)
            {
                transform.GetChild(2).GetComponent<ParticleSystem>().Play();
                playerAnimator.SetTrigger("jump");
                playerAnimator.SetBool("isJumping", true);

                Vector2 velocity = rb.velocity;
                velocity.y = jumpForce;
                rb.velocity = velocity;

                playerAnimator.SetBool("isGrounded", false); // ability merker
                isGrounded = false;
            }

            else if (verticalMove <= -.5f)
            {
                playerAnimator.SetBool("crouch", true); // activate crouch anim

                bc.offset = new Vector2(-0.02f, -0.09f); // Setting hitbox to normal
                bc.size = new Vector2(0.15f, 0.16f);
            }
            else
            {
                playerAnimator.SetBool("crouch", false);

                bc.offset = new Vector2(0, -0.03f); // Setting hitbox to normal
                bc.size = new Vector2(0.15f, 0.28f);
            }
            #endregion
        }

        if (isCooldown1) // Ability1 Cooldown
        {
            ImageAbility1.fillAmount -= 1 / cooldownAbility1 * Time.deltaTime;
            if (ImageAbility1.fillAmount <= 0)
            {
                ImageAbility1.fillAmount = 0;
                isCooldown1 = false;
            }
        }
        #region GlowEffect
        if (glowSkinEffect && Time.time >= timeleftGlowEffect)
        {
            Color newColor = new Color(
                Random.value,
                Random.value,
                Random.value,
                1);
            transform.GetComponent<Renderer>().material.SetVector("Color_60AD3DD4", newColor * intensitySkin);
            timeleftGlowEffect = Time.time + .1f;
        }
        #endregion
    }

    [HideInInspector] public Quaternion faceRight = new Quaternion(0, 0, 0, 0);
    [HideInInspector] public Quaternion faceLeft = new Quaternion(0, 180, 0, 0);
    private void MovePlayer(bool side) // true = right ; false = left
    {
        if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hawk_Attack") && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hawk_Attack2x") && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hawk_Attack3x") && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hawk_ComboAttack") && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hawk_AbilityAttack1-1") && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hawk_Shooting"))
        {
            HorizontalMovement.x = moveSpeed;
            transform.Translate(HorizontalMovement);
        }
        playerAnimator.SetBool("isRunning", true);

        if (side) transform.rotation = faceRight; // rotate player right

        else if (!side) transform.rotation = faceLeft; // rotate player left

        transform.GetChild(2).GetComponent<ParticleSystem>().Play(); // EVENTUELL ÄNDERN
    }

    public void Attack()
    {
        GameObject.Find("Canvas/HomeScreen/Button_Attack").GetComponent<Animator>().SetTrigger("Attack"); // attackButton Anim

        if (!SaveGame.Load<bool>("WeaponCast", false))
        {
            if (!playerDead && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hawk_Hurt"))
            {
                if (Time.time >= nextAttackTime)
                {
                    SoundManager.PlaySFX("PlayerAttack", false, 0, .3f); // SOUND PLAYERATTACK
                    Camera.main.GetComponent<Animator>().SetTrigger("earthQuake");

                    if (playerAnimator.GetBool("isJumping") == false)  // if not jumping continue
                    {
                        switch (Random.Range(0, 3)) // random Attack animations
                        {
                            case 0:
                                playerAnimator.SetTrigger("attack");
                                break;
                            case 1:
                                playerAnimator.SetTrigger("attack2x");
                                break;
                            case 2:
                                playerAnimator.SetTrigger("attack3x");
                                break;
                        }
                        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                        foreach (Collider2D enemy in hitEnemies)
                        {
                            enemy.GetComponent<EnemyMovement>().TakeDamage(PlayerAttackDamage);

                            var enemyPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);

                            GameObject Popup = Instantiate(PopupDamageText, enemy.transform.position, Quaternion.Euler(0, 0, -13.37f)); // make a gameobject popup to get specific textpopup for the text
                            Popup.transform.GetChild(0).GetComponent<TextMesh>().text = PlayerAttackDamage.ToString();
                        }
                        nextAttackTime = Time.time + 1f / attackRate;
                        nextAttackTimeCombo = Time.time + 1f / (attackRate * 2);
                    }
                    else if (playerAnimator.GetBool("isJumping") == true) // if player is jumping continue
                    {
                        playerAnimator.SetTrigger("jumpAttack");

                        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                        foreach (Collider2D enemy in hitEnemies)
                        {
                            enemy.GetComponent<EnemyMovement>().TakeDamage(PlayerAttackDamage);

                            var enemyPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);

                            GameObject Popup = Instantiate(PopupDamageText, enemy.transform.position, Quaternion.Euler(0, 0, -13.37f)); // make a gameobject popup to get specific textpopup for the text
                            Popup.transform.GetChild(0).GetComponent<TextMesh>().text = PlayerAttackDamage.ToString("0");
                        }
                        nextAttackTime = Time.time + 1f / attackRate;
                        nextAttackTimeCombo = Time.time + 1f / (attackRate * 2);
                    }
                }
                else if (Time.time >= nextAttackTimeCombo && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hawk_ComboAttack") && playerAnimator.GetBool("isJumping") == false)
                {
                    playerAnimator.SetTrigger("attackCombo");

                    StartCoroutine(comboAttack(.45f / 3));

                    nextAttackTime = Time.time + 1f / attackRate;
                    nextAttackTimeCombo = Time.time + 1f / (attackRate * 2);
                }
            }
        }
        else if (SaveGame.Load<bool>("WeaponCast", false))
        {
            if (!playerDead && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hawk_Hurt"))
            {
                if (Time.time >= nextAttackTime)
                {
                    SoundManager.PlaySFX("PlayerAttackCast", false, 0, .3f); // SOUND PLAYERATTACK
                    playerAnimator.SetTrigger("AttackCast");

                    StartCoroutine(AttackCast());
                    nextAttackTime = Time.time + 1f / attackRate;
                    nextAttackTimeCombo = Time.time + 1f / (attackRate * 2);
                }
            }
        }
    }
    private IEnumerator AttackCast()
    {
        yield return new WaitForSeconds(.45f); // delay because of animation
        Camera.main.GetComponent<Animator>().SetTrigger("earthQuake");

        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(attackPoint.position, attackPoint.right);
        foreach (RaycastHit2D hit in hitInfo)
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyMovement>().TakeDamage(PlayerAttackDamage / 2);
                var enemyPos = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z);

                GameObject Popup = Instantiate(PopupDamageText, hit.transform.position, Quaternion.Euler(0, 0, -13.37f)); // make a gameobject popup to get specific textpopup for the text
                Popup.transform.GetChild(0).GetComponent<TextMesh>().text = (PlayerAttackDamage / 2).ToString();

                //var newPosAttackPoint = new Vector3(attackPoint.transform.position.x, attackPoint.transform.position.y - .25f, attackPoint.transform.position.z);
                //transform.GetChild(3).GetComponent<LineRenderer>().SetPosition(0, newPosAttackPoint);
                //transform.GetChild(3).GetComponent<LineRenderer>().SetPosition(1, hit.point);
            }
            else
            {
                //var newPosAttackPoint = new Vector3(attackPoint.transform.position.x, attackPoint.transform.position.y - .25f, attackPoint.transform.position.z);
                //transform.GetChild(3).GetComponent<LineRenderer>().SetPosition(0, newPosAttackPoint);
                //transform.GetChild(3).GetComponent<LineRenderer>().SetPosition(1, attackPoint.position + attackPoint.right * 100);
            }
        }
        var newPosAttackPoint = new Vector3(attackPoint.transform.position.x, attackPoint.transform.position.y + .5f, attackPoint.transform.position.z);
        Instantiate(FireBall, newPosAttackPoint, attackPoint.rotation);
        transform.GetChild(3).GetComponent<LineRenderer>().enabled = true;
        yield return new WaitForSeconds(.02f);
        transform.GetChild(3).GetComponent<LineRenderer>().enabled = false;
    }
    #region Ability 1
    private bool Ability1InProcress = false;
    public void AbilityAttack()
    {
        if (!isCooldown1)
        {
            ImageAbility1.fillAmount = 1;
            GameObject.Find("Canvas/HomeScreen/Button_AttackAbility").GetComponent<Animator>().SetTrigger("Attack"); // attackButton Anim

            if (!playerDead && isGrounded)
            {
                Ability1InProcress = true;
                Vector2 velocity = rb.velocity;
                velocity.y = jumpForce;
                //if (transform.rotation == faceLeft) velocity.x = -jumpForce / 1.5f;
                //else velocity.x = jumpForce / 1.5f;
                rb.velocity = velocity;
                playerAnimator.SetBool("isGrounded", false); // ability merker
                isGrounded = false;
                playerAnimator.SetTrigger("AttackAbility");
            }
            else if (!playerDead && !isGrounded)
            {
                Ability1InProcress = true;
                playerAnimator.SetBool("isGrounded", false); // ability merker
                playerAnimator.SetTrigger("AttackAbility");
            }
            isCooldown1 = true;
        }
    }
    public void RevivePlayer() // REFERENCE IN REVIVEAD SCRIPT
    {
        playerDead = false;
        currentHealth = maxHealth;
        ImageAbility1.fillAmount = 1;
        Ability1InProcress = true;
        playerAnimator.SetBool("isGrounded", true); // ability merker
        playerAnimator.SetTrigger("crouch");
        playerAnimator.SetTrigger("AttackAbility");
       
        SoundManager.PlaySFX("PlayerAttack", false, 0, .5f); // SOUND PLAYERATTACK
        Camera.main.GetComponent<Animator>().SetTrigger("earthQuake");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange * 4, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyMovement>().TakeDamage(PlayerAttackDamage * multiplierAbility1 * 5);
            var enemyPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
            GameObject Popup = Instantiate(PopupDamageText, enemy.transform.position, Quaternion.Euler(0, 0, -13.37f)); // make a gameobject popup to get specific textpopup for the text
            Popup.transform.GetChild(0).GetComponent<TextMesh>().text = (PlayerAttackDamage * multiplierAbility1).ToString();
        }
        isCooldown1 = true;
    }
    private void AbilityAttackDealingDMG()
    {
        SoundManager.PlaySFX("PlayerAttack", false, 0, .45f); // SOUND PLAYERATTACK
        Camera.main.GetComponent<Animator>().SetTrigger("earthQuake");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange * 3, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyMovement>().TakeDamage(PlayerAttackDamage * multiplierAbility1);
            var enemyPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
            GameObject Popup = Instantiate(PopupDamageText, enemy.transform.position, Quaternion.Euler(0, 0, -13.37f)); // make a gameobject popup to get specific textpopup for the text
            Popup.transform.GetChild(0).GetComponent<TextMesh>().text = (PlayerAttackDamage * multiplierAbility1).ToString();
        }
    }
    #endregion

    private IEnumerator comboAttack(float delay)
    {
        for (int i = 0; i <= 2; i++)
        {
            SoundManager.PlaySFX("PlayerAttack", false, 0, .3f); // SOUND PLAYERATTACK
            Camera.main.GetComponent<Animator>().SetTrigger("earthQuake");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyMovement>().TakeDamage(PlayerAttackDamage);

                var enemyPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);

                GameObject Popup = Instantiate(PopupDamageText, enemy.transform.position, Quaternion.Euler(0, 0, -13.37f)); // make a gameobject popup to get specific textpopup for the text
                Popup.transform.GetChild(0).GetComponent<TextMesh>().text = PlayerAttackDamage.ToString();
            }
            yield return new WaitForSeconds(delay);
        }
    }

    public void Healing()
    {
        GameObject.Find("Canvas/HomeScreen/Button_Heal").GetComponent<Animator>().SetTrigger("Attack"); // Healbutton Anim
        if (MediumHealPots > 0)
        {
            SoundManager.PlaySFX("HealpotDrinking", false, 0, .3f); // SOUND ITEMBOUGHT
            currentHealth = currentHealth + Item.GetHealth(Item.ItemType.Health_1_500HP);

            SaveGame.Save<int>("MaxStack500HP", SaveGame.Load<int>("MaxStack500HP", 0) - 1);
            MediumHealPots = SaveGame.Load<int>("MaxStack500HP", 0);
            HealButton.transform.GetChild(1).GetComponent<Text>().text = SaveGame.Load<int>("MaxStack500HP", 0).ToString() + "/5";

            if (currentHealth > maxHealth) currentHealth = maxHealth;

            if (MediumHealPots < 1) GameObject.Find("Canvas/HomeScreen/Button_Heal").SetActive(false);
        }
    }
    public void ChangeWeapon()
    {
        SoundManager.PlaySFX("ButtonSound", false, 0, .3f); // SOUND ITEMBOUGHT
        if (!SaveGame.Load<bool>("WeaponCast", false))
        {
            SaveGame.Save<bool>("WeaponCast", true);
            WeaponInfo.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Weapon Cast";
            //WeaponInfo.GetComponent<Animator>().SetTrigger("Show");
        }
        else if(SaveGame.Load<bool>("WeaponCast", false))
        {
            SaveGame.Save<bool>("WeaponCast", false);
            WeaponInfo.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Weapon Sword";
            //WeaponInfo.GetComponent<Animator>().SetTrigger("Show");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (Ability1InProcress)
            {
                Ability1InProcress = false;
                AbilityAttackDealingDMG();
            }

            playerAnimator.SetBool("isJumping", false);
            playerAnimator.SetBool("isGrounded", true); // ability merker
            if (!isGrounded) isGrounded = true;
            if (!isGroundedDJump) isGroundedDJump = true;
        }
        if (collision.gameObject.tag == "WallLeft" || collision.gameObject.tag == "WallRight")
        {
            if (isGroundedDJump)
            {
                isGrounded = true; // reset isGrounded
                isGroundedDJump = false; // excute only once
            }           
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WallLeft")
        {
            hittedWallLeft = true;
        }
        if (collision.gameObject.tag == "WallRight")
        {
            hittedWallRight = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WallLeft")
        {
            hittedWallLeft = false;
        }
        if (collision.gameObject.tag == "WallRight")
        {
            hittedWallRight = false;
        }
    }
}