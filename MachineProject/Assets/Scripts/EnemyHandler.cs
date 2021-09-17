using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class EnemyHandler : MonoBehaviour
{
    public Vector3 growthRate = new Vector3(0.001f, 0.001f, 0.001f);
    public Vector3 maxGrowth = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Animator animator;
    public ParticleSystem particle;
    public int health = 100;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        if(this.gameObject.layer == 3)
        {
            animator.SetTrigger("Dance");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(this.gameObject.layer == 6)
        {
            Grow();
            if (this.transform.localScale.x >= maxGrowth.x)
            {
                Explode();
            }
        }
        else if (this.gameObject.layer == 3)
        {
            onBoss();
        }
    }

    public void Grow()
    {
        this.transform.localScale += growthRate;
        Debug.Log($"transform{this.transform.localScale}");
    }

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
    }

    private void onBoss()
    {
        timer += Time.fixedDeltaTime;
        if(timer >= 5.0f)
        {
            timer = 0;
            Vector3 position = new Vector3(0, 1, 0);
            animator.SetTrigger("Throw");
            GameObject explodeParticle = GameObject.Instantiate(particle.gameObject, this.transform.position + position, Quaternion.Euler(-90, 0, 0));
            explodeParticle.GetComponent<ParticleSystem>().Play();
        }
    }

    private void Explode()
    {
        playerStats.playerScore -= 10;
        Vector3 position = new Vector3(0, 1, 0);
        GameObject explodeParticle = GameObject.Instantiate(particle.gameObject, this.transform.position + position, Quaternion.Euler(-90,0,0));
        explodeParticle.GetComponent<ParticleSystem>().Play();
        Destroy(this.gameObject);
    }

    public void onDeath()
    {
        animator.SetTrigger("Dead");
    }
}
