using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScripts : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    private bool isNen;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * 5f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * 5f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (isNen)
            {
                //transform.Translate(Vector3.up * 5f *Time.deltaTime);
                rigidbody2D.AddForce(new Vector2(0, 200));
                isNen = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "nen")
        {
            isNen = true;
            // Destroy (gameObject);
        }
    }
}
