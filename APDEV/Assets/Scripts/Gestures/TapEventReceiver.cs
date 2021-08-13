using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEventReceiver : MonoBehaviour
{
    public GameObject spawnItem;
    private int timer;
    private int tick;
    public Ball ballStats;

    // Start is called before the first frame update
    void Start()
    {
        GestureManager.Instance.OnTap += onTap; // assign function onTap
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnTap -= onTap; // deassign function
    }

    public void onTap(object sender, TapEventArgs args)
    {
        Ray r = Camera.main.ScreenPointToRay(args.TapPosition);
        SpawnObject(args.TapPosition);
    }

    public void SpawnObject(Vector3 pos)
    {
        GameObject spawn = GameObject.Instantiate(spawnItem, transform);
        spawn.transform.localPosition = Vector3.zero;
        ballStats = spawn.GetComponent<Ball>();

        Ray screenToPointer = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(screenToPointer, out hit, Mathf.Infinity))
        {
            Vector3 theVector = (transform.position - hit.point).normalized;
            spawn.GetComponent<Rigidbody>().velocity = new Vector3(theVector.x * -60 * ballStats.ballSpeed[ballStats.type], theVector.y * -60 * ballStats.ballSpeed[ballStats.type], theVector.z * -60 * ballStats.ballSpeed[ballStats.type]);
        }
    }
}
