using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    public EnemyHandler enemyStats;
    [SerializeField] private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"Collided with {other.name}");
        if (other.layer == 3)//layer is boss
        {
            if (this.gameObject.tag == other.tag)
            {
                enemyStats.health -= 5;
                if(enemyStats.health <= 0)
                {
                    playerStats.playerScore += 15;
                    playerStats.moneyAmount += 50;
                }
            }
        }
        else if (this.gameObject.tag == other.tag)
        {
            Destroy(other);
            playerStats.playerScore += 10;
            playerStats.moneyAmount += 20;
        }
        Debug.Log($"SCORE: {playerStats.playerScore}");
        Debug.Log($"BESOS: {playerStats.moneyAmount}");
    }
}
