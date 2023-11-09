using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEvents
{
    public static event Action OnWaveStartE;
    public static event Action OnWaveEndE;

    public static void OnWaveStart()
    {
        OnWaveStartE?.Invoke();
    }

    public static void OnWaveEnd()
    {
        OnWaveEndE?.Invoke();
    }
}
