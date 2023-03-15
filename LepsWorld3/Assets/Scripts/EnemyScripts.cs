using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScripts : MonoBehaviour
{
    public float start, end;

    private bool isRight; // isRight == true di chuyen phai,nguoc lai

    public GameObject player;

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
            if (positionPlayer > start && positionPlayer < end)
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
        if (positionEnemy < start)
        {
            isRight = true;
        }
        if (positionEnemy > end)
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

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "trai")
        {
            /*
            if(isRight == true){
                isRight = false; 
                else{
                    isRight = true;
                }
            */
            isRight = isRight ? false : true; //isRight = !isRight;
        }
    }

    public void SetStart(float start)
    {
        this.start = start;
    }

    public void SetEnd(float end)
    {
        this.end = end;
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
