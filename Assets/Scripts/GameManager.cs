using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        public static GameManager instance;

        public int money;
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private float _firstWaveTime = 5f;
        [SerializeField] private float _timeBetweenWaves = 20f;
        
        private int _currentWave = 1;
        private int _waveScore = 8;



        private void Start()
        {
                StartCoroutine(StartWave(_firstWaveTime));
        }

        private IEnumerator StartWave(float _waitTime)
        {
               Debug.Log(_currentWave);
               yield return new WaitForSeconds(_waitTime);
               EnemyManager.instance.SpawnWave(_waveScore,2);
               _waveScore += 3;
               StartCoroutine(StartWave(_timeBetweenWaves));
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
                moneyText.text = money.ToString();
        }


}
