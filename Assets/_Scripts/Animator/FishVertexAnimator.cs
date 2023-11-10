using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishVertexAnimator : MonoBehaviour
{
    Renderer renderer => GetComponent<Renderer>();

    private void Awake()
    {
        renderer.material.SetFloat("_RandomSeed", UnityEngine.Random.Range(0, 1000));
    }

    public void animateTail(float playerspeed)
    {
        float playerSpeedRemap = playerspeed.Remap(0, 15, 0, 1);

        float tailRot = Mathf.SmoothStep(0.3f,1,playerSpeedRemap);
        renderer.material.SetFloat("_TailRotation", tailRot);

        float wobbleIntensity = Mathf.SmoothStep(1, 3.5f, playerSpeedRemap);
        renderer.material.SetFloat("_WobbleIntensity", wobbleIntensity);
    }
}
