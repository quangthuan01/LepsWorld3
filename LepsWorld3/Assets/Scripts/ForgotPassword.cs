using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForgotPassword : MonoBehaviour
{
    public TMP_InputField txtUsername;

    public TMP_InputField txtOTP;

    public TMP_InputField txtNewPassword;

    public TMP_InputField txtConfirmPassword;

    public GameObject resetPassword;

    public GameObject sendOTP;

    public GameObject login;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    //goi API send OTP
    public void SendOTP()
    {
        //goi API send OTP
        var user = txtUsername.text;
        OTPModel otpModel = new OTPModel(user);
        StartCoroutine(SendOTPAPI(otpModel));
        SendOTPAPI (otpModel);
    }

    //goi API OTP
    IEnumerator SendOTPAPI(OTPModel otpModel)
    {
        string jsonStringRequest = JsonConvert.SerializeObject(otpModel); // chuyển đổi từ object sang json

        var request =
            new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/send-otp",
                "POST"); // gọi API
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw); // gửi dữ liệu lên server
        request.downloadHandler = new DownloadHandlerBuffer(); // nhận dữ liệu từ server
        request.SetRequestHeader("Content-Type", "application/json"); // gửi dữ liệu dạng json
        yield return request.SendWebRequest(); // gửi dữ liệu lên server

        if (
            request.result != UnityWebRequest.Result.Success // kiểm tra kết quả
        )
        {
            // errorText.text = "Login Failed";
            Debug.Log(request.error);
        }
        else
        {
            var jsonString = request.downloadHandler.text.ToString(); // nhận dữ liệu từ server
            ReponModel otpReponModel =
                JsonConvert.DeserializeObject<ReponModel>(jsonString); //   chuyển đổi từ json sang object
            if (otpReponModel.status == 1)
            {
                //thanh cong load panel reset
                resetPassword.SetActive(true);
                sendOTP.SetActive(false);
                Debug.Log("OTP Send Success");
            }
            else
            {
                Debug.Log("OTP Send Failed");
                // goi lai api
            }
        }
        request.Dispose();
    }

    //goi API reset password
    public void ResetPassword()
    {
        //goi API reset password
        var newpassword = txtNewPassword.text;
        var confirmpassword = txtConfirmPassword.text;

        if (newpassword.Equals(confirmpassword))
        {
            var user = txtUsername.text;
            int otp = int.Parse(txtOTP.text);
            ResetPasswordModel resetPasswordModel = new ResetPasswordModel(user, otp, newpassword);
            StartCoroutine(ResetPasswordAPI(resetPasswordModel));
            ResetPasswordAPI (resetPasswordModel);
        }else{
            Debug.Log("Password not match");
        }

        
    }

    IEnumerator ResetPasswordAPI(ResetPasswordModel resetPasswordModel)
    {
        string jsonStringRequest = JsonConvert.SerializeObject(resetPasswordModel); // chuyển đổi từ object sang json

        var request =
            new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/reset-password",
                "POST"); // gọi API
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw); // gửi dữ liệu lên server
        request.downloadHandler = new DownloadHandlerBuffer(); // nhận dữ liệu từ server
        request.SetRequestHeader("Content-Type", "application/json"); // gửi dữ liệu dạng json
        yield return request.SendWebRequest(); // gửi dữ liệu lên server

        if (
            request.result != UnityWebRequest.Result.Success // kiểm tra kết quả
        )
        {
            // errorText.text = "Login Failed";
            Debug.Log(request.error);
        }
        else
        {
            var jsonString = request.downloadHandler.text.ToString(); // nhận dữ liệu từ server
            ReponModel resetPassReponModel = JsonConvert.DeserializeObject<ReponModel>(jsonString); //   chuyển đổi từ json sang object
            if (resetPassReponModel.status == 1)
            {
                //thanh cong load panel reset
                resetPassword.SetActive(false);
                login.SetActive(true);
                Debug.Log("Reset Password Success");
            }
            else
            {
                Debug.Log("Reset Password Failed");
                // goi lai api
            }
        }
        request.Dispose();
    }
}
