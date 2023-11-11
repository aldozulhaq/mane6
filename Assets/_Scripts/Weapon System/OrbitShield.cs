using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitShield : MonoBehaviour, IWeapon
{
    public int level = 1;
    public int maxLevel = 4;

    [SerializeField] GameObject orbitShieldFishPrefab;
    Color originalColor;
    
    void Update()
    {
        transform.RotateAround(transform.parent.position, Vector3.up, 100 * Time.deltaTime);
    }

    public void LevelUp()
    {
        if(level >= 4) return;
        level++;

        var orbitShieldFish = Instantiate(orbitShieldFishPrefab, transform.position, Quaternion.identity, transform);

        switch (level)
        {
            case 2:
                orbitShieldFish.transform.localPosition = Vector3.zero.WithZ(-9);
                orbitShieldFish.transform.localRotation = Quaternion.Euler(0, 180, 0);
                break;
            case 3:
                orbitShieldFish.transform.localPosition = Vector3.zero.WithX(9);
                orbitShieldFish.transform.localRotation = Quaternion.Euler(0, 90, 0);
                break;
            case 4:
                orbitShieldFish.transform.localPosition = Vector3.zero.WithX(-9);
                orbitShieldFish.transform.localRotation = Quaternion.Euler(0, -90, 0);
                break;
        }
    }
}
