using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCenter
{
    int i;
    static int j;
    //Singleton Class
    static DataCenter _instance = null;
    public static DataCenter GetInstance()
    {
        if(null == _instance)
        {
            _instance = new DataCenter();
        }
        return _instance;
        
    }
    int _count = 0;
    public void AddCount()
    {
        _count++;
        Debug.Log("count : " + _count);
    }
    public int GetCount()
    {
        return _count;
    }
}
