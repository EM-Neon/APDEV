using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlers : MonoBehaviour, ISwipped, IDragged, ISpread, IRotated
{

    public float speed = 10;
    public float resizeSpeed = 5;
    public float rotateSpeed = 1;
    private Vector3 TargetPos = Vector3.zero;

    private void OnEnable()
    {
        TargetPos = transform.position;
    }

    private void Update()
    {
        
    }

    public void OnSwipe(SwipeEventArgs args)
    {
        Vector3 dir = Vector3.zero;
        switch (args.SwipeDirection)
        {
            case SwipeDirections.UP: Debug.Log("Swiped Up"); break;
            case SwipeDirections.DOWN: Debug.Log("Swiped Down"); break;
            case SwipeDirections.LEFT: Debug.Log("Swiped Left"); break;
            case SwipeDirections.RIGHT: Debug.Log("Swiped Right"); break;
        }
    }

    public void OnDrag(DragEventArgs args)
    {
        if(args.HitObject == gameObject)
        {
            Ray r = Camera.main.ScreenPointToRay(args.TargetFinger.position);
            Vector3 worldPoint = r.GetPoint(10);

            TargetPos = worldPoint;
            transform.position = worldPoint;
        }   
    }

    public void OnSpread(SpreadEventArgs args)
    {
        float scale = (args.DistanceDelta / Screen.dpi) * resizeSpeed;
        Vector3 scaleVector = new Vector3(scale, scale, scale);
        transform.localScale += scaleVector;
    }

    public void OnRotate(RotateEventArgs args)
    {
        float angle = args.Angle * rotateSpeed;

        if(args.RotationDirection == RotationDirections.CW)
        {
            angle *= -1;
        }
        transform.Rotate(0, 0, angle);
    }
}
