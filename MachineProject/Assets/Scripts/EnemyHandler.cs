using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.Audio;

public class EnemyHandler : MonoBehaviour
{
    public Vector3 growthRate = new Vector3(0.002f, 0.002f, 0.002f);
    public Vector3 maxGrowth = new Vector3(1.5f, 1.5f, 1.5f);
    public PlayerStats playerStats;
    public Animator animator;
    public AnimationSFX bossAudio;
    public GameObject panel;
    public Text score;
    public Text money;
    public ParticleSystem particle;
    public float health = 300;
    private float timer = 0;
    private float deathTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        if(this.gameObject.layer == 3)
        {
            bossAudio.onSpawn();
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
        if(other.tag == "Clear")
            this.gameObject.SetActive(false);

        playerStats.playerScore -= 30;
    }

    private void onBoss()
    {
        timer += Time.fixedDeltaTime;
        if(timer >= 5.0f)
        {
            timer = 0;
            Vector3 position = new Vector3(0, 1, 0);
            animator.SetTrigger("Throw");
            bossAudio.onThrow();
            GameObject explodeParticle = GameObject.Instantiate(particle.gameObject, this.transform.position + position, Quaternion.Euler(-90, 0, 0));
            explodeParticle.GetComponent<ParticleSystem>().Play();
        }
    }

    private void Explode()
    {
        playerStats.playerScore -= 30;
        Vector3 position = new Vector3(0, 1, 0);
        GameObject explodeParticle = GameObject.Instantiate(particle.gameObject, this.transform.position + position, Quaternion.Euler(-90,0,0));
        explodeParticle.GetComponent<ParticleSystem>().Play();
        Destroy(this.gameObject);
    }

    public void onDeath()
    {
        animator.SetTrigger("Dead");
        StartCoroutine(bossDeath());
    }

    IEnumerator bossDeath()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
        panel.SetActive(true);
        Time.timeScale = 0;
        score.text = "Score: " + playerStats.playerScore;
        money.text = "Besos: " + playerStats.moneyAmount;
        // checks if all the current level is unlocked as it unlocks the next level for selection
        if (playerStats.levelUnlocked[0])
        {
            playerStats.levelUnlocked[1] = true;
        }
        else if (playerStats.levelUnlocked[1])
        {
            playerStats.levelUnlocked[2] = true;
        }
        if(playerStats.playerScore < 2000)
        {
            Debug.Log("GameOver: Cesar put something here");
        }
    }
}
