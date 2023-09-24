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
    [SerializeField] AudioClip StartaudioClip;
    [SerializeField] AudioClip BackaudioClip;
    [SerializeField] AudioClip SelectaudioClip;

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
    public float endValue;
    
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
        SoundManager_SE.m_Instane.PlaySoundEfect(BackaudioClip,0.2f);

        StageSelectGroup.GetComponent<RectTransform>().DOAnchorPosY(900.0f, 0.5f, true);
    }

    public void ToStart()
    {
        SoundManager_SE.m_Instane.PlaySoundEfect(StartaudioClip,0.2f);

        //camera.DOFieldOfView(endValue, 1.0f);
        Debug.Log("start");


        //SceneManager.LoadScene("MainScene");

        //ステージが複数できたら下の処理に変える

        switch(stagenumber)
        {
            case 0:
                GameManager.PlayStageNum = 0;
                SceneManager.LoadScene("MainScene");
            break;
            case 1:
                GameManager.PlayStageNum = 1;
                SceneManager.LoadScene("MainScene2");
            break;
            case 2:
                GameManager.PlayStageNum = 2;
                SceneManager.LoadScene("MainScene3");
            break;
        }

        
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
        SoundManager_SE.m_Instane.PlaySoundEfect(SelectaudioClip,0.2f);

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
        SoundManager_SE.m_Instane.PlaySoundEfect(SelectaudioClip,0.2f);

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
