using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public class GestureManager : MonoBehaviour
{   
    public static GestureManager Instance;

    public EventHandler<TapEventArgs> OnTap;
    public EventHandler<SwipeEventArgs> OnSwipe;
    public EventHandler<DragArgs> OnDrag;
    public DragProperty _dragProperty;
    public TapProperty _tapProperty;
    public SwipeProperty _swipeProperty;

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

                if (gestureTime <= _tapProperty.tapTime && Vector2.Distance(startPoint, endPoint) < (Screen.dpi * _tapProperty.tapDistance))
                {
                    FireTapEvent(startPoint);
                }

                else if (gestureTime <= _swipeProperty.swipeTime && Vector2.Distance(startPoint, endPoint) >= (_swipeProperty.minSwipeDistance * Screen.dpi))
                {
                    FireSwipeEvent();
                }
            }
            else
            {
                gestureTime += Time.deltaTime;
                if(gestureTime >= _dragProperty.bufferTime)
                {
                    FireDragEvent();
                }
            }
        }
    }

    private void FireSwipeEvent()
    {
        Ray r = Camera.main.ScreenPointToRay(startPoint);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if(Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }
        Vector2 diff = startPoint - endPoint;

        SwipeDirections swipeDir = SwipeDirections.RIGHT;

        if(Mathf.Abs(diff.x) > Mathf.Abs(diff.y)){
            if(diff.x <= 0)
            {
                swipeDir = SwipeDirections.RIGHT;
            }
            else
            {
                swipeDir = SwipeDirections.LEFT;
            }
        }
        else
        {
            if (diff.y <= 0)
            {
                swipeDir = SwipeDirections.UP;
            }
            else
            {
                swipeDir = SwipeDirections.DOWN;
            }
        }
        SwipeEventArgs args = new SwipeEventArgs(startPoint, swipeDir, diff, hitObj);
        if(OnSwipe != null)
        {
            OnSwipe(this, args);
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

    private void FireDragEvent()
    {
        Debug.Log($"Drag: {trackedFinger.position}");

        Ray r = Camera.main.ScreenPointToRay(trackedFinger.position);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if(Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }

        DragArgs args = new DragArgs(trackedFinger, hitObj);
        if (OnDrag != null)
        {
            OnDrag(this, args);
        }
    }
}
