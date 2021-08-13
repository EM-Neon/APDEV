using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragEventReceiver : MonoBehaviour
{
    public GameObject spawnItem;
    private Vector3 TargetPos = Vector3.zero;
    public float speed = 10;
    // Start is called before the first frame update
    public void Start()
    {
        GestureManager.Instance.OnDrag += onDrag;
    }

    public void onDrag(object sender, DragArgs args)
    {
        Ray r = Camera.main.ScreenPointToRay(args.TargetFinger.position);
        Vector3 worldPoint = r.GetPoint(10);

        TargetPos = worldPoint;
        transform.position = worldPoint;
        SpawnObjects(TargetPos);
    }

    public void SpawnObjects(Vector3 pos)
    {
        GameObject spawn = GameObject.Instantiate(spawnItem, transform);
        spawn.transform.localPosition = Vector3.zero;

        Ray screenToPointer = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(screenToPointer, out hit, Mathf.Infinity))
        {
            Vector3 theVector = (transform.position - hit.point).normalized;
            spawn.GetComponent<Rigidbody>().velocity = new Vector3(theVector.x * 60, theVector.y * 60, theVector.z * -60 );
        }
    }
}
