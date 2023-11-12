using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModifierManager : MonoBehaviour
{
    public static ItemModifierManager instance;

    public List<Modifier> modifiers;

    [Header("Events")]
    [SerializeField] ModifierTypeEventChannel modifierTypeEventChannel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void AddModifier(Modifier modifier)
    {
        PlayerStats.ApplyModifiers(modifier.datas);
        NotifyModifierChangeOnType(modifier);
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

    void NotifyModifierChangeOnType(Modifier modifier)
    {
        foreach(var data in modifier.datas)
        {
            modifierTypeEventChannel.Invoke(data.modifierType);
        }
    }
}
