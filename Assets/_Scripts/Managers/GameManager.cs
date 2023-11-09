using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        
    }

    [ContextMenu("Start Wave")]
    private void StartWave()
    {
        GameplayEvents.OnWaveStart();
    }
}
