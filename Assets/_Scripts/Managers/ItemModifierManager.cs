using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModifierManager : MonoBehaviour
{
    public static ItemModifierManager instance;

    public List<Modifier> modifiers;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void AddModifier(Modifier modifier)
    {
        PlayerStats.ApplyModifiers(modifier.datas);
        modifiers.Add(modifier);
    }

    public void ClearModifier()
    {
        modifiers.Clear();
    }

    public int GetModifierLevel(Modifier modifier)
    {
        int level = 0;

        foreach (var mod in modifiers)
        {
            if (mod.name == modifier.name)
                level++;
        }

        return level;
    }
}
