using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointList : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
       int wayPointCount =  transform.childCount;
        List<GameObject> wayPointList = new List<GameObject>();
        for (int i = 0; i<wayPointCount; i++)
        {
            GameObject wayPointObject = transform.GetChild(i).gameObject;
            wayPointList.Add(wayPointObject);
        }
        //WayPoint 스크립트의 리스트 세팅
        for (int i = 0; i < wayPointCount; i++)
        {
            GameObject wayPointObject = transform.GetChild(i).gameObject;
            WayPoint wayPointScript = wayPointObject.GetComponent<WayPoint>();
            wayPointScript.setWaypointList(wayPointList);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
