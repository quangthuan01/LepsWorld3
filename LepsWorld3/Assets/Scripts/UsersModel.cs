using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsersModel
{
    public string username{get;set;}
    public string password{get;set;}

    public UsersModel(string username, string password)
    {
        this.username = username;
        this.password = password;
    }

}
