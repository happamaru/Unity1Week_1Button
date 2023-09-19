using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharaImage : MonoBehaviour,
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public System.Action onClickCallback;

    Vector2 OriginalScale;

    [SerializeField] bool Type_MoveScene;
    bool isClick;

    public int CharaNumber;
    public TitleManager titlemanager;
    public TeamCompositionManager teamcompositionmanager;
    Button button;


    void Start()
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
        if(button.interactable == false) return;
        transform.DOScale(OriginalScale * 1.3f, 0.24f).SetUpdate(true).SetEase(Ease.OutCubic);
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
                teamcompositionmanager.SetCharaImage(i, transform.GetChild(0).gameObject.GetComponent<Image>().sprite);
                return;
            }
        }
        
            //returnしていないということは編成セットが満員
            Debug.Log("編成セットが満員です！");
    }

}
