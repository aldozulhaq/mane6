using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable
{
    string GetDesc();
    string GetName();
    Sprite GetSprite();
    void Use();
}