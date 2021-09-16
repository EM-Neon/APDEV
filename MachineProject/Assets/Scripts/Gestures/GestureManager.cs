using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public class GestureManager : MonoBehaviour
{   
    public static GestureManager Instance;
    public EventHandler<SwipeEventArgs> OnSwipe;
    public EventHandler<DragEventArgs> OnDrag;
    public EventHandler<TwoFingerPanEventArgs> OnTwoFingerPan;
    public EventHandler<SpreadEventArgs> OnSpread;
    public EventHandler<RotateEventArgs> OnRotate;

    public SwipeProperty _swipeProperty;
    public DragProperty _dragProperty;
    public TwoFingerPanProperty _twoFingerPanProperty;
    public SpreadProperty _spreadProperty;
    public RotateProperty _rotateProperty;
    
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
    private Touch trackedFinger2;
    private Vector2 startPoint = Vector2.zero;
    private Vector2 endPoint = Vector2.zero;
    private float gestureTime = 0f;
    private bool gestureRegistered = false;
    public bool isDrag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            if (gestureRegistered)
                return;
            if(Input.touchCount == 1)
            {
                CheckSingleFingerGesture();
            }
            else if(Input.touchCount > 1)
            {
                trackedFinger = Input.GetTouch(0);
                trackedFinger2 = Input.GetTouch(1);

                if ((trackedFinger.phase == TouchPhase.Moved && trackedFinger2.phase == TouchPhase.Moved))
                {
                    Vector2 prevPoint1 = GetPreviousPoint(trackedFinger);
                    Vector2 prevPoint2 = GetPreviousPoint(trackedFinger2);

                    Vector2 diffVector = trackedFinger.position - trackedFinger2.position;
                    Vector2 prevDiffVector = prevPoint1 - prevPoint2;

                    float angle = Vector2.Angle(prevDiffVector, diffVector);

                    float currDistance = Vector2.Distance(trackedFinger.position, trackedFinger2.position);
                    float prevDistance = Vector2.Distance(prevPoint1, prevPoint2);

                    /*if (Mathf.Abs(currDistance - prevDistance) >= (_spreadProperty.MinDistanceChange * Screen.dpi))
                    {
                        FireSpreadEvent(currDistance - prevDistance);
                    }
                    else if(angle >= _rotateProperty.minChange)
                    { 
                        FireRotateEvent(diffVector, prevDiffVector, angle);
                    }*/
                }
            }
        }
        else
        {
            gestureRegistered = false;
            isDrag = false;
        }
    }

    private Vector2 GetPreviousPoint(Touch finger)
    {
        return finger.position - finger.deltaPosition;
    }

    private Vector2 Midpoint(Vector2 p1, Vector2 p2)
    {
        return (p1 + p2) / 2;
    }

    private void CheckSingleFingerGesture()
    {
        trackedFinger = Input.GetTouch(0);
        if (trackedFinger.phase == TouchPhase.Began)
        {
            gestureTime = 0f;
            startPoint = trackedFinger.position;
        }
        else if (trackedFinger.phase == TouchPhase.Ended)
        {
            endPoint = trackedFinger.position;

            if (gestureTime <= _swipeProperty.swipeTime && Vector2.Distance(startPoint, endPoint) >= (_swipeProperty.minSwipeDistance * Screen.dpi))
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

    private void FireSwipeEvent()
    {
        Vector2 diff = startPoint - endPoint;

        Ray r = Camera.main.ScreenPointToRay(startPoint);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if(Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }

        SwipeDirections swipeDir;

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
        gestureRegistered = true;

        SwipeEventArgs args = new SwipeEventArgs(startPoint, swipeDir, diff, hitObj);

        if(OnSwipe != null){
            OnSwipe(this, args);
        }

        if(hitObj != null)
        {
            ISwipped swipe = hitObj.GetComponent<ISwipped>();
            if (swipe != null)
            {
                swipe.OnSwipe(this, args);
            }
        }
    }

    private void FireDragEvent()
    {
        Ray r = Camera.main.ScreenPointToRay(trackedFinger.position);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if(Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }

        isDrag = true;

        DragEventArgs args = new DragEventArgs(trackedFinger, hitObj);

        if(OnDrag != null)
        {
            OnDrag(this, args);
        }

        if(hitObj != null)
        {
            IDragged drag = hitObj.GetComponent<IDragged>();
            if(drag != null)
            {
                drag.OnDrag(this, args);
            }
        }
    }

    private void FireSpreadEvent(float dist)
    {
        if (!gestureRegistered)
        {
            if(dist > 0)
            {
                
                gestureRegistered = true;
            }
            else
            {
                
                gestureRegistered = true;
            }
        }
    }

    private void FireRotateEvent(Vector2 diffVector, Vector2 prevDiffVector, float angle)
    {
        Vector3 cross = Vector3.Cross(prevDiffVector, diffVector);
        if (!gestureRegistered)
        {
            if (cross.z > 0)
            {
                
                gestureRegistered = true;
            }
            else
            {
                
                gestureRegistered = true;
            }
        }        
    }

/*    private void FireTwoFingerPanEvent()
    {
        Debug.Log("Two Finger Pan!!!");

        TwoFingerPanEventArgs args = new TwoFingerPanEventArgs(trackedFinger, trackedFinger2);
        if(OnTwoFingerPan != null)
        {
            OnTwoFingerPan(this, args);
        }
    }*/
}
