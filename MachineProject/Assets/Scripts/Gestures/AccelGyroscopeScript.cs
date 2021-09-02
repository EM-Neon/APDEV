using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelGyroscopeScript : MonoBehaviour
{
    
    private void FixedUpdate()
    {
        float xValue = Input.acceleration.magnitude;
        if (xValue >= 2.0f)
        {
           
        }
    }
}
