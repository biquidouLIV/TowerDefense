using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Scriptable Objects/TowerData")]
public class TowerData : ScriptableObject
{
    
    [Header("data")]
        public TowerType type;
        public int price;
        public int damage;
        public int maxDamage;
        public float range;
        public float maxRange;
        public float fireDelay;
        public float minFireDelay;
        public int amoCost;

    
    [Header("refs")]
        public Sprite sprite;
        public GameObject bullet;
    
    [Header("upgrade price")]
        public int damageUpgradePrice;
        public int rangeUpgradePrice;
        public int fireDelayUpgradePrice;
    
    [Header("upgrade price augmentation")]
        public int damageUpgradeAugmentation;
        public int rangeUpgradeAugmentation;
        public int fireDelayUpgradeAugmentation;

    [Header("upgrade value")]
        public int damageUpgrade;
        public float rangeUpgrade;
        public float fireDelayUpgrade;


}