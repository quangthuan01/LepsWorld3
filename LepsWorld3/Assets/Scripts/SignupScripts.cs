using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
using System;
public class SignupScripts : MonoBehaviour
{

    public TMP_InputField username;

    public TMP_InputField password;

    public Button loginButton;

    public TMP_Text errorText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckLogin()
    {
        var user = username.text;
        var pass = password.text;
        //goi API

        UsersModel userModel = new UsersModel(user, pass);
        StartCoroutine(Login(userModel));
        Login(userModel);
    }

    IEnumerator Login(UsersModel userModel)
    {
        string jsonStringRequest = JsonConvert.SerializeObject(userModel);// chuyển đổi từ object sang json

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/register", "POST");// gọi API
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);// gửi dữ liệu lên server
        request.downloadHandler = new DownloadHandlerBuffer();// nhận dữ liệu từ server
        request.SetRequestHeader("Content-Type", "application/json");// gửi dữ liệu dạng json
        yield return request.SendWebRequest();// gửi dữ liệu lên server

        if (request.result != UnityWebRequest.Result.Success)// kiểm tra kết quả
        {
            errorText.text = "Login Failed";
            Debug.Log(request.error);
        }
        else
        {
            var jsonString = request.downloadHandler.text.ToString();// nhận dữ liệu từ server
            InformationModel informationModel = JsonConvert.DeserializeObject<InformationModel>(jsonString);//   chuyển đổi từ json sang object
            if (informationModel.status == 1)
            {
                SceneManager.LoadScene(0);
                Debug.Log("Register Success");
            }
            else
            {
                errorText.text = informationModel.notification;
                Debug.Log("Register Failed");
            }
        }
    }
}
