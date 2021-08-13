using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{

    public EnemyType myType;
    public enum EnemyType {Power, Speed, Regular};
    private readonly float[] leftOrRight = { 1, -1 };

    [SerializeField] private GameObject player;
    [SerializeField] private Material[] typeColors = new Material[3];
    [SerializeField] private float[] shootSpeed = new float[3];
    [SerializeField] private float[] moveSpeed = new float[3];
    [SerializeField] private int[] enemyHPTypes = new int[3];
    [SerializeField] public int hp;
    [SerializeField] private UltimateBar ultimateCounter;
    int deadEnemies = 0;

    float moveTick = 0;
    float moveDelay;
    float moveDelay2;
    int randomLeftOrRight;

    float shootTick = 0;
    float shootDelay;
    float shootDelay2;

    private IEnumerator despawn()
    {
        yield return new WaitForSeconds(6.0f);

        GameObject.Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("despawn");

        int type = Random.Range(0, 3);
        myType = (EnemyType)type;

        this.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = typeColors[type];
        hp = enemyHPTypes[type];

        Debug.Log(myType);

        moveDelay = Random.Range(0.5f, 1.0f);
        moveDelay2 = Random.Range(0.25f, 0.5f) + moveDelay;
        randomLeftOrRight = Random.Range(0, 2);

        shootDelay = Random.Range(0.5f, 1.0f);
        shootDelay2 = Random.Range(0.5f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }

        moveTick += Time.deltaTime;
        if (moveTick >= moveDelay)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(leftOrRight[randomLeftOrRight] * (3 * moveSpeed[(int)myType]), 0, 0);
            if (moveTick >= moveDelay2)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                moveTick = 0;
                moveDelay = Random.Range(0.5f, 1.0f);
                moveDelay2 = Random.Range(0.25f, 0.5f) + moveDelay;
                randomLeftOrRight = Random.Range(0, 2);
            }
        }

        shootTick += Time.deltaTime;
        if(shootTick >= shootDelay)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount -= 1.0f / shootSpeed[(int)myType] * Time.deltaTime;
            if(shootTick >= shootDelay + shootSpeed[(int)myType])
            {
                player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Player>().hp--;

                this.transform.GetChild(0).gameObject.SetActive(false);
                this.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = 1;
                if(shootTick >= shootDelay + shootSpeed[(int)myType] + shootDelay2)
                {
                    shootTick = 0;
                    shootDelay = Random.Range(0.5f, 1.0f);
                    shootDelay2 = Random.Range(0.5f, 1.0f);
                }
            }
        }
    }
}