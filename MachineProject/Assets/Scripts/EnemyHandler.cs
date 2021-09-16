using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public Vector3 growthRate = new Vector3(0.001f, 0.001f, 0.001f);
    public Vector3 maxGrowth = new Vector3(2.0f, 2.0f, 2.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Grow();
        if(this.transform.localScale.x >= maxGrowth.x)
        {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.tag == "Boss")
        {
            Debug.Log($"Boss Hit");
        }
    }

    private void Grow()
    {
        this.transform.localScale += growthRate;
        Debug.Log($"transform{this.transform.localScale}");
    }

    private void Explode()
    {

    }
}
