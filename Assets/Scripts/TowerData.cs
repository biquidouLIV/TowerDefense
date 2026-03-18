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
    

    public int damageUpgradePrice;
    public int rangeUpgradePrice;
    public int fireDelayUpgradePrice;

    public Sprite sprite;
}