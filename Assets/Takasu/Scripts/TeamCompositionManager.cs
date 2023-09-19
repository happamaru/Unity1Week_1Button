using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TeamCompositionManager : MonoBehaviour
{
    public GameObject[] TeamImage;

    public GameObject CharaImageGroup;

    public GameObject TeamCompositionGauge;

    public int[] Heroes = new int[4];

    //チーム編成で画像を差し替える
    public void SetCharaImage(int num, Sprite sprite)
    {
        TeamImage[num].GetComponent<Image>().sprite = sprite;
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
        CharaImageGroup.transform.GetChild(Heroes[a - 1] - 1).GetComponent<Button>().interactable = true;   //ボタンリセット
        Heroes[a - 1] = 0;
        TeamImage[a - 1].GetComponent<Image>().sprite = null;
        TeamCompositionGauge.GetComponent<Image>().DOFillAmount(0.25f * (a - 1), 0.1f);
    }

    //チーム編成をリセットする
    public void OnTeamReset()
    {
        //チーム編成、編成画面をリセット
        for(int i = 0; i < 4; i++)
        {
            Heroes[i] = 0;

            TeamImage[i].GetComponent<Image>().sprite = null;
        }

        //キャラ選択ボタンをリセット
        Transform children = CharaImageGroup.GetComponentInChildren<Transform>();
        foreach(Transform ob in children)
        {
            ob.GetComponent<Button>().interactable = true;
        }

        //ゲージをリセット
        TeamCompositionGauge.GetComponent<Image>().DOFillAmount(0.0f, 0.1f);
        
    }
}
