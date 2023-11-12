using UnityEngine;

public enum WeaponType
{
    Orbit,
    Follow
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject, IUsable
{
    public GameObject prefab;
    public WeaponType weaponType;
    public Sprite sprite;
    public string desc;
    public int maxLevel;
    public int currentLevel = 1;

    public string GetName()
    {
        return name;
    }

    public void Use()
    {
        WeaponManager.instance.AddWeapon(this);
    }

    public string GetDesc()
    {
        return desc;
    }
    public Sprite GetSprite()
    {
        return sprite;
    }
}
