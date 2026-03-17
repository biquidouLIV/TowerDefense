using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public EnemyType type;
    public int pv;
    public float speed;
    public Sprite sprite;
    public int price;

    public int difficulty;



}
