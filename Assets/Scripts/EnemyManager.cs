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

    private List<EnemyData> _levelOneEnemyList = new List<EnemyData>();
    private List<EnemyData> _levelTwoEnemyList = new List<EnemyData>();
    private List<EnemyData> _levelThreeEnemyList = new List<EnemyData>();
    private List<EnemyData> _levelFourEnemyList = new List<EnemyData>();

    [SerializeField] private float timeBetweenEnemies = 1.5f;
    
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
                    _levelOneEnemyList.Add(enemy);
                    break;
                case(2):
                    _levelTwoEnemyList.Add(enemy);
                    break;
                case(3):
                    _levelThreeEnemyList.Add(enemy);
                    break;
                case(4):
                    _levelFourEnemyList.Add(enemy);
                    break;
            }
        }
    }
    

    private Stack<EnemyData> GenerateWave(int _score)
    {
        Stack<EnemyData> wave = new Stack<EnemyData>();
        while (_score >=0)
        {
            switch (_score)
            {
                case(>40):
                    wave.Push(_levelFourEnemyList[Random.Range(0,_levelFourEnemyList.Count)]);
                    _score -= 4;
                    break;
                case(>20):
                    wave.Push(_levelThreeEnemyList[Random.Range(0,_levelThreeEnemyList.Count)]);
                    _score -= 3;
                    break;
               case(>5):
                   wave.Push(_levelTwoEnemyList[Random.Range(0,_levelTwoEnemyList.Count)]);
                   _score -= 2;
                   break; 
                default:
                    wave.Push(_levelOneEnemyList[Random.Range(0,_levelOneEnemyList.Count)]);
                    _score -= 1;
                    break;
            }
        }
        return wave;
    }

    public void SpawnWave(int _score)
    {
        Stack<EnemyData> wave = GenerateWave(_score);
        StartCoroutine(SpawnEnemy(wave));
    }

    private IEnumerator SpawnEnemy(Stack<EnemyData> enemyList)
    {
        foreach (var enemy in enemyList)
        {
            yield return new WaitForSeconds(timeBetweenEnemies);
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
    tungTungTungSahur,
    tralaleroTralala,
    brrBrrBicusDicusBombicus,
    laVacaSaturnoSaturnita,
    liriliLarila,
    frigoCamelo,
    chimpanziniBananini,
    cappucinoAssassino,
    sixSeven,
    matteo,
    odinDinDinDinDun,
    trippiTroppiTroppaTrippa,
    dragonCaneloni,
    blackHoleGoat,
    
}