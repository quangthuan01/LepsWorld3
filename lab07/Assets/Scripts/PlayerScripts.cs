using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using System;

public class PlayerScripts : MonoBehaviour
{
    public GameObject menu;
    private bool isNen;
    private bool isRight = true;
    private Rigidbody2D rigidbody2D;
    private int score = 0;
    public TMP_Text scoreText;
    public bool isPlaying = true;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    rigidbody2D = GetComponent<Rigidbody2D>();
    // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 scale = transform.localScale;
        // animator.SetBool("isRunning", false);
        if (Input.GetKey(KeyCode.D))
        {   
            isRight = true;
            scale.x = 1;
            // animator.SetBool("isRunning", true);
            transform.Translate(Vector3.right * 5f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {   
            isRight = false;
            scale.x = -1;
            // animator.SetBool("isRunning", true);
            transform.Translate(Vector3.left * 5f * Time.deltaTime);
        }

        transform.localScale = scale;

        if (Input.GetKey(KeyCode.Space))
        {
            if (isNen)
            {
                //transform.Translate(Vector3.up * 5f *Time.deltaTime);
                rigidbody2D.AddForce(new Vector2(0, 350));
                isNen = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.P))
            {   
                showMenu();
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

    private void OnTriggerEnter2D(Collider2D collider){

        if(collider.gameObject.tag == "coin"){
            score+=10; 
            scoreText.text = score + " x";
            Destroy(collider.gameObject);

        }
    }

    public void showMenu()
    {
        if(isPlaying)
        {
            menu.SetActive(true);
            Time.timeScale = 0;//dung game
            isPlaying = false;
        }
        else
        {
            menu.SetActive(false);
            Time.timeScale = 1;//choi game
            isPlaying = true;
        }
    }

    public void reStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
