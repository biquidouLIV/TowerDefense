using System;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Tower : MonoBehaviour
{
   
    [Header("Refs")]
        [SerializeField] private TowerData[] towerDataList;
        [SerializeField] private GameObject placeTowerUI;
        [SerializeField] private GameObject upgradeTowerUI;
        [SerializeField] private TMP_Text damageUpgradeText;
        [SerializeField] private TMP_Text rangeUpgradeText;
        [SerializeField] private TMP_Text fireDelayUpgradeText;
        [SerializeField] private TMP_Text sellValue;
        [SerializeField] private SpriteRenderer skin;
        
        
    private TowerType _type;
    private TowerData _data;
    private Enemy _target;
    private float _lastShotTime;
    private GameObject _bulletPrefab;
    
    private float _range; 
    private int _damage;
    private float _fireDelay;
    private int _value; 
    private int _amoCost;
    
    private int _damageUpgradePrice;
    private int _rangeUpgradePrice;
    private int _fireDelayUpgradePrice;
    
    private int _damageUpgrade;
    private float _rangeUpgrade;
    private float _fireDelayUpgrade;
    
    private int _damageUpgradeAugmentation;
    private int _rangeUpgradeAugmentation;
    private int _fireDelayUpgradeAugmentation;



    
    
    private void Start()
    {
        PlaceTower(0);
    }

    public void PlaceTower(int towerType)
    {
        _data = towerDataList[towerType];
        if (GameManager.instance.money < _data.price)
        {
            _data = towerDataList[0];
            return;
        }

        GameManager.instance.money -= _data.price;
        _type = _data.type;
        _range = _data.range;
        _damage = _data.damage;
        _fireDelay = _data.fireDelay;
        placeTowerUI.SetActive(false); 
        _damageUpgradePrice = _data.damageUpgradePrice;
        _rangeUpgradePrice = _data.rangeUpgradePrice;
        _fireDelayUpgradePrice = _data.fireDelayUpgradePrice;
        _value = _data.price;
        _amoCost = _data.amoCost;
        skin.sprite = _data.sprite;

        _damageUpgrade = _data.damageUpgrade;
        _rangeUpgrade = _data.rangeUpgrade;
        _fireDelayUpgrade = _data.fireDelayUpgrade;

        _damageUpgradeAugmentation = _data.damageUpgradeAugmentation;
        _rangeUpgradeAugmentation = _data.rangeUpgradeAugmentation;
        _fireDelayUpgradeAugmentation = _data.rangeUpgradeAugmentation;

        damageUpgradeText.text = "damage "+_damageUpgradePrice +"€";
        rangeUpgradeText.text = "range "+_rangeUpgradePrice + "€";
        fireDelayUpgradeText.text = "fireDelay "+_fireDelayUpgradePrice + "€";
        sellValue.text = "sell " +_value/2 + "€";

        _bulletPrefab = _data.bullet;

        if (_data.type != TowerType.empty)
        {
            SoundManager.instance.RequestPlaySound(SoundManager.instance.moneySound[Random.Range(0,SoundManager.instance.moneySound.Length)], 0.3f);
        }
        
    }

    public void ActiveUI()
    {
        if (_type == TowerType.empty)
        {
            if (placeTowerUI.activeSelf)
            {
               placeTowerUI.SetActive(false); 
            }
            else
            {
                placeTowerUI.SetActive(true);
            }
        }
        else
        {
            if (upgradeTowerUI.activeSelf)
            {
                upgradeTowerUI.SetActive(false); 
            }
            else
            {
                upgradeTowerUI.SetActive(true);
            }
        }
    }

    public void UpgradeRange()
    {
        bool isMaxed = false;
        if (GameManager.instance.money < _rangeUpgradePrice)
        {
            return;
        }

        if (isMaxed)
        {
            return;
        }
        
        _value += _rangeUpgradePrice;
        GameManager.instance.money -= _rangeUpgradePrice;
        
        _range += _rangeUpgrade;
        SoundManager.instance.RequestPlaySound(SoundManager.instance.moneySound[Random.Range(0,SoundManager.instance.moneySound.Length)], 0.3f);

        _rangeUpgradePrice += _rangeUpgradeAugmentation;
        rangeUpgradeText.text = "range "+_rangeUpgradePrice + "€";
        sellValue.text = "sell " +_value/2 + "€";

        if (_range >= _data.maxRange)
        {
            isMaxed = true;
            rangeUpgradeText.text = "Max.";
        }
    }
    public void UpgradeDamage()
    {
        bool isMaxed = false;
        
        if (GameManager.instance.money < _damageUpgradePrice)
        {
            return;
        }

        if (isMaxed)
        {
            return;
        }
        
        _value += _damageUpgradePrice;
        GameManager.instance.money -= _damageUpgradePrice;
        _damage += _damageUpgrade;
        SoundManager.instance.RequestPlaySound(SoundManager.instance.moneySound[Random.Range(0,SoundManager.instance.moneySound.Length)], 0.3f);

        _damageUpgradePrice += _damageUpgradeAugmentation;
        damageUpgradeText.text = "damage "+_damageUpgradePrice + "€";
        sellValue.text = "sell " +_value/2 +"€";
        
        if (_damage >= _data.maxDamage)
        {
            isMaxed = true;
            damageUpgradeText.text = "Max.";
        }
        
        

    }
    public void UpgradeFireDelay()
    {
        bool isMaxed = false;
        
        if (GameManager.instance.money < _fireDelayUpgradePrice)
        {
            return;
        }

        if (isMaxed)
        {
            return;
        }
        
        _value += _fireDelayUpgradePrice;
        GameManager.instance.money -= _fireDelayUpgradePrice;
        _fireDelay -= _fireDelayUpgrade;
        SoundManager.instance.RequestPlaySound(SoundManager.instance.moneySound[Random.Range(0,SoundManager.instance.moneySound.Length)], 0.3f);

        _fireDelayUpgradePrice += _fireDelayUpgradeAugmentation;
        fireDelayUpgradeText.text = "fireDelay "+_fireDelayUpgradePrice+"€";
        sellValue.text = "sell " +_value/2 +"€";
        
        if (_fireDelay <= _data.minFireDelay)
        {
            isMaxed = true;
            fireDelayUpgradeText.text = "Max.";
        }
        
    }

    public void SellTower()
    {
        GameManager.instance.money += _value/2;
        PlaceTower(0);
        upgradeTowerUI.SetActive(false);
        SoundManager.instance.RequestPlaySound(SoundManager.instance.moneySound[Random.Range(0,SoundManager.instance.moneySound.Length)], 0.3f);
    }
    
    private void Update()
    {
        if (Time.time - _fireDelay > _lastShotTime)
        {
            FindTarget();
            Shoot();
            _lastShotTime = Time.time;
        }

        
        
        if (_target != null)
        {
            Vector3 direction = _target.transform.position - transform.position;
            
            
            float angle = Mathf.Acos(Vector3.Dot(Vector3.up, direction)/direction.magnitude*Vector3.up.magnitude);
            angle = (angle * 180 / Mathf.PI + 270) + 90;
            
            skin.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
        }
        
        
        
    }

    private void FindTarget()
    {
        _target = null;
        foreach (var enemy in EnemyManager.instance.allEnemyList)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) < _range)
            {
                _target = enemy;
                break;
            }
        }
    }

    private void Shoot()
    {
        if (_target == null)
        {
            return;
        }

        if (GameManager.instance.money <= 0)
        {
            return;
        }

        GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.target = _target;
        bulletScript.damage = _damage;


        SoundManager.instance.RequestPlaySound(SoundManager.instance.shootSound,0.1f);
        GameManager.instance.money -= _amoCost;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,_range);
    }
}
public enum TowerType
{
    empty,
    normal,
    sniper,
    rapide,
    
}