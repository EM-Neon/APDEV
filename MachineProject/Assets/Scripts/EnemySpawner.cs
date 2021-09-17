using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;

    private void Awake()
    {
        for (int i = 0; i < enemies.Count; i++){
            enemies[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].SetActive(true);
        }
    }
}
