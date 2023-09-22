using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public GameObject StageSelectGroup;
    GameObject rightbutton;
    GameObject leftbutton;

    public int stagenumber;
    
    void Start()
    {
        rightbutton = StageSelectGroup.transform.GetChild(0).GetChild(2).gameObject;
        leftbutton = StageSelectGroup.transform.GetChild(0).GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToTitle()
    {
        StageSelectGroup.GetComponent<RectTransform>().DOAnchorPosY(900.0f, 0.5f, true);
    }

    public void ToStart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
