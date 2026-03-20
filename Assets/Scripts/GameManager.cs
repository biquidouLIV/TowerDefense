using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
        public static GameManager instance;

        [Header("data")]
                public int money;
                [SerializeField] private int life = 20;
                [SerializeField] private float _timeBetweenWaves = 5f;
        
        [Header("Refs")]
                [SerializeField] private TMP_Text moneyText;
                [SerializeField] private TMP_Text lifeText;
                [SerializeField] private TMP_Text waveText;
                [SerializeField] private TMP_Text stealText;
                [SerializeField] public GameObject gameOverScreen;
                [SerializeField] private GameObject pauseUI;
                [SerializeField] private ParticleSystem firework;
                [SerializeField] private Image[] AlliesImage = new Image[4];
                public GameObject chest;
                
              
        private List<EnemyData> Allies = new List<EnemyData>();     
        private int stealPrize;
        public int _currentWave = 0;
        private int _waveScore = 5;
        private bool _currentlyInWave = false;
        public bool waveIsSpawning = false;



        
        
        private void Update()
        {
                if (Keyboard.current[Key.A].wasPressedThisFrame)
                {
                        baseTakeDamage(1);
                }
                
                
                if (EnemyManager.instance.allEnemyList.Count == 0 && _currentlyInWave && !waveIsSpawning)
                {
                        _currentlyInWave = false;
                }
                if(!_currentlyInWave)
                {
                        waveIsSpawning = true;
                        StartCoroutine(StartWave());
                }
                
                
                moneyText.text = money + "€";
                lifeText.text = life.ToString();
                waveText.text = "wave " + _currentWave;

                if (EnemyManager.instance.allEnemyList.Count != 0)
                {
                      stealPrize = EnemyManager.instance.allEnemyList[0].data.stealPrice;
                      stealText.text = "steal a Brainrot " + stealPrize + "€";  
                }
                else
                {
                        stealText.text = "no Brainrot to steal";  
                }


                if (Keyboard.current[Key.Escape].wasPressedThisFrame)
                {
                        Pause();
                }
        }

        private IEnumerator StartWave()
        {
                if (_currentWave != 0)
                {
                        firework.Play();          
                }

               _currentlyInWave = true;
               AlliesIncome();
               yield return new WaitForSeconds(_timeBetweenWaves);
               EnemyManager.instance.SpawnWave(_waveScore);
               _currentWave++;
               _waveScore = 5 + 5 * _currentWave;
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
                        image.gameObject.SetActive(false);
                }
                
                for (int i = 0; i < Allies.Count; i++)
                {
                        AlliesImage[i].sprite = Allies[i].sprite; 
                        AlliesImage[i].gameObject.SetActive(true);
                }
        }

        private void AlliesIncome()
        {
                List<EnemyData> escapes = new List<EnemyData>();
                foreach (var ally in Allies)
                {
                        money += ally.income;
                        SoundManager.instance.RequestPlaySound(SoundManager.instance.moneySound[Random.Range(0,SoundManager.instance.moneySound.Length)]);
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

        public void baseTakeDamage(int damage)
        {
                life -= damage;
                if (life <= 0)
                {
                        gameOverScreen.SetActive(true);
                        SoundManager.instance.EndMusic();
                        waveIsSpawning = true;
                }

                Camera.main.gameObject.transform.DOShakePosition(0.3f, new Vector3(1, 1, 0), 100, 10)
                        .OnComplete((() =>
                        {
                                Camera.main.gameObject.transform.position = new Vector3(0, -0.3f, -10);
                        }));
                SoundManager.instance.RequestPlaySound(SoundManager.instance.baseDamageSound[Random.Range(0,SoundManager.instance.baseDamageSound.Length)]);
        }

        public void Pause()
        {
                if (Time.timeScale == 0)
                {
                        Time.timeScale = 1;
                        pauseUI.SetActive(false);
                }
                else
                {
                        Time.timeScale = 0;
                        pauseUI.SetActive(true);
                }
        }

        public void MainMenu()
        {
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
        }



}
