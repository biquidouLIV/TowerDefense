using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    public EnemyData data;
    
    private int _pv;
    private float _speed;
    private int _damage;
    
    
    private Vector3 _destination;
    private int _destinationIndex = 0;
    private Vector3 _direction;
    
    
    [SerializeField] private Image _lifeBar;
    
    
    void Start()
    {
        data = EnemyManager.instance.enemyDataList[(int)data.type];
        _pv = data.pv;
        _speed = data.speed;
        _damage = data.damage;
        
        if (data.sprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = data.sprite;
        }
        
        
        
        _destination = EnemyManager.instance.path[0].transform.position;
        _direction = (_destination - transform.position).normalized;
        

        EnemyManager.instance.allEnemyList.Add(this);
    }
    
    
    void FixedUpdate()
    {
        
        transform.Translate(_direction * _speed / 50);
        
        if (Vector3.Distance(gameObject.transform.position, _destination) < 0.1f)
        {
            if (_destinationIndex >= EnemyManager.instance.path.Length-1)
            {
                _direction = Vector3.zero;
                GameManager.instance.baseTakeDamage(_damage);
                Die(false);
                return;
            }
            _direction = (EnemyManager.instance.path[_destinationIndex + 1].position - EnemyManager.instance.path[_destinationIndex].position).normalized;
            _destination = EnemyManager.instance.path[_destinationIndex + 1].position;
            _destinationIndex++;

        }
    }

    
    
    
    public void TakeDamage(int damage)
    {
        _pv -= damage;
        if (_pv <= 0)
        {
            Die(true);
        }

        _lifeBar.fillAmount = (float)_pv / data.pv;
    }

    public void Die(bool giveMoney)
    {
        if (giveMoney)
        {
            GameManager.instance.money += data.price;
        }
        EnemyManager.instance.allEnemyList.Remove(this);
        Destroy(gameObject);
    }
}
