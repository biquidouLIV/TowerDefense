using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    

    public List<Enemy> allEnemyList = new List<Enemy>();
    public EnemyData[] enemyDataList;
    public Transform[] path;

    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject EnemySpawn;
    
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

    private void Update()
    {
        if (Keyboard.current[Key.R].wasPressedThisFrame)
        {
            GameObject newEnemy = Instantiate(EnemyPrefab, EnemySpawn.transform.position, Quaternion.identity);
            Enemy enemyData = newEnemy.GetComponent<Enemy>();
            enemyData.data = enemyDataList[0];
        }
    }

    private void SpawnEnemy()
    {
        
    }
}

public enum EnemyType
{
    kanard = 0,
    vache = 1
}