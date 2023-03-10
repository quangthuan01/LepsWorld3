using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    public GameObject player;

    public float

            start,
            end;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //lấy vị trí trục x của player
        var playerX = player.transform.position.x;

        //lấy vị trí trục x của camera
        var cameraX = transform.position.x;
        if (playerX > start && playerX < end)
        {
            cameraX = playerX; 
        }
        else
        {
            if (playerX < start)
            {
                cameraX = start;
            }
            if (playerX > end)
            {
                cameraX = end;
            }
        }
        transform.position = new Vector3(cameraX, 0, -10);
    }
}
