using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawn : MonoBehaviour
{
    public GameObject spawnObject;
    private ParticleSystem ps;
    private List<ParticleCollisionEvent> collisionEvents;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>(0);
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Collide!");

        int collCount = ps.GetSafeCollisionEventSize();

        if (collCount > collisionEvents.Count)
            collisionEvents = new List<ParticleCollisionEvent>(collCount);

        int eventCount = ps.GetCollisionEvents(other, collisionEvents);

        for(int i = 0; i < eventCount; i++)
        {
            GameObject spawn = GameObject.Instantiate(spawnObject, collisionEvents[i].intersection, Quaternion.identity);
            spawn.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void FixedUpdate()
    {

    }
}
