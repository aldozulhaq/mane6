using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishVertexAnimator : MonoBehaviour
{
    Renderer renderer => GetComponent<Renderer>();

    public void adjustShaderParam(Vector2 playerDirAndSpeed)
    {
        float playerDir = playerDirAndSpeed.x;
        float playerSpeed = Mathf.Clamp(playerDirAndSpeed.y, 5, 15);

        renderer.material.SetFloat("_TailRotation", playerDir);

    }
}
