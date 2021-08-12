using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    [SerializeField] Camera target;

    private void Start()
    {
        target = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(transform.position + target.transform.rotation * Vector3.forward);
    }
}
