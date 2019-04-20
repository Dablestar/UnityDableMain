using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] GameObject _parentObject;
    [SerializeField] GameObject _characterPrefab;
    [SerializeField] List<GameObject> _wayPointList;
    [SerializeField] GameObject _currentWayPoint;

    // Use this for initialization
    void Start()
    {
        //Generate();
        StartCoroutine(ExecGenerate());
    }

    IEnumerator ExecGenerate()
    {
        while (true)
        {
            Generate();
            yield return new WaitForSeconds(5.0f);
        }
    }

    void Generate()
    {
        GameObject obj = GameObject.Instantiate<GameObject>(_characterPrefab);
        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
        obj.transform.SetParent(_parentObject.transform);

        Character character = obj.GetComponent<Character>();
        character.SetWaypointList(_wayPointList);
        DataCenter.GetInstance().AddCount();
    }
    public void setWaypointList(List<GameObject> wayPointList)
    {
        _wayPointList = wayPointList;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
