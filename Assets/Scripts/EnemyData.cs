using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("data")]
        public EnemyType type;
        public int pv;
        public float speed;
        public int damage;
        public int price;
        public int difficulty;

        [Header("steal data")] 
        public int stealPrice;
        public int income;
        public float escape;
    [Header("refs")]
        public Sprite sprite;
        public AudioClip spawnSound;


}
