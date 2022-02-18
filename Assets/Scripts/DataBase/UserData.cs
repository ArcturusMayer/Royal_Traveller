using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    private int user_id;
    private string username;
    private string password;

    private static List<UserData> users = new List<UserData>();

    public UserData(int user_id, string username, string password)
    {
        this.user_id = user_id;
        this.username = username;
        this.password = password;
        users.Add(this);
    }

    public static UserData authorization(string username, string password)
    {
        foreach(UserData user in users)
        {
            if(user.username == username)
            {
                if(user.password == password)
                {
                    return user;
                }
            }
        }
        return null;
    }

    public static bool isNameFree(string username)
    {
        bool isAllowed = true;
        foreach(UserData user in users)
        {
            if(user.username == username)
            {
                isAllowed = false;
            }
        }
        return isAllowed;
    }

    public int getUserId()
    {
        return user_id;
    }
}
