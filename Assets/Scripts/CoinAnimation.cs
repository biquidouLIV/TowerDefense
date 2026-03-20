using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinAnimation : MonoBehaviour
{
    public Vector3 target;
    
    
    private void Start()
    {
        transform.DOMove(transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 0), Random.Range(0.5f,0.8f))
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                transform.DOMove(GameManager.instance.chest.transform.position, Random.Range(0.5f,0.8f))
                    .SetEase(Ease.InOutCubic)
                    .OnComplete((() =>
                    {
                        Destroy(this.gameObject);
                    }));
            });
    }
}
