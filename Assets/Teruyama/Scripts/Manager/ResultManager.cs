using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using DG.Tweening;

public class ResultManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ResultScore; 
    [SerializeField] TextMeshProUGUI TimeBonus;
    [SerializeField] TextMeshProUGUI HPBonus;
    [SerializeField] TextMeshProUGUI FinalScore;
    [SerializeField] ButtonManager buttonManager;
     [SerializeField] ButtonManager RankingButton;
     [SerializeField] GameObject GRankingButton;
     [SerializeField] GameObject GTitleButton;
     
     [SerializeField] bool IsDebug;
     [SerializeField] int DebugScore;
     [SerializeField] float scoredelay;
     [SerializeField] float finalscoreduration;

    int timebonus;
    int hpbonus;
    void Start(){
        if(IsDebug)
        {
            GameManager.resultScore = 50000;
            GameManager.time = 100;
            GameManager.hp = 80;
        }

        buttonManager.onClickCallback = ToTitle;
        RankingButton.onClickCallback = OpenRanking;

        GRankingButton.SetActive(false);
        GTitleButton.SetActive(false);

        ResultScore.text = GameManager.resultScore.ToString();

        //タイムボーナスを計算する処理を追加して
        timebonus = CulcTimeBonus();
        TimeBonus.text = timebonus.ToString();

        //HPボーナスを計算する処理を追加して
        hpbonus = CulcHPBonus();
        HPBonus.text = hpbonus.ToString();

        //スコアをひとつづつ表示する
        ResultScore.GetComponent<RectTransform>().DOAnchorPosY(-12.0f, 0.5f).SetDelay(scoredelay).OnComplete(() => {
            TimeBonus.GetComponent<RectTransform>().DOAnchorPosY(-12.0f, 0.5f).SetDelay(scoredelay).OnComplete(() => {
                HPBonus.GetComponent<RectTransform>().DOAnchorPosY(-12.0f, 0.5f).SetDelay(scoredelay).OnComplete(() => {
                    // 指定したupdateNumberまでカウントアップ・カウントダウンする
                    int nowNumber = 0;
                    int updateNumber = GameManager.resultScore + timebonus + hpbonus;
                    DOTween.To(() => nowNumber, (n) => nowNumber = n, updateNumber, finalscoreduration)
                        .OnUpdate(() => FinalScore.text = nowNumber.ToString("#,0")).OnComplete(() => {
                            GRankingButton.SetActive(true);
                            GTitleButton.SetActive(true);
                        });
                });
            });
        });

        


        
    }
    public void ToTitle(){
        SceneManager.LoadScene("TitleScene");
    }
    public void OpenRanking(){
        if(IsDebug){
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(DebugScore);    
        }else{
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking (GameManager.resultScore);
        }
    }

    //-----------------------------------------------------------------------------------------

    int CulcTimeBonus()
    {
        int bonus = (int)((float)GameManager.time / 300.0f * GameManager.resultScore);

        return bonus;
    }

    int CulcHPBonus()
    {
        int bonus = (int)((float)GameManager.hp / 100.0f * GameManager.resultScore);

        return bonus;
    }
}
