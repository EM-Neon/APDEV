using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelGyroscopeScript : MonoBehaviour
{
    private void Start()
    {

    }

    private void FixedUpdate()
    {
        float xValue = Input.acceleration.magnitude;
        if (xValue >= 2.0f)
        {

        }
    }
}
