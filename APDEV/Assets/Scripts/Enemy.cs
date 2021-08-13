using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    enum EnemyType {Power, Speed, Regular};
    private EnemyType myType;
    private float[] leftOrRight = { 1, -1 };

    [SerializeField] private Material[] typeColors = new Material[3];
    [SerializeField] private float[] shootSpeed = new float[3];
    [SerializeField] private float[] moveSpeed = new float[3];

    float moveTick = 0;
    float moveDelay;
    float moveDelay2;
    int randomLeftOrRight;

    float shootTick = 0;
    float shootDelay;
    float shootDelay2;

    // Start is called before the first frame update
    void Start()
    {
        int type = Random.Range(0, 3);
        myType = (EnemyType)type;

        this.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = typeColors[type];
        
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