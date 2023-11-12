using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    public List<Weapon> weapons = new List<Weapon>();

    [SerializeField] Transform weaponOrbitParent;
    [SerializeField] Transform weaponFollowParent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
            Destroy(gameObject);
    }

    public void AddWeapon(Weapon weapon)
    {
        switch(weapon.weaponType)
        {
            case WeaponType.Orbit:
                Instantiate(weapon.prefab, weaponOrbitParent);
                break;
            case WeaponType.Follow:
                Instantiate(weapon.prefab, weaponFollowParent);
                break;
        }
        weapons.Add(weapon);
    }

    public void ClearWeapon()
    {
        weapons.Clear();
    }

    public int GetWeaponLevel(Weapon weapon)
    {
        int level = 0;

        foreach (var weap in weapons)
        {
            if (weap.name == weapon.name)
                level++;
        }

        return level;
    }
}
