using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
     private bool isRight; // isRight == true di chuyen phai,nguoc lai
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         /* Following the player. */
        //di theo player
        var positionEnemy = transform.position.x;

        if (player != null)
        {
            var positionPlayer = player.transform.position.x;
            if (positionPlayer > -2f && positionPlayer < 5f)
            {
                if (positionPlayer < positionEnemy)
                {
                    isRight = false;
                }
                if (positionPlayer > positionEnemy)
                {
                    isRight = true;
                }
            }
        }

        //dichuyen
        if (positionEnemy < -2f)
        {
            isRight = true;
        }
        if (positionEnemy > 5f)
        {
            isRight = false;
        }
        Vector2 scale = transform.localScale;
        if (isRight)
        {
            scale.x = -1;
            transform.Translate(Vector3.right * 2f * Time.deltaTime);
        }
        else
        {
            scale.x = 1;
            transform.Translate(Vector3.left * 2f * Time.deltaTime);
        }
        transform.localScale = scale;
        
    }
}
