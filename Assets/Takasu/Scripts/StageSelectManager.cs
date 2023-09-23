using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class StageSelectManager : MonoBehaviour
{
    Vector2 OriginalScale;

    public GameObject StageSelectGroup;
    GameObject stageimage;
    GameObject stagenametext;
    GameObject rightbutton;
    GameObject leftbutton;
    GameObject difficultytext;

    public int stagenumber;

    public Sprite[] stagesprite;
    public string[] stagename;
    public string [] difficulty;
    
    void Start()
    {
        OriginalScale = transform.localScale;

        stageimage = StageSelectGroup.transform.GetChild(0).GetChild(0).gameObject;
        stagenametext = StageSelectGroup.transform.GetChild(0).GetChild(1).gameObject;
        rightbutton = StageSelectGroup.transform.GetChild(0).GetChild(2).gameObject;
        leftbutton = StageSelectGroup.transform.GetChild(0).GetChild(3).gameObject;
        difficultytext = StageSelectGroup.transform.GetChild(0).GetChild(6).gameObject;
        
        stagenumber = 0;
        
        SetStageInfo(stagenumber);

    }

    public void ToTitle()
    {
        StageSelectGroup.GetComponent<RectTransform>().DOAnchorPosY(900.0f, 0.5f, true);
    }

    public void ToStart()
    {
        SceneManager.LoadScene("MainScene");

        //ステージが複数できたら下の処理に変える
/*
        switch(stagenumber)
        {
            case 0:
                SceneManager.LoadScene("ステージ名");
            break;
            case 1:
                SceneManager.LoadScene("ステージ名");
            break;
            case 2:
                SceneManager.LoadScene("ステージ名");
            break;
        }
*/
        
    }

    void SetStageInfo(int stagenumber)
    {
        stageimage.GetComponent<Image>().sprite = stagesprite[stagenumber];
        stagenametext.GetComponent<TextMeshProUGUI>().text = stagename[stagenumber];
        difficultytext.GetComponent<TextMeshProUGUI>().text = difficulty[stagenumber];

        if(stagenumber == 0)
        {
            leftbutton.GetComponent<Button>().interactable = false;
        }
        if(stagenumber == 2)
        {
            rightbutton.GetComponent<Button>().interactable = false;
        }
    }

    public void OnRightButton()
    {
        stagenumber++;

        leftbutton.GetComponent<Button>().interactable = true;

        SetStageInfo(stagenumber);

        if(stagenumber == 2)
        {
            ScaleDown();
        }
    }

    public void OnLeftButton()
    {
        stagenumber--;

        rightbutton.GetComponent<Button>().interactable = true;

        SetStageInfo(stagenumber);

        if(stagenumber == 0)
        {
            ScaleDown();
        }
    }

    public void ScaleDown()
    {
        Debug.Log("scaledown");
        transform.DOScale(OriginalScale, 0.24f).SetUpdate(true).SetEase(Ease.OutCubic);
    }
}
