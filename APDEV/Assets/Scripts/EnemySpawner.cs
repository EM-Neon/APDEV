using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject playerPos;

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            GameObject temp = GameObject.Instantiate(enemy, playerPos.transform);
            temp.gameObject.transform.localPosition = new Vector3(0, 2, 10);
        }
    }
}
