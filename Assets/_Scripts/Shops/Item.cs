using UnityEngine;

[SerializeField]
public enum ItemType
{
    Weapon,
    Modifier
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;

}
