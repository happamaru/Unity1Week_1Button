using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;
using UnityEngine.U2D.Animation;
using Assets.PixelHeroes.Scripts.CharacterScripts;
using AnimationState = Assets.PixelHeroes.Scripts.CharacterScripts.AnimationState;
using UnityEngine.Experimental.AI;

public class TeamCompositionManager : MonoBehaviour
{
    [SerializeField] AudioClip TeamComposeaudioClip;
    [SerializeField] AudioClip OneBackaudioClip;
    [SerializeField] AudioClip ResetaudioClip;
    [SerializeField] AudioClip BackaudioClip;


    public GameObject[] TeamAnimObject;

    public GameObject CharaImageGroup;
    public GameObject TeamImageGroup;
    public GameObject TeamCompositionGauge;
    public GameObject CharaExplanationObject;
    public GameObject TeamComposeButton;
    public GameObject TeamMessage;
    public Canvas canvas;

    public GameObject test;

    public int[] Heroes = new int[4];



    //チーム編成でアニメーションを切り替える
    public void SetCharaAnim(int num, Transform go)
    {
        TeamAnimObject[num].transform.GetChild(0).GetComponent<SpriteLibrary>().spriteLibraryAsset = go.transform.GetChild(0).GetComponent<SpriteLibrary>().spriteLibraryAsset;
        TeamAnimObject[num].transform.localScale = new Vector3(60, 60, 60);
    }

    //チーム編成を一つ戻す
    public void OnBackOneStep()
    {
        SoundManager_SE.m_Instane.PlaySoundEfect(BackaudioClip,0.2f);

        int a = 4;

        for(int i = 0; i < 4; i++)
        {
            if(Heroes[i] == 0)
            {
                a = i;
                break;
            }
        }

        //もしチームが一人も選ばれていなかったら何もしない
        if(a == 0) return;

        //一つ分チーム選択をリセットする
        CharaImageGroup.transform.GetChild(Heroes[a - 1]).GetComponent<UnityEngine.UI.Button>().interactable = true;   //ボタンリセット
        Heroes[a - 1] = 0;

        //チーム編成スロット上の表示を消す
        TeamAnimObject[a - 1].transform.localScale = Vector3.zero;

        TeamCompositionGauge.GetComponent<UnityEngine.UI.Image>().DOFillAmount(0.25f * (a - 1), 0.1f);

        if(a == 4)
        {
            //決定ボタンを消す
            TeamComposeButton.SetActive(false);
        }
        
    }

    //チーム編成をリセットする
    public void OnTeamReset()
    {
        SoundManager_SE.m_Instane.PlaySoundEfect(BackaudioClip,0.2f);

        //チーム編成、編成画面をリセット
        for(int i = 0; i < 4; i++)
        {
            Heroes[i] = 0;

            TeamAnimObject[i].transform.localScale = Vector3.zero;
        }

        //キャラ選択ボタンをリセット
        /*
        Transform children = CharaImageGroup.GetComponentInChildren<Transform>();
        foreach(Transform ob in children)
        {
            ob.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        */
        for(int i = 0; i < 12; i++)
        {
            CharaImageGroup.transform.GetChild(i + 1).GetComponent<UnityEngine.UI.Button>().interactable = true;
        }

        //ゲージをリセット
        TeamCompositionGauge.GetComponent<UnityEngine.UI.Image>().DOFillAmount(0.0f, 0.1f);

        //決定ボタンを消す
        TeamComposeButton.SetActive(false);
    }

    //決定ボタンを押されたらチーム編成を更新する
    public void OnTeamCompose()
    {
        SoundManager_SE.m_Instane.PlaySoundEfect(TeamComposeaudioClip,0.2f);

        //チーム編成を更新
        for(int i = 0; i < 4; i++)
        {
            GameManager.team[i] = Heroes[i] - 1;
        }


        GameObject go = GameObject.Find("TeamMessageBG(Clone)");
        if(go != null)
            Destroy(go);
        

            GameObject newTextObject = Instantiate(TeamMessage, transform.position, Quaternion.identity);
            newTextObject.transform.SetParent(canvas.transform); 
            newTextObject.transform.localScale = new Vector3(3.9f, 0.85f, 1.0f);
            newTextObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(580, 500);
            newTextObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(580, 375), 0.5f).SetEase(Ease.OutBack).OnComplete(() => {
                newTextObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(580, 500), 0.5f).SetDelay(3f).SetEase(Ease.OutBack).OnComplete(() => {
                Destroy(newTextObject);
            });
        });
        
    }


    //staticのパーティー編成を編成画面のほうに反映させる
    public void SetTeamCompose()
    {
        for(int i = 0; i < 4; i++)
        {
            CharaImageGroup.transform.GetChild(GameManager.team[i] + 1).GetComponent<CharaImage>().SetCharaImage();
        }
    }
}
