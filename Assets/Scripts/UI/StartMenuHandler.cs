using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuHandler : MonoBehaviour
{
    public int targetFrameRate = 30;

    public InputField name;
    public InputField password;

    public static int user_id = 0;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LogIn()
    {
        UserData user;
        if((user = DBHandler.authorize(name.text, password.text)) != null)
        {
            user_id = user.getUserId();
            SceneManager.LoadScene("Menu");
        }
        else
        {
            name.text = "";
            password.text = "";
        }
    }

    public void SignUp()
    {
        if (DBHandler.registration(name.text, password.text))
        {
            UserData user;
            if ((user = DBHandler.authorize(name.text, password.text)) != null)
            {
                user_id = user.getUserId();
                SceneManager.LoadScene("Menu");
            }
        }
        else
        {
            name.text = "";
            password.text = "";
        }
    }

    public void exit()
    {
        Application.Quit();
    }
}
