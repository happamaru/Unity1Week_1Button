using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class Skull : MonoBehaviour, IScore
{
    public GameObject coineffect;
    public GameObject skulleffect;
    public int[] skullscore = new int[4];
    public float scoresize = 2.0f;

    public GameObject scoretext;




    public int AddScore()
    {
        int score;

        int scorerand = UnityEngine.Random.Range(0, 100);

        if(scorerand > 80)// 81 ~ 99で10000点
        {
            score = skullscore[0];
        }
        else if(scorerand > 40)//41 ~ 80で1000点
        {
            score = skullscore[1];
                
        }
        else if(scorerand > 10)//11～40で500点
        {
            score = skullscore[2];     
        }
        else//0～10で-5000点
            score = skullscore[3];
        
        if(scorerand > 10)
            Instantiate(coineffect, transform.position, Quaternion.identity);
        else
            Instantiate(skulleffect, transform.position, Quaternion.identity);

        DisplayScore(score);

        Destroy(gameObject);
            
        return score;
    }

    void DisplayScore(int score)
    {
        GameObject go = Instantiate(scoretext, transform.position, quaternion.identity);
        go.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.5f, 1.0f);
        go.GetComponent<RectTransform>().localScale = go.transform.GetComponent<RectTransform>().localScale * scoresize;
        if(score > 0)
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + score.ToString();
        else
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = score.ToString();

        go.transform.DOMoveY(transform.position.y + 0.5f, 0.5f, false).SetEase(Ease.OutBack).OnComplete(() => {
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOFade(0.0f, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                Destroy(go);
            });
        });
    }
}
