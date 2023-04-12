using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScripts : MonoBehaviour
{
    private bool isNen;

    private bool isRight = true;

    public bool isPlaying = true;

    private Animator animator;

    private Rigidbody2D rigidbody2D;

    public float myValue = 0; // the total

    public TMP_Text scoreText;

    public GameObject menu;

    // public bool isPlaying = true;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //tang diem theo time
            myValue += Time.deltaTime;

            //We only need to update the text if the score changed.
            scoreText.text = myValue.ToString();
        

        //di chuyen
        Vector2 scale = transform.localScale;
        animator.SetBool("isRunning", false);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            isRight = true;
            scale.x = 1;
            animator.SetBool("isRunning", true);
            transform.Translate(Vector3.right * 5f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isRight = false;
            scale.x = -1;
            animator.SetBool("isRunning", true);
            transform.Translate(Vector3.left * 5f * Time.deltaTime);
        }

        transform.localScale = scale;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space))
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

        if (collider.gameObject.tag == "trai")
        {
            // Destroy (gameObject);

            Time.timeScale = 0;//dung game
            
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

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
