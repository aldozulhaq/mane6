using UnityEngine;

public struct DamageData
{
    public GameObject target;
    public float damage;
}

[CreateAssetMenu(menuName = "Event Channel/DamageEventChannel")]
public class DamageChannel : EventChannel<DamageData> { }