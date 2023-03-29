using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPasswordModel
{
    public string username{get;set;}
    public int otp{get;set;}
    public string newpassword{get;set;}
    
    public ResetPasswordModel(string username, int otp, string newpassword)
    {
        this.username = username;
        this.otp = otp;
        this.newpassword = newpassword;
    }
}
