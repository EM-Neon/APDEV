using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int hp = 3;

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            Debug.Log("U ded");
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
