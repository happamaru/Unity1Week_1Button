using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;

public class TeamCompositionManager : MonoBehaviour
{
    public GameObject[] TeamImage;

    public GameObject CharaImageGroup;
    public GameObject TeamCompositionGauge;
    public UnityEngine.UI.Image CharaExplanationImage;
    public GameObject TeamComposeButton;
    public GameObject TeamMessage;
    public Canvas canvas;

    public int[] Heroes = new int[4];

    //チーム編成で画像を差し替える
    public void SetCharaImage(int num, Sprite sprite)
    {
        TeamImage[num].GetComponent<UnityEngine.UI.Image>().sprite = sprite;
    }

    //チーム編成を一つ戻す
    public void OnBackOneStep()
    {
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
        CharaImageGroup.transform.GetChild(Heroes[a - 1] - 1).GetComponent<UnityEngine.UI.Button>().interactable = true;   //ボタンリセット
        Heroes[a - 1] = 0;
        TeamImage[a - 1].GetComponent<UnityEngine.UI.Image>().sprite = null;
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
        //チーム編成、編成画面をリセット
        for(int i = 0; i < 4; i++)
        {
            Heroes[i] = 0;

            TeamImage[i].GetComponent<UnityEngine.UI.Image>().sprite = null;
        }

        //キャラ選択ボタンをリセット
        Transform children = CharaImageGroup.GetComponentInChildren<Transform>();
        foreach(Transform ob in children)
        {
            ob.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }

        //ゲージをリセット
        TeamCompositionGauge.GetComponent<UnityEngine.UI.Image>().DOFillAmount(0.0f, 0.1f);

        //決定ボタンを消す
        TeamComposeButton.SetActive(false);
    }

    //決定ボタンを押されたらチーム編成を更新する
    public void OnTeamCompose()
    {
        //チーム編成を更新
        for(int i = 0; i < 4; i++)
        {
            GameManager.team[i] = Heroes[i];
        }


        GameObject go = GameObject.Find("TeamMessage(Clone)");
        if(go != null)
            Destroy(go);
        

        GameObject newTextObject = Instantiate(TeamMessage, transform.position, Quaternion.identity);
        newTextObject.transform.SetParent(canvas.transform); 
        newTextObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        newTextObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(500, 500);
        newTextObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(500, 375), 0.5f).SetEase(Ease.OutBack).OnComplete(() => {
            newTextObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(500, 500), 0.5f).SetDelay(3f).SetEase(Ease.OutBack).OnComplete(() => {
                Destroy(newTextObject);
            });
        });
        
    }


    //staticのパーティー編成を編成画面のほうに反映させる
    public void SetTeamCompose()
    {
        for(int i = 0; i < 4; i++)
        {
            CharaImageGroup.transform.GetChild(GameManager.team[i] - 1).GetComponent<CharaImage>().OnClickCharaImage();
        }
    }
}
