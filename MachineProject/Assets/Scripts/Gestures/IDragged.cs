using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragged
{
    void OnDrag(object sender, DragEventArgs args);
}
