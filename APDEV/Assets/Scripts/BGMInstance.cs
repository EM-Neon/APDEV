using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMInstance : MonoBehaviour
{
    private static BGMInstance instance = null;
    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
            instance = this;
        else
            DestroyBGMDuplicate();
    }

    private void DestroyBGMDuplicate()
    {
        Destroy(this.gameObject);
    }

    private void DestroyBGM()
    {
        instance = null;
        Destroy(this.gameObject);
    }
}
