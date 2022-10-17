using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product
{
    public int Idx;
    public int Product_Idx;
    public string Product_Name;
    public string Product_Option;
    public int Box_Count;
    public int Box_Per_Count;
    public int Person_Per_Count;
    public int Total_Person;
    public int Error_Count;
    public float Remain_Count;
    public float Origin_Remain_Count;
    public float Price_Per_Person;
    public int Count = 1;

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    //public Product(Product product = null)
    //{
    //    Idx = product.Idx;
    //    Product_Idx = product.Idx;
    //    Product_Name = product.Product_Name;
    //    Product_Option = product.Product_Option;
    //    Box_Count = product.Box_Count;
    //    Box_Per_Count = product.Box_Per_Count;
    //    Person_Per_Count = product.Person_Per_Count;
    //    Total_Person = product.Total_Person;
    //    Remain_Count = product.Remain_Count;
    //    Price_Per_Person = product.Price_Per_Person;
    //}
}
