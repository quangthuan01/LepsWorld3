using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SigninScripts : MonoBehaviour
{
    public TMP_InputField username;

    public TMP_InputField password;

    public TMP_Text errorText;

    public Selectable first;

    private EventSystem eventSystem;

    public Button loginButton;

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
        first.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            loginButton.onClick.Invoke();
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            Selectable next =
                eventSystem
                    .currentSelectedGameObject
                    .GetComponent<Selectable>()
                    .FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Selectable next =
                eventSystem
                    .currentSelectedGameObject
                    .GetComponent<Selectable>()
                    .FindSelectableOnUp();
            if (next != null)
            {
                next.Select();
            }
        }
    }

    public void CheckLogin()
    {
        var user = username.text;
        var pass = password.text;

        if (username.text == "admin" && password.text == "admin")
        {
            SceneManager.LoadScene(1);

            //SceneManager.LoadScene("main");
            Debug.Log("Login Success");
        }
        else
        {
            errorText.text = "Login Failed";
            Debug.Log("Login Failed");
        }
    }
}
