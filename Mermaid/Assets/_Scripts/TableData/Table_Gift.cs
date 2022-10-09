using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;

public class Table_Gift : Table_Base
{
    public Table_Gift(string[] data)
    {
        if (false == string.IsNullOrEmpty(data[0]))
            idx = System.Convert.ToInt32(data[0]);
        if (false == string.IsNullOrEmpty(data[1]))
            product_idx = System.Convert.ToInt32(data[1]);
        name = data[2];
        option = data[3];
        if (false == string.IsNullOrEmpty(data[4]))
            box_count = System.Convert.ToInt32(data[4]);
        if (false == string.IsNullOrEmpty(data[5]))
            box_per_count = System.Convert.ToInt32(data[5]);
        if (false == string.IsNullOrEmpty(data[6]))
            person_per_count = System.Convert.ToInt32(data[6]);
        if (false == string.IsNullOrEmpty(data[7]))
            total_person = System.Convert.ToInt32(data[7]);
        if (false == string.IsNullOrEmpty(data[8]))
            error_count = System.Convert.ToInt32(data[8]);
        if (false == string.IsNullOrEmpty(data[9]))
            remain_count = System.Convert.ToInt32(data[9]);
        if (false == string.IsNullOrEmpty(data[10]))
            buy_price = System.Convert.ToInt32(data[10]);
        if (false == string.IsNullOrEmpty(data[11]))
            total_buy_price = System.Convert.ToInt32(data[11]);
        if (false == string.IsNullOrEmpty(data[12]))
            sell_price = System.Convert.ToInt32(data[12]);
        if (false == string.IsNullOrEmpty(data[13]))
            total_sell_price = System.Convert.ToInt32(data[13]);
        if (false == string.IsNullOrEmpty(data[14]))
            price_per_person = System.Convert.ToSingle(data[14]);
    }

    public ObscuredInt idx;
    public ObscuredInt product_idx;
    public ObscuredString name;
    public ObscuredString option;
    public ObscuredInt box_count;
    public ObscuredInt box_per_count;
    public ObscuredInt person_per_count;
    public ObscuredInt total_person;
    public ObscuredInt error_count;
    public ObscuredInt remain_count;
    public ObscuredInt buy_price;
    public ObscuredInt total_buy_price;
    public ObscuredInt sell_price;
    public ObscuredInt total_sell_price;
    public ObscuredFloat price_per_person;
}