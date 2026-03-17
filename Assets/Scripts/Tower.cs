using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tower : MonoBehaviour
{

    
    
    [SerializeField] private TowerData[] towerDataList;
    [SerializeField] private TowerType _type;
    private TowerData _data;
    private Enemy _target;
    private float lastShotTime;

    [SerializeField] private GameObject PlaceTowerUI;
    [SerializeField] private GameObject UpgradeTowerUI;
    
    
    //retirer serialize
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private float _fireDelay;
    [SerializeField] private int _value;
    [SerializeField] private int _amoCost;
    
    private int _damageUpgradePrice;
    private int _rangeUpgradePrice;
    private int _fireDelayUpgradePrice;

    
    
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

    public void UpgradeRange(int _rangeToAdd)
    {
        if (GameManager.instance.money < _rangeUpgradePrice)
        {
            return;
        }

        _value += _rangeUpgradePrice;
        GameManager.instance.money -= _rangeUpgradePrice;
        
        _range += _rangeToAdd;
    }
    public void UpgradeDamage(int _damageToAdd)
    {
        if (GameManager.instance.money < _damageUpgradePrice)
        {
            return;
        }

        
        _value += _damageUpgradePrice;
        GameManager.instance.money -= _damageUpgradePrice;
        _damage += _damageToAdd;
    }
    public void UpgradeFireDelay(float _fireDelayReduction)
    {
        if (GameManager.instance.money < _fireDelayUpgradePrice)
        {
            return;
        }

        _value += _fireDelayUpgradePrice;
        GameManager.instance.money -= _fireDelayUpgradePrice;
        _fireDelay *= _fireDelayReduction;
    }

    public void SellTower()
    {
        GameManager.instance.money += _value;
        PlaceTower(0);
        UpgradeTowerUI.SetActive(false);
    }
    
    private void Update()
    {
        if (Time.time - _fireDelay > lastShotTime)
        {
            FindTarget();
            Shoot();
            lastShotTime = Time.time;
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

        GameManager.instance.money -= _amoCost;
        _target.TakeDamage(_damage);
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
    longRange
}