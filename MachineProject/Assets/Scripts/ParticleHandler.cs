using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ParticleHandler : MonoBehaviour
{
    [SerializeField] private EnemyHandler enemyStats;
    [SerializeField] private PlayerStats playerStats;
    public List<Slider> slider;
    private bool ultReady = false;
    private int damage = 5;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();

        slider[0].value = 20;
        slider[1].value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(slider[0].value >= 20)
        {
            playerStats.canUlt = true;
            ultReady = true;
        }
        if(playerStats.isUlting)
        {
            slider[0].value = playerStats.ultimateCount;
            Debug.Log($"Slide Value: {slider[0].value}");
        }
        else if (playerStats.canUlt == false)
        {
            ultReady = false;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"Collided with {other.name}");
        if(other.layer == 6 || other.layer == 3)
        {
            enemyStats = other.GetComponent<EnemyHandler>();
        }
        
        if(playerStats.isUlting)
        {
            damage = 100;
        }
        else
        {
            damage = 5;
        }
        if (other.layer == 3)//layer is boss
        {
            if (this.gameObject.tag == other.tag)
            {
                enemyStats.health -= damage;
            }
            else if (this.gameObject.tag != other.tag)
            {
                if (other.CompareTag("Regular") || other.CompareTag("Electric") || other.CompareTag("Gas"))
                {
                    enemyStats.health -= damage / 2;
                }
            }
            if (enemyStats.health <= 0)
                {
                    playerStats.playerScore += 15;
                    playerStats.moneyAmount += 50;
                    Destroy(other);
                }
            slider[1].value = enemyStats.health;
        }
        else if (this.gameObject.tag == other.tag)
        {
            Destroy(other);
            playerStats.playerScore += 10;
            playerStats.moneyAmount += 20;
            slider[0].value += 1;
        }
        else if (this.gameObject.tag != other.tag)
        {
            if (other.CompareTag("Regular") || other.CompareTag("Electric") || other.CompareTag("Gas"))
            {
                enemyStats.Grow();
            }
        }
        Debug.Log($"SCORE: {playerStats.playerScore}");
        Debug.Log($"BESOS: {playerStats.moneyAmount}");
    }
}
