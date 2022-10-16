using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Data_Client
{
    public string Client_Name;
    public List<Product> Products = new List<Product>();
    public DateTime Time;
    public int Total_Cash;
}
