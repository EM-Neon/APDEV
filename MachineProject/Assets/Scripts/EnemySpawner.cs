using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    public Slider slider;
    private void Awake()
    {
        for (int i = 0; i < enemies.Count; i++){
            enemies[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MainCamera")
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i] != null)
                {
                    if(enemies[i].gameObject.layer == 3)
                    {
                        slider.value = 300;
                        slider.gameObject.SetActive(true);
                    }
                    enemies[i].SetActive(true);
                }
                    
            }
        }
        
    }
}
