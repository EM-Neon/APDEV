using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Enemy enemyStats;
    [SerializeField] private int[] ballStrengths = new int[3];
    [SerializeField] public float[] ballSpeed = new float[3];
    public int type;
    public Material blueBall;
    public Material redBall;
    public Material yellowBall;

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
        StartCoroutine("Begone");

        if(collision.gameObject.tag == "Enemy")
        {
            enemyStats = collision.gameObject.GetComponent<Enemy>();
            if(this.gameObject.GetComponent<MeshRenderer>().material == blueBall)
            {
                type = 2;
            }
            else if (this.gameObject.GetComponent<MeshRenderer>().material == yellowBall)
            {
                type = 1;
            }
            else if (this.gameObject.GetComponent<MeshRenderer>().material == redBall)
            {
                type = 0;
            }
            enemyStats.hp -= ballStrengths[type];
        }
    }
}
