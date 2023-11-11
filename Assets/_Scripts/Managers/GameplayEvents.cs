using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEvents
{
    public static event Action OnWaveStartE;
    public static event Action OnWaveEndE;
    public static event Action OnEnemyDiedE;

    public delegate void OnBulletHitDelegate(GameObject gameObject, float damage);
    public static event OnBulletHitDelegate OnBulletHitE;

    public static void OnWaveStart()
    {
        OnWaveStartE?.Invoke();
    }

    public static void OnWaveEnd()
    {
        OnWaveEndE?.Invoke();
    }

    public static void OnBulletHit(GameObject gameObject, float damage)
    {
        OnBulletHitE?.Invoke(gameObject, damage);
    }

    public static void OnEnemyDied()
    {
        OnEnemyDiedE?.Invoke();
    }
}
