using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tower : MonoBehaviour
{
    private enum TowerType
    {
        normal,
        longRange
    }
    
    [SerializeField] private TowerData[] towerDataList;
    [SerializeField] private TowerType _type;
    private TowerData _data;
    private Enemy _target;
    private float lastShotTime;
    
    private void Start()
    {
        _data = towerDataList[(int)_type];
    }

    private void Update()
    {
        if (Time.time - _data.fireDelay > lastShotTime)
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
            if (Vector3.Distance(enemy.transform.position, transform.position) < _data.range)
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
        Debug.Log("piou !!!");
        _target.GetComponent<SpriteRenderer>().color = Color.red;
        _target.TakeDamage(_data.damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,_data.range);
    }
}
