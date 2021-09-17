using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHandler : MonoBehaviour
{
    public Vector3 growthRate = new Vector3(0.001f, 0.001f, 0.001f);
    public Vector3 maxGrowth = new Vector3(2.0f, 2.0f, 2.0f);
    [SerializeField] private PlayerStats playerStats;
    public ParticleSystem particle;
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Grow();
        if (this.transform.localScale.x >= maxGrowth.x)
        {
            Explode();
        }
    }

    public void Grow()
    {
        this.transform.localScale += growthRate;
        Debug.Log($"transform{this.transform.localScale}");
    }

    private void Explode()
    {
        playerStats.playerScore -= 10;
        Vector3 position = new Vector3(0, 1, 0);
        GameObject explodeParticle = GameObject.Instantiate(particle.gameObject, this.transform.position + position, Quaternion.Euler(-90,0,0));
        explodeParticle.GetComponent<ParticleSystem>().Play();
        Destroy(this.gameObject);
    }
}
