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
        modifier.Use();
        modifiers.Add(modifier);
    }

    public void ClearModifier()
    {
        modifiers.Clear();
    }
}
