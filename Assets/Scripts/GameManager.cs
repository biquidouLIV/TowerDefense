using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
        public static GameManager instance;

        public int money;
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private float _firstWaveTime = 5f;
        [SerializeField] private float _timeBetweenWaves = 20f;
        [SerializeField] private int stealPrize = 300;
        private int _currentWave = 1;
        private int _waveScore = 8;

        [SerializeField] private List<EnemyData> Allies = new List<EnemyData>();
        [SerializeField] private Image[] AlliesImage = new Image[4];



        private void Start()
        {
                StartCoroutine(StartWave(_firstWaveTime));
        }
        

        private IEnumerator StartWave(float _waitTime)
        {
               yield return new WaitForSeconds(_waitTime);
               EnemyManager.instance.SpawnWave(_waveScore,2);
               _currentWave++;
               _waveScore += 3;
               AlliesIncome();
               StartCoroutine(StartWave(_timeBetweenWaves));
        }


        public void StealEnemy()
        {
                if (money < stealPrize)
                {
                        return;
                }
                
                if (EnemyManager.instance.allEnemyList.Count == 0 || Allies.Count >= 4)
                {
                        return;
                }

                money -= stealPrize;
                Allies.Add(EnemyManager.instance.allEnemyList[0].data);
                EnemyManager.instance.allEnemyList[0].Die(false);
                DisplayAllies();
        }

        private void DisplayAllies()
        {
                foreach (var image in AlliesImage)
                {
                        image.sprite = null;
                }
                
                for (int i = 0; i < Allies.Count; i++)
                {
                        AlliesImage[i].sprite = Allies[i].sprite; 
                }
        }

        private void AlliesIncome()
        {
                List<EnemyData> escapes = new List<EnemyData>();
                foreach (var ally in Allies)
                {
                        money += ally.income;
                        if (Random.Range(0f, 1f) < ally.escape)
                        {
                                escapes.Add(ally);
                        }
                }

                foreach (var ally in escapes)
                {
                        Allies.Remove(ally);
                }
                DisplayAllies();
                
        }
        
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
                moneyText.text = money + "€";
        }


}
