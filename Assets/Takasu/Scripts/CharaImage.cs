using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.U2D.Animation;
using TMPro;

public class CharaImage : MonoBehaviour,
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField] AudioClip CharaSelectaudioClip;

    public System.Action onClickCallback;

    Vector2 OriginalScale;

    [SerializeField] bool Type_MoveScene;
    bool isClick;

    public int CharaNumber;
    public TitleManager titlemanager;
    public TeamCompositionManager teamcompositionmanager;
    Button button;

    public TextMeshProUGUI charanametext;
    public TextMeshProUGUI charaexplainetext;

    public string[] charanames;
    [TextArea(1, 4)] public string[] charaexplaintext;



    void Awake()
    {
        OriginalScale = transform.localScale;

        button = GetComponent<Button>();
    }

    void OnEnable()
    {
        isClick = false;
    }

    

    public void OnClickCharaImage()
    {
        Debug.Log(CharaNumber);


        SoundManager_SE.m_Instane.PlaySoundEfect(CharaSelectaudioClip,0.2f);

        //titlemanagerのheroes配列にクリックしたボタンのnumberをセットする
        SetCharaNumber(CharaNumber);

    }

    public void SetCharaImage()
    {
        Debug.Log(CharaNumber);

        //titlemanagerのheroes配列にクリックしたボタンのnumberをセットする
        SetCharaNumber(CharaNumber);
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(button.interactable == false) return;
        if (isClick) return;
        //SoundManager.soundManager.ButtonsoundPlay(0, 1);
        if (Type_MoveScene)
        {
            isClick = true;
        }
        onClickCallback?.Invoke();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        teamcompositionmanager.CharaExplanationObject.transform.GetChild(0).GetComponent<SpriteLibrary>().spriteLibraryAsset = transform.GetChild(0).GetChild(0).GetComponent<SpriteLibrary>().spriteLibraryAsset;
        SetCharaName();
        SetCharaImageExplain();

        if(button.interactable == false) return;
        transform.DOScale(OriginalScale * 1.3f, 0.24f).SetUpdate(true).SetEase(Ease.OutCubic);

        //キャラの名前と説明を入れる

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(button.interactable == false) return;
        transform.DOScale(OriginalScale, 0.24f).SetUpdate(true).SetEase(Ease.OutCubic);
    }

    void SetCharaNumber(int num)
    {
        int[] heroes = new int[4];
        for(int i = 0; i < 4; i++)
        {
            heroes[i] = teamcompositionmanager.Heroes[i];
        }

        //もし編成セットに空きがあったら、左詰めにセットする
        for(int i = 0; i < 4; i++)
        {
            if(heroes[i] == 0)
            {
                teamcompositionmanager.Heroes[i] = CharaNumber;
                teamcompositionmanager.TeamCompositionGauge.GetComponent<Image>().DOFillAmount(0.25f * (i + 1), 0.1f);
                button.interactable = false;

                //クリックしたボタンの拡大を直す
                transform.DOScale(OriginalScale, 0.24f).SetUpdate(true).SetEase(Ease.OutCubic);

                //編成画面のスプライトを変更する
                teamcompositionmanager.SetCharaAnim(i, transform.GetChild(0));

                //４人編成できたら決定ボタンが出てくる
                if(i == 3)
                {
                    teamcompositionmanager.TeamComposeButton.SetActive(true);
                }

                return;
            }
        }
        
            //returnしていないということは編成セットが満員
            Debug.Log("編成セットが満員です！");
    }

    void SetCharaName()
    {
        charanametext.GetComponent<TextMeshProUGUI>().text = charanames[CharaNumber - 1];
    }

    void SetCharaImageExplain()
    {
        charaexplainetext.GetComponent<TextMeshProUGUI>().text = charaexplaintext[CharaNumber - 1];
    }


    void SetNameAndExplaine()
    {
        
        switch(CharaNumber)
        {
            case 1:
            {
                

                break;
            }
            case 2:
            {
                break;
            }
            case 3:
            {
                break;
            }
            case 4:
            {
                break;
            }
            case 5:
            {
                break;
            }
            case 6:
            {
                break;
            }
            case 7:
            {
                break;
            }
            case 8:
            {
                break;
            }
            case 9:
            {
                break;
            }
            case 10:
            {
                break;
            }
            case 11:
            {
                break;
            }
            case 12:
            {
                break;
            }
        }

    }

}
