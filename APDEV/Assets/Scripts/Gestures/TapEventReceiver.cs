using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapEventReceiver : MonoBehaviour
{
    public GameObject spawnItem;
    private int timer;
    private int tick;

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
        spawn.transform.position = Camera.main.ScreenToWorldPoint(pos);
        Debug.Log($"Position {pos}");
        Ray screenToPointer = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(screenToPointer, out hit, Mathf.Infinity))
        {
            /*Vector3 theVector = (transform.position - hit.point).normalized;*/
            Vector3 theVector = screenToPointer.direction;
            /*spawn.GetComponent<Rigidbody>().velocity = new Vector3(theVector.x * -60, theVector.y * -60, theVector.z * -60);*/
        }
    }
}
