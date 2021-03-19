using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    [Header("Current Wave Info")]
    [SerializeField] private int totalGuardian;
    [SerializeField] private int totalBat;
    [SerializeField] private int totalWolf;
    [SerializeField] private int totalWitch;
    [SerializeField] private int totalGolem;
    [SerializeField] private int totalSpawn;
    
    [Header("Enemys")]
    [SerializeField] private GameObject guardian;
    [SerializeField] private GameObject witch;
    [SerializeField] private GameObject bat;
    [SerializeField] private GameObject wolf;
    [SerializeField] private GameObject golem;
    
    [Header("Variables")]
    [SerializeField] private int currentWave;
    [SerializeField] int nextWave;
    [SerializeField] public float searchEnemys;
    [SerializeField] private float orignalSearch;
    [SerializeField] private bool spawningUnique;
    [SerializeField] private SpawnState state = SpawnState.WAITING;
    [SerializeField] private bool foundEnemy;
    [SerializeField] private bool newWave;

    public enum SpawnState {SPAWNING, WAITING}
    [SerializeField] public Wave[] waves;
    [SerializeField] public Transform[] spawnPoints;
    [SerializeField] public Transform spawns;
    
    [System.Serializable]
    public class Wave
    {
        [SerializeField] public GameObject enemyToSpawn;
        [SerializeField] public int minGuardian;
        [SerializeField] public int maxGuardian;
        [SerializeField] public int minWitch;
        [SerializeField] public int maxWitch;
        [SerializeField] public int minBat;
        [SerializeField] public int maxBat;
        [SerializeField] public int minWolf;
        [SerializeField] public int maxWolf;
        [SerializeField] public int maxGolem;
        [SerializeField] public int minGolem;
        [SerializeField] public float spawnerRate;
    }
    
    public void Start()
    {
        orignalSearch = searchEnemys;
    }

    public void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive() && !newWave)
            {
                WaveCompleted();
                newWave = true;
            }

            else
            {
                return;
            }
        }

        if (state != SpawnState.SPAWNING)
        {
            StartCoroutine(SpawnWave(waves[currentWave]));
        }
    }
    
    IEnumerator SpawnWave(Wave waveNumber)
    {
        GameManager.Instance.UpdateWave();
        state = SpawnState.SPAWNING;
        newWave = true;

        totalGuardian = Random.Range(waveNumber.minGuardian, waveNumber.maxGuardian);
        totalWitch = Random.Range(waveNumber.minWitch, waveNumber.maxWitch);
        totalBat = Random.Range(waveNumber.minBat, waveNumber.maxBat);
        totalWolf = Random.Range(waveNumber.minWolf, waveNumber.maxWolf);
        totalGolem = Random.Range(waveNumber.minGolem, waveNumber.maxGolem);
        totalSpawn = (totalBat + totalGolem + totalGuardian + totalWitch + totalWolf);
        
        for (int i = 0; i < totalSpawn; i++)
        {
            //ebug.Log(i);
            SpawnEnemy(waveNumber.enemyToSpawn);
            yield return new WaitForSeconds(1f * waveNumber.spawnerRate);
        }
        
        //Debug.Log("Finished Spawning");
        state = SpawnState.WAITING;
        newWave = false;
    }

    public void SpawnEnemy(GameObject enemyToSpawn)
    {
        if (totalGuardian > 0)
        {
            enemyToSpawn = guardian;
            spawns = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyToSpawn, spawns.position, spawns.rotation);
            totalGuardian = totalGuardian - 1;
            //Debug.Log(totalGuardian);
        }
        
        if (totalWitch > 0)
        {
            enemyToSpawn = witch;
            spawns = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyToSpawn, spawns.position, spawns.rotation);
            totalWitch = totalWitch - 1;
            //Debug.Log(totalGuardian);
        }
        
        if (totalBat > 0)
        {
            enemyToSpawn = bat;
            spawns = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyToSpawn, spawns.position, spawns.rotation);
            totalBat = totalBat - 1;
            //Debug.Log(totalGuardian);
        }
        
        if (totalWolf > 0)
        {
            enemyToSpawn = wolf;
            spawns = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyToSpawn, spawns.position, spawns.rotation);
            totalWolf = totalWolf - 1;
            //Debug.Log(totalGuardian);
        }
        
        if (totalGolem > 0)
        {
            enemyToSpawn = golem;
            spawns = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyToSpawn, spawns.position, spawns.rotation);
            totalGolem = totalGolem - 1;
            //Debug.Log(totalGuardian);
        }
    }

    public bool EnemyIsAlive()
    {
        searchEnemys -= Time.deltaTime;
        if (searchEnemys <= 0f)
        {
            //Debug.Log("Search");
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                //Debug.Log("Nothing is Alive");
                foundEnemy = false;
                return false;
            }
            
            if (GameObject.FindGameObjectWithTag("Enemy") != null)
            {
                //Debug.Log("Enemy Alive");
                foundEnemy = true;
            }
            searchEnemys = orignalSearch;
        }
        return true;
    }

    public void WaveCompleted()
    {
        currentWave = nextWave;
        GameManager.Instance.currentWave = currentWave;
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
        }
        nextWave += 1;
        
    }
}
