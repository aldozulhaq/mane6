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
}

[CreateAssetMenu(menuName = "Event Channel/DamageEventChannel")]
public class DamageChannel : EventChannel<DamageData> { }