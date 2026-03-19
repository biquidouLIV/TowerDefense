using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Scriptable Objects/TowerData")]
public class TowerData : ScriptableObject
{
    public TowerType type;
    public int price;
    public int damage;
    public float range;
    public float fireDelay;
    public int amoCost;
    public Sprite sprite;
    
    
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