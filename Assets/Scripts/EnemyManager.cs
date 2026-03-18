using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    

    public List<Enemy> allEnemyList = new List<Enemy>();
    public EnemyData[] enemyDataList;
    public Transform[] path;

    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject EnemySpawn;

    private List<EnemyData> levelOneEnemyList = new List<EnemyData>();
    private List<EnemyData> levelTwoEnemyList = new List<EnemyData>();
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        foreach (var enemy in enemyDataList)
        {
            switch (enemy.difficulty)
            {
                case(1):
                    levelOneEnemyList.Add(enemy);
                    break;
                case(2):
                    levelTwoEnemyList.Add(enemy);
                    break;
            }
        }
    }
    

    private Stack<EnemyData> GenerateWave(int _score, int _maxDifficulty)
    {
        Stack<EnemyData> wave = new Stack<EnemyData>();
        while (_score >=0)
        {
            switch (_score)
            {
                case(>50):
                    _score -= 4;
                    break;
                case(>25):
                    _score -= 3;
                    break;
               case(>5):
                   wave.Push(levelTwoEnemyList[Random.Range(0,levelTwoEnemyList.Count)]);
                   _score -= 2;
                   break; 
                default:
                    wave.Push(levelOneEnemyList[Random.Range(0,levelOneEnemyList.Count)]);
                    _score -= 1;
                    break;
            }
        }
        return wave;
    }

    public void SpawnWave(int _score, int _maxDifficulty)
    {
        Stack<EnemyData> wave = GenerateWave(_score, _maxDifficulty);
        StartCoroutine(SpawnEnemy(wave));
    }

    private IEnumerator SpawnEnemy(Stack<EnemyData> enemyList)
    {
        foreach (var enemy in enemyList)
        {
            yield return new WaitForSeconds(1.0f);
            SpawnEnemy(enemy);
        }

        GameManager.instance.waveIsSpawning = false;
    }
    private void SpawnEnemy(EnemyData enemyToSpawn)
    {
        GameObject newEnemy = Instantiate(EnemyPrefab, EnemySpawn.transform.position, Quaternion.identity);
        Enemy enemyData = newEnemy.GetComponent<Enemy>();
        enemyData.data = enemyToSpawn;
    }
}

public enum EnemyType
{
    tungTungTungSahur = 0,
    tralaleroTralala = 1,
    brrBrrBicusDicusBombicus = 2
}