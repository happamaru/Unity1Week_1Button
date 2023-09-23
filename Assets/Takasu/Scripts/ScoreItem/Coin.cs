using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class Coin : MonoBehaviour,IScore
{
    public int coinscore;
    public float scoresize = 2.0f;

    public GameObject coineffect;

    public GameObject scoretext;


    public int AddScore()
    {
        Instantiate(coineffect, transform.position, Quaternion.identity);
        SoundManager_SE.m_Instane.PlaySoundEfect(GameObject.Find("CharacterInformation").GetComponent<PlayerInformation>().coin,0.2f);
        DisplayScore(coinscore);

        //ゲームオブジェクトを削除
        Destroy(gameObject);

        return coinscore;
    }

    void DisplayScore(int score)
    {
        GameObject go = Instantiate(scoretext, transform.position, quaternion.identity);
        go.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.5f, 1.0f);
        go.GetComponent<RectTransform>().localScale = go.transform.GetComponent<RectTransform>().localScale * scoresize;
        go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + score.ToString();

        go.transform.DOMoveY(transform.position.y + 0.5f, 0.5f, false).SetEase(Ease.OutBack).OnComplete(() => {
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOFade(0.0f, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                Destroy(go);
            });
        });
    }
}
