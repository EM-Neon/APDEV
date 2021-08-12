using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public class GestureManager : MonoBehaviour
{
    public EventHandler<TapEventArgs> OnTap;
    public static GestureManager Instance;
    public TapProperty _tapProperty;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Touch trackedFinger;
    private Vector2 startPoint = Vector2.zero;
    private Vector2 endPoint = Vector2.zero;
    private float gestureTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Update()
    {
        if(Input.touchCount > 0)
        {
            trackedFinger = Input.GetTouch(0);
            if(trackedFinger.phase == TouchPhase.Began)
            {
                gestureTime = 0f;
                startPoint = trackedFinger.position;
            }
            else if(trackedFinger.phase == TouchPhase.Ended)
            {
                endPoint = trackedFinger.position;
                if(gestureTime <= _tapProperty.tapTime && Vector2.Distance(startPoint, endPoint) < (Screen.dpi * _tapProperty.tapDistance))
                {
                    FireTapEvent(startPoint);
                }
            }
            else
            {
                gestureTime += Time.deltaTime;
            }
        }
    }

    private void FireTapEvent(Vector2 pos)
    {
        if(OnTap != null)
        {
            TapEventArgs args = new TapEventArgs(pos);
            OnTap(this, args);
        }
    }
}
