using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;  
using System.Text;
using System;

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

        // load lai diem cua player, vi tri cua player
        if(SigninScripts.informationModel.score >= 0){
            score = SigninScripts.informationModel.score;
            scoreText.text = score + " x";
        }      

        if(SigninScripts.informationModel.positionX != null 
        && SigninScripts.informationModel.positionY != null 
        && SigninScripts.informationModel.positionZ != null){

            var positionX = float.Parse(SigninScripts.informationModel.positionX);
            var positionY = float.Parse(SigninScripts.informationModel.positionY);
            var positionZ = float.Parse(SigninScripts.informationModel.positionZ);

            transform.position = new Vector3(positionX, positionY, positionZ);

        }
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

        if(collision.gameObject.tag == "quaman"){
            SceneManager.LoadScene(3);
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

        if(collider.gameObject.tag == "checkpoints"){
            SavePosition();
        }

    }

    public void SaveScore(){
        var user = SigninScripts.informationModel.username;
        ScoreModel scoreModel = new ScoreModel (score, user);
        StartCoroutine(SaveScoreAPI(scoreModel));
        SaveScoreAPI(scoreModel);
    }

    //api luu score

    IEnumerator SaveScoreAPI(ScoreModel scoreModel)
    {
        string jsonStringRequest = JsonConvert.SerializeObject(scoreModel);// chuyển đổi từ object sang json

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/save-score", "POST");// gọi API
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);// gửi dữ liệu lên server
        request.downloadHandler = new DownloadHandlerBuffer();// nhận dữ liệu từ server
        request.SetRequestHeader("Content-Type", "application/json");// gửi dữ liệu dạng json
        yield return request.SendWebRequest();// gửi dữ liệu lên server

        if (request.result != UnityWebRequest.Result.Success)// kiểm tra kết quả
        {
            // errorText.text = "Login Failed";
            Debug.Log(request.error);
        }
        else
        {
            var jsonString = request.downloadHandler.text.ToString();// nhận dữ liệu từ server
            ReponModel scoreReponModel = JsonConvert.DeserializeObject<ReponModel>(jsonString);//   chuyển đổi từ json sang object
            if (scoreReponModel.status == 1)
            {
                showMenu();
                Debug.Log("Save Score Success");
            }
            else
            {
                Debug.Log("Save Score Failed");
            }
        }
        request.Dispose();
    }


    public void SavePosition(){

        var user = SigninScripts.informationModel.username;
        var positionX = transform.position.x;
        var positionY = transform.position.y;
        var positionZ = transform.position.z;

        SavePositionModel positionModel = new SavePositionModel (user, positionX.ToString(), positionY.ToString(), positionZ.ToString());
        StartCoroutine(SaveScoreAPI(positionModel));
        SaveScoreAPI(positionModel);

    }

     //Save Position

    IEnumerator SaveScoreAPI(SavePositionModel positionModel)
    {
        string jsonStringRequest = JsonConvert.SerializeObject(positionModel);// chuyển đổi từ object sang json

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/save-position", "POST");// gọi API
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);// gửi dữ liệu lên server
        request.downloadHandler = new DownloadHandlerBuffer();// nhận dữ liệu từ server
        request.SetRequestHeader("Content-Type", "application/json");// gửi dữ liệu dạng json
        yield return request.SendWebRequest();// gửi dữ liệu lên server

        if (request.result != UnityWebRequest.Result.Success)// kiểm tra kết quả
        {
            // errorText.text = "Login Failed";
            Debug.Log(request.error);
        }
        else
        {
            var jsonString = request.downloadHandler.text.ToString();// nhận dữ liệu từ server
            ReponModel locationReponModel = JsonConvert.DeserializeObject<ReponModel>(jsonString);//   chuyển đổi từ json sang object
            if (locationReponModel.status == 1)
            {
                Debug.Log("Save Score Success");
            }
            else
            {
                Debug.Log("Save Score Failed");
                // goi lai api 
            }
        }
        request.Dispose();
    }
}
