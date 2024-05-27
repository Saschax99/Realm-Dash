using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private GameObject[] Bosses;
    [SerializeField] private GameObject Textannouncement;
    public GameObject Coin;

    private Vector3[] Spawns = new Vector3[3];

    public int enemySpawnAmount, enemiesKilled, enemyBossSpawnAmount = 0;
    private int waveNumber = 1;
    private int waveBossChecker = 5;
    private Text TextCoinsAmount;


    #region Wave 1 standard Values
    [HideInInspector] public float EnemyAttack_Multiplier_FlyingEye = 10; // attack
    [HideInInspector] public float EnemyAttack_Multiplier_Goblin = 30; // attack
    [HideInInspector] public float EnemyAttack_Multiplier_Skeleton = 20; // attack
    [HideInInspector] public float EnemyAttack_Multiplier_Mushroom = 25; // attack

    [HideInInspector] public float EnemyAttack_Multiplier_Boss1 = 50; // attack
    [HideInInspector] public float EnemyAttack_Multiplier_Boss2 = 55; // attack

    [HideInInspector] public float EnemyHPMultiplier_FlyingEye = 80; // enemyHP
    [HideInInspector] public float EnemyHPMultiplier_Goblin = 120; // enemyHP
    [HideInInspector] public float EnemyHPMultiplier_Skeleton = 180; // enemyHP
    [HideInInspector] public float EnemyHPMultiplier_Mushroom = 150; // enemyHP

    [HideInInspector] public float EnemyHPMultiplier_Boss1 = 600; // enemyHP
    [HideInInspector] public float EnemyHPMultiplier_Boss2 = 600; // enemyHP
    #endregion

    private void Awake()
    {
        // for the first load needs to sign the spawns.. otherwise enemy would spawn at 0,0,0
        Spawns[0] = GameObject.Find("Main Camera/SpawnPointLeft").transform.position;
        Spawns[1] = GameObject.Find("Main Camera/SpawnPointRight").transform.position;
        Spawns[2] = GameObject.Find("Main Camera/SpawnPointAbove").transform.position;

        if (Time.timeScale != 1) Time.timeScale = 1;// if quit game or restart game in pause menu
    }
    private void Start()
    {
        //TextCoinsAmount = transform.Find("Grid_Softcurrencies/Resource_Coins/Panel_Bar/Text_CoinsAmount").GetComponent<Text>();

        //if (SaveGame.Exists("CoinsAmount"))
        //{
        //    if (TextCoinsAmount.text != SaveGame.Load<int>("CoinsAmount", 0).ToString())
        //    {
        //        TextCoinsAmount.text = SaveGame.Load<int>("CoinsAmount", 0).ToString();
        //    }
        //}

        StartCoroutine(StartWave());
    }

    private void Update()
    {
        Spawns[0] = GameObject.Find("Main Camera/SpawnPointLeft").transform.position;
        Spawns[1] = GameObject.Find("Main Camera/SpawnPointRight").transform.position;
        Spawns[2] = GameObject.Find("Main Camera/SpawnPointAbove").transform.position;
    }
    public void CheckEnemiesKilled()
    {
        if (enemiesKilled >= enemySpawnAmount && enemyBossSpawnAmount == 0)
        {
            StartCoroutine(NextWave());
        }
    }
    private void SpawnEnemy()
    {
        var enemy = Random.Range(0, Enemies.Length);
        if (enemy >= 1) Instantiate(Enemies[enemy], Spawns[Random.Range(0, Spawns.Length - 1)], Quaternion.identity);// land Enemies
        else if (enemy <= 0) Instantiate(Enemies[enemy], Spawns[Random.Range(0, Spawns.Length)], Quaternion.identity);// flying Enemies
    }

    private IEnumerator StartWave()
    {
        waveNumber = 1;
        enemySpawnAmount = 3;
        enemiesKilled = 0;
        yield return new WaitForSeconds(.5f);
        Textannouncement.GetComponent<Animator>().SetTrigger("Show");
        Textannouncement.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "Wave " + waveNumber;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < enemySpawnAmount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(.1f);
        }
    }

    public IEnumerator NextWave()
    {
        // DMG
        EnemyAttack_Multiplier_FlyingEye *= 1.15f;
        EnemyAttack_Multiplier_Goblin *= 1.15f;
        EnemyAttack_Multiplier_Skeleton *= 1.15f;
        EnemyAttack_Multiplier_Mushroom *= 1.15f;

        EnemyAttack_Multiplier_Boss1 *= 1.2f;
        EnemyAttack_Multiplier_Boss2 *= 1.2f;

        // HP
        EnemyHPMultiplier_FlyingEye *= 1.15f;
        EnemyHPMultiplier_Goblin *= 1.15f;
        EnemyHPMultiplier_Skeleton *= 1.15f;
        EnemyHPMultiplier_Mushroom *= 1.15f;

        EnemyHPMultiplier_Boss1 *= 1.2f;
        EnemyHPMultiplier_Boss2 *= 1.2f;

        waveNumber++;
        enemyBossSpawnAmount = 0;

        if (waveNumber == waveBossChecker)
        {
            enemyBossSpawnAmount = 1;
            waveBossChecker += 5;
        }
        enemySpawnAmount += 1;


        enemiesKilled = 0;
        yield return new WaitForSeconds(2f);
        Textannouncement.GetComponent<Animator>().SetTrigger("Show");

        if (waveNumber == waveBossChecker -5) Textannouncement.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "Boss Wave " + waveNumber; // -5 because increased value above

        else Textannouncement.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "Wave " + waveNumber;

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < enemySpawnAmount; i++)
        {
            var enemy = Random.Range(0, Enemies.Length); // what enemy

            if (enemy >= 1)
            {
                var spawnPoint = Random.Range(0, Spawns.Length - 1);
                Instantiate(Enemies[enemy], Spawns[spawnPoint], Quaternion.identity);// land Enemies
            }

            else if (enemy <= 0)
            {
                var spawnPoint = Random.Range(0, Spawns.Length);
                Instantiate(Enemies[enemy], Spawns[spawnPoint], Quaternion.identity);// flying Enemies
            }
            yield return new WaitForSeconds(.1f);
        }

        if (enemyBossSpawnAmount >= 1)
        {
            for (int i = 0; i < enemyBossSpawnAmount; i++)
            {
                var enemy = Random.Range(0, Bosses.Length); // ONLY BOSSES RANDOM
                var spawnPoint = Random.Range(0, Spawns.Length - 1); // ONLY LEFT AND RIGHT SPAWN
                Instantiate(Bosses[enemy], Spawns[spawnPoint], Quaternion.identity);
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}
