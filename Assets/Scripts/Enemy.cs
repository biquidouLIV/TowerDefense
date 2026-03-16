using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public EnemyData data;
    
    private int _pv;
    private float _speed;
    
    
    private Vector3 _destination;
    private int _destinationIndex = 0;
    private Vector3 _direction;
    
    
    [SerializeField] private TMP_Text text;
    
    
    void Start()
    {
        data = EnemyManager.instance.enemyDataList[(int)data.type];
        _pv = data.pv;
        _speed = data.speed;
        
        if (data.sprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = data.sprite;
        }
        
        
        
        _destination = EnemyManager.instance.path[0].transform.position;
        _direction = (_destination - transform.position).normalized;
        

        EnemyManager.instance.allEnemyList.Add(this);
        
        Debug.Log("spawn ennemi");
    }
    
    
    void FixedUpdate()
    {
        text.text = _pv.ToString();
        
        
        transform.Translate(_direction * _speed / 50);
        
        if (Vector3.Distance(gameObject.transform.position, _destination) < 0.1f)
        {
            if (_destinationIndex >= EnemyManager.instance.path.Length-1)
            {
                _direction = Vector3.zero;
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
        Debug.Log("aaaaaaaaa");
        if (_pv <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        EnemyManager.instance.allEnemyList.Remove(this);
        Destroy(gameObject);
    }
}
