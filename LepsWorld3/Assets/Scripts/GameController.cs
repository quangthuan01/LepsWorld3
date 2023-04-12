using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int count = 20;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (count-- > 0) 
        {
            float position = Random.Range(-5f, 177f);

            GameObject quaivat = (GameObject) Instantiate(Resources.Load("Prefabs/quaivat"),
            new Vector3(position, -3.5f, 0),
            Quaternion.identity);
            quaivat.GetComponent<EnemyScripts>().SetStart(position -5);
            quaivat.GetComponent<EnemyScripts>().SetEnd(position + 177);
            quaivat.GetComponent<EnemyScripts>().SetPlayer(player);
        }
    }
}
