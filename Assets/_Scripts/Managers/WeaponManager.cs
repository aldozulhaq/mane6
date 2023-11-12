using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    public List<Weapon> weapons = new List<Weapon>();

    [SerializeField] Transform weaponOrbitParent;
    [SerializeField] Transform weaponFollowParent;

    public List<Weapon> maxLeveledWeapons = new List<Weapon>();

    [SerializeField] EventChannel onShiledLevelUp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
            Destroy(gameObject);
    }

    public void AddWeapon(Weapon weapon)
    {
        switch(weapon.weaponType)
        {
            case WeaponType.Orbit:
                var a = Instantiate(weapon.prefab, weaponOrbitParent);
                a.transform.localRotation = Quaternion.Euler(Vector3.zero);

                switch(weapon.currentLevel)
                {
                    case 1:
                        a.transform.localPosition = new Vector3(-3.32f, 0, -3.82f);
                        break;
                    case 2:
                        a.transform.localPosition = new Vector3(+3.32f, 0, +3.82f);
                        break;
                    case 3:
                        a.transform.localPosition = new Vector3(-6.64f, 0, -6.64f);
                        break;
                    case 4:
                        a.transform.localPosition = new Vector3(+6.64f, 0, +6.64f);
                        break;
                }
                break;
            case WeaponType.Follow:
                if(weapon.currentLevel == 1)
                    Instantiate(weapon.prefab, weaponFollowParent);
                onShiledLevelUp.Invoke();
                break;
        }
        weapons.Add(weapon);
        weapon.currentLevel++;
        CheckIfWeaponMaxLeveled(weapon);
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

    void CheckIfWeaponMaxLeveled(Weapon weapon)
    {
        if(GetWeaponLevel(weapon) == weapon.maxLevel)
        {
            maxLeveledWeapons.Add(weapon);
        }
    }
}
