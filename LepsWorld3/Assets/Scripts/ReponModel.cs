using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReponModel {
    public int status{get;set;}
    public string notification{get;set;}

    public ReponModel(int status, string notification)
    {
        this.status = status;
        this.notification = notification;
    }
}

