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
    // 
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

        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("isRunning", true);
            scale.x = 1;
            transform.Translate(Vector3.right * 5f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {   animator.SetBool("isRunning", true);
            scale.x = -1;
            transform.Translate(Vector3.left * 5f * Time.deltaTime);
        }

        transform.localScale = scale;

        if (Input.GetKey(KeyCode.Space))
        {
            if (isNen)
            {
                psKhoi.Play();
                //transform.Translate(Vector3.up * 5f *Time.deltaTime);
                rigidbody2D.AddForce(new Vector2(0, 200));
                isNen = false;
            }
        }

    //input.getkey -> nhan giu hoat dong
    //getkeydowm -> nhan 1 lan hoat dong
    //getkeyup -> tha ra hoat dong
    //show menu
    if(Input.GetKeyDown(KeyCode.P))
    {   
        if(isPlaying)
        {
            showMenu();
        }
        else
        {
            menu.SetActive(false);
            Time.timeScale = 1;//choi game
            isPlaying = true;
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

    public void showMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0;//dung game
        isPlaying = false;
    }

    // public void OnTriggerEnter2D(OnCollisionEnter2D collision){

    //     if(collision.gameObject.tag == "coin"){
    //         score+=10;
            
    //         Destroy(collision.gameObject);
    //     }

    // }
}
