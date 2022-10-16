using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using CodeStage.AntiCheat.Storage;
using CodeStage.AntiCheat.ObscuredTypes;
//using Photon.Pun;
//using System;

[System.Serializable]
public class Data_User
{
    public List<Product> ProductList = new List<Product>();
    public List<Data_Client> ClientList = new List<Data_Client>();
}
