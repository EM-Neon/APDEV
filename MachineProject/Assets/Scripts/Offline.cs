using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offline : MonoBehaviour
{
    public bool hasInternet = true;
    
    // Update is called once per frame
    void Update()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            hasInternet = false;
        }
        else
        {
            hasInternet = true;
        }
    }
}
