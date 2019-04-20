using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameCanvas : MonoBehaviour
{
    public Text _text;
    // Start is called before the first frame update
    void Start()
    {
                    
    }

    // Update is called once per frame
    void Update()
    {
        int curCount = DataCenter.GetInstance().GetCount();
        _text.text = "Count : " + curCount.ToString();
    }
}
