using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public class DragEventArgs : EventArgs
{
    private Touch targetFinger;
    private GameObject hitObject;

    public DragEventArgs(Touch _targetFinger, GameObject _hitObject)
    {
        targetFinger = _targetFinger;
        hitObject = _hitObject;
    }

    public Touch TargetFinger
    {
        get { return targetFinger; }
    }

    public GameObject HitObject
    {
        get { return hitObject; }
    }
}
