using UnityEngine;

public struct DamageData
{
    public GameObject target;
    public float damage;

    // Constructor for player taking damage
    public DamageData(Player player, float damage) : this()
    {
        target = player.gameObject;
        this.damage = damage;
    }

    // Constructor for enemy taking damage
    public DamageData(GameObject target, float damage) : this()
    {
        this.target = target;
        this.damage = damage;
    }
}

[CreateAssetMenu(menuName = "Event Channel/DamageEventChannel")]
public class DamageChannel : EventChannel<DamageData> { }