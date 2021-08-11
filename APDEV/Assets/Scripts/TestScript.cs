using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
   /* void OnMouseUp()
    {
        Ray screenToPointer = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (GetComponent<Collider>().Raycast(screenToPointer, out hit, Mathf.Infinity))
        {
            Vector3 theVector = (transform.position - hit.point).normalized;
            GetComponent<Rigidbody>().velocity = new Vector3(theVector.x * 10, theVector.y * 10, theVector.z * 10);
        }
    }*/

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            Ray screenToPointer = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(screenToPointer, out hit, Mathf.Infinity))
            {
                Vector3 theVector = (transform.position - hit.point).normalized;
                GetComponent<Rigidbody>().velocity = new Vector3(theVector.x * -50, theVector.y * -40 + 20, theVector.z * -50);
            }
        }
    }
}
