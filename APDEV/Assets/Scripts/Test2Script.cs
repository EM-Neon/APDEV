using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2Script : MonoBehaviour
{
    public GameObject spawnItem;
    private int timer;
    private int tick;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("MyEvent");
    }

   
    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                GameObject spawn = GameObject.Instantiate(spawnItem, transform);
                spawn.transform.localPosition = Vector3.zero;

                Ray screenToPointer = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(screenToPointer, out hit, Mathf.Infinity))
                {
                    Vector3 theVector = (transform.position - hit.point).normalized;
                    spawn.GetComponent<Rigidbody>().velocity = new Vector3(theVector.x * -60, theVector.y * -60, theVector.z * -60);
                }
            }    
        }

        
        /*if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            GameObject spawn = GameObject.Instantiate(spawnItem, transform);
            spawn.transform.localPosition = Vector3.zero;

            Ray screenToPointer = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(screenToPointer, out hit, Mathf.Infinity))
            {
                Vector3 theVector = (transform.position - hit.point).normalized;
                spawn.GetComponent<Rigidbody>().velocity = new Vector3(theVector.x * -60, theVector.y * -60, theVector.z * -60);
            }
        }*/
    }

     
}
