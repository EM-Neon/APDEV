using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TouchInput : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            Debug.Log(touch.phase);
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Began");
                /*Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray))
                {
                    Instantiate(particle, transform.position, transform.rotation);
                }*/
            }
        }

        /*int touch = Input.touchCount;
        if (touch > 0)
        {
            Debug.Log("Testinh");
            Touch t = Input.GetTouch(0);
            Debug.Log($"Touch: {t.phase}");
        }*/

    }
}
