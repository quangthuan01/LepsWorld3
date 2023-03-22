using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScripts : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private bool isNen;
    public ParticleSystem psKhoi;
    public GameObject menu;
    public bool isPlaying = true;
    private int score = 0;
    public TMP_Text scoreText;
    public AudioSource soundScore;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   

        Vector2 scale = transform.localScale;

        animator.SetBool("isRunning", false);

        Quaternion rotation = psKhoi.transform.localRotation;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation.y = 180;
            psKhoi.transform.localRotation = rotation;
            psKhoi.Play();
            animator.SetBool("isRunning", true);
            scale.x = 1;
            transform.Translate(Vector3.right * 5f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {   
            rotation.y = 0;
            psKhoi.transform.localRotation = rotation;
            psKhoi.Play();
            animator.SetBool("isRunning", true);
            scale.x = -1;
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

    //input.getkey -> nhan giu hoat dong
    //getkeydowm -> nhan 1 lan hoat dong
    //getkeyup -> tha ra hoat dong
    //show menu
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

    private void OnTriggerEnter2D(Collider2D collider){

        if(collider.gameObject.tag == "coin"){
            soundScore.Play();
            score+=10; 
            scoreText.text = score + " x";
            Destroy(collider.gameObject);

        }

    }
}
