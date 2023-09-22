using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    public GameObject StageSelectGroup;
    GameObject rightbutton;
    GameObject leftbutton;
    
    void Start()
    {
        rightbutton = StageSelectGroup.transform.GetChild(0).GetChild(2).gameObject;
        leftbutton = StageSelectGroup.transform.GetChild(0).GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
