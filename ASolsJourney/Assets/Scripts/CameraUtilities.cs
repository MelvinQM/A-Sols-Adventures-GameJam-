using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraUtilities : Singleton<CameraUtilities>
{
    private CinemachineVirtualCamera cinemachineCamera;
    private float timer;
    private CinemachineBasicMultiChannelPerlin _cbmcp;

    internal override void Awake()
    {
        base.Awake();
        
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        _cbmcp = cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float shakeIntensity = 1f, float shakeTime = 0.2f)
    {
        _cbmcp.m_AmplitudeGain = shakeIntensity;
        _cbmcp.m_FrequencyGain = shakeIntensity;
        timer = shakeTime;
    }

    public void StopShake()
    {
        _cbmcp.m_AmplitudeGain = 0f;
        _cbmcp.m_FrequencyGain = 0f;
        timer = 0f;
    }

    void Update()
    {
        // if(Input.GetKey(KeyCode.Q))
        // {
        //     Debug.Log("SHAKE");
        //     ShakeCamera();
        // }

        if(timer > 0)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
                StopShake();
        }
    }
}
