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

    
    
    [SerializeField] private TowerData[] towerDataList;
    [SerializeField] private TowerType _type;
    private TowerData _data;
    private Enemy _target;
    private float lastShotTime;

    [SerializeField] private GameObject PlaceTowerUI;
    [SerializeField] private GameObject UpgradeTowerUI;

    [SerializeField] private TMP_Text damageUpgradeText;
    [SerializeField] private TMP_Text rangeUpgradeText;
    [SerializeField] private TMP_Text fireDelayUpgradeText;
    [SerializeField] private TMP_Text sellValue;
    
    [SerializeField] private SpriteRenderer _skin;
    [SerializeField] private GameObject bulletPrefab;
    
    
    
    //retirer serialize
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private float _fireDelay;
    [SerializeField] private int _value;
    [SerializeField] private int _amoCost;
    
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
        PlaceTowerUI.SetActive(false); 
        _damageUpgradePrice = _data.damageUpgradePrice;
        _rangeUpgradePrice = _data.rangeUpgradePrice;
        _fireDelayUpgradePrice = _data.fireDelayUpgradePrice;
        _value = _data.price;
        _amoCost = _data.amoCost;
        _skin.sprite = _data.sprite;

        _damageUpgrade = _data.damageUpgrade;
        _rangeUpgrade = _data.rangeUpgrade;
        _fireDelayUpgrade = _data.fireDelayUpgrade;

        _damageUpgradeAugmentation = _data.damageUpgradeAugmentation;
        _rangeUpgradeAugmentation = _data.rangeUpgradeAugmentation;
        _fireDelayUpgradeAugmentation = _data.rangeUpgradeAugmentation;

        damageUpgradeText.text = "damage "+_damageUpgradePrice +"€";
        rangeUpgradeText.text = "range "+_rangeUpgradePrice + "€";
        fireDelayUpgradeText.text = "fireDelay "+_fireDelayUpgradePrice + "€";
        sellValue.text = "sell " +_value + "€";
        
        
        SoundManager.instance.RequestPlaySound(SoundManager.instance.moneySound[Random.Range(0,SoundManager.instance.moneySound.Length)]);
    }

    public void ActiveUI()
    {
        if (_type == TowerType.empty)
        {
            if (PlaceTowerUI.activeSelf)
            {
               PlaceTowerUI.SetActive(false); 
            }
            else
            {
                PlaceTowerUI.SetActive(true);
            }
        }
        else
        {
            if (UpgradeTowerUI.activeSelf)
            {
                UpgradeTowerUI.SetActive(false); 
            }
            else
            {
                UpgradeTowerUI.SetActive(true);
            }
        }
    }

    public void UpgradeRange()
    {
        if (GameManager.instance.money < _rangeUpgradePrice)
        {
            return;
        }

        _value += _rangeUpgradePrice;
        GameManager.instance.money -= _rangeUpgradePrice;
        
        _range += _rangeUpgrade;
        SoundManager.instance.RequestPlaySound(SoundManager.instance.moneySound[Random.Range(0,SoundManager.instance.moneySound.Length)]);

        _rangeUpgradePrice += _rangeUpgradeAugmentation;
        rangeUpgradeText.text = "range "+_rangeUpgradePrice + "€";
        sellValue.text = "sell " +_value + "€";

    }
    public void UpgradeDamage()
    {
        if (GameManager.instance.money < _damageUpgradePrice)
        {
            return;
        }

        
        _value += _damageUpgradePrice;
        GameManager.instance.money -= _damageUpgradePrice;
        _damage += _damageUpgrade;
        SoundManager.instance.RequestPlaySound(SoundManager.instance.moneySound[Random.Range(0,SoundManager.instance.moneySound.Length)]);

        _damageUpgradePrice += _damageUpgradeAugmentation;
        damageUpgradeText.text = "damage "+_damageUpgradePrice + "€";
        sellValue.text = "sell " +_value +"€";

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
        SoundManager.instance.RequestPlaySound(SoundManager.instance.moneySound[Random.Range(0,SoundManager.instance.moneySound.Length)]);

        _fireDelayUpgradePrice += _fireDelayUpgradeAugmentation;
        fireDelayUpgradeText.text = "fireDelay "+_fireDelayUpgradePrice+"€";
        sellValue.text = "sell " +_value +"€";
        
        if (_fireDelay <= _fireDelayUpgrade)
        {
            isMaxed = true;
            fireDelayUpgradeText.text = "Max.";
        }
        
    }

    public void SellTower()
    {
        GameManager.instance.money += _value;
        PlaceTower(0);
        UpgradeTowerUI.SetActive(false);
        SoundManager.instance.RequestPlaySound(SoundManager.instance.moneySound[Random.Range(0,SoundManager.instance.moneySound.Length)]);
    }
    
    private void Update()
    {
        if (Time.time - _fireDelay > lastShotTime)
        {
            FindTarget();
            Shoot();
            lastShotTime = Time.time;
        }

        
        
        if (_target != null)
        {
            Vector3 direction = _target.transform.position - transform.position;
            
            
            float angle = Mathf.Acos(Vector3.Dot(Vector3.up, direction)/direction.magnitude*Vector3.up.magnitude);
            angle = (angle * 180 / Mathf.PI + 270) + 90;
            
            _skin.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
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

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.target = _target;
        bulletScript.damage = _damage;
        
        
        SoundManager.instance.RequestPlaySound(SoundManager.instance.moneySound[Random.Range(0,SoundManager.instance.moneySound.Length)]);
        GameManager.instance.money -= _amoCost;
        //_target.TakeDamage(_damage);
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
    Tower1,
    Tower2,
    Tower3
    
}