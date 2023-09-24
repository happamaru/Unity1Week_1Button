using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class AsobikataManager : MonoBehaviour
{
    [SerializeField] AudioClip SelectaudioClip;
    [SerializeField] AudioClip BackaudioClip;

    public GameObject AsobikataGroup;
    public GameObject leftbutton;
    public GameObject rightbutton;
    public GameObject AsobikataImage;
    public GameObject AsobikataImage2;
    public GameObject AsobikataText;
    public Sprite[] AsobikataSprite;
    public Sprite[] AsobikataSprite2;
    [TextArea(1, 10)] public string[] AsobikataSetsumei;

    int asobikatanumber;

    // Start is called before the first frame update
    void Start()
    {
        asobikatanumber = 0;
        
        SetAsobikataInfo(asobikatanumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRightButton()
    {
        SoundManager_SE.m_Instane.PlaySoundEfect(SelectaudioClip,0.2f);

        asobikatanumber++;

        leftbutton.GetComponent<Button>().interactable = true;

        SetAsobikataInfo(asobikatanumber);

    }

    public void OnLeftButton()
    {
        SoundManager_SE.m_Instane.PlaySoundEfect(SelectaudioClip,0.2f);

        asobikatanumber--;

        rightbutton.GetComponent<Button>().interactable = true;

        SetAsobikataInfo(asobikatanumber);

        
    }

    public void ToTitle()
    {
        SoundManager_SE.m_Instane.PlaySoundEfect(BackaudioClip,0.2f);

        AsobikataGroup.GetComponent<RectTransform>().DOAnchorPosY(900.0f, 0.5f, true);
    }

    void SetAsobikataInfo(int stagenumber)
    {
        AsobikataImage.GetComponent<Image>().sprite = AsobikataSprite[stagenumber];
        AsobikataImage2.GetComponent<Image>().sprite = AsobikataSprite2[stagenumber];
        
        
        AsobikataText.GetComponent<TextMeshProUGUI>().text = AsobikataSetsumei[stagenumber];

        if(stagenumber == 0)
        {
            leftbutton.GetComponent<Button>().interactable = false;
        }
        if(stagenumber == 2)
        {
            rightbutton.GetComponent<Button>().interactable = false;
        }
    }

    
}
