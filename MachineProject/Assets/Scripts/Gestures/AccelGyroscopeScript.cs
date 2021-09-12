using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelGyroscopeScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    ParticleSystem.MainModule beamParticle;

    private bool hasUlt = false;
    private float timer = 0;
    /*public InputHandlers inputHandlers;*/
    private void Start()
    {
        beamParticle = particle.main;
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        float xValue = Input.acceleration.magnitude;
        if (xValue >= 2.0f && timer <= 5)
        {
            Debug.Log($"ACCELERATE: {xValue}");
            beamParticle.startSpeedMultiplier = 100;
            beamParticle.startColor = Color.white;
            Debug.Log($"Speed Multiplier: {beamParticle.startSpeedMultiplier}");
        }
    }
}
