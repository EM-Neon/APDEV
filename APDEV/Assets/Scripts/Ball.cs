using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Enemy enemyStats;
    [SerializeField] private int[] ballStrengths = new int[3];
    private int type;

    private IEnumerator Begone()
    {
        yield return new WaitForSeconds(3.0f);
        GameObject.Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        enemyStats = collision.gameObject.GetComponent<Enemy>();
        type = (int)enemyStats.myType;

        enemyStats.hp -= ballStrengths[type];


    }
}
