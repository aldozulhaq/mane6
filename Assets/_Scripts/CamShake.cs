using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public static CamShake instance;

    private CinemachineVirtualCamera virtualCamera;
    private float shakeTimer = 0.2f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        DOVirtual.Float(0, 5, shakeTimer, (float x) =>
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = x;
        }).OnComplete(() =>
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        });
    }
}
