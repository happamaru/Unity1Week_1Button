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
     [SerializeField] int DebugID;
     [SerializeField] float scoredelay;
     [SerializeField] float finalscoreduration;
     [SerializeField] float TimeBonusNumber;
     [SerializeField] float HPBonusNumber;

     [SerializeField] AudioClip WadaikoaudioClip;
     [SerializeField] AudioClip DramrallaudioClip;
     [SerializeField] AudioClip HakushuaudioClip;
     [SerializeField] AudioClip BGMaudioClip;
     [SerializeField] AudioClip SelectaudioClip;

    int timebonus;
    int hpbonus;
    int finalscore;
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
            SoundManager_SE.m_Instane.PlaySoundEfect(WadaikoaudioClip,0.2f);
            TimeBonus.GetComponent<RectTransform>().DOAnchorPosY(-12.0f, 0.5f).SetDelay(scoredelay).OnComplete(() => {
                SoundManager_SE.m_Instane.PlaySoundEfect(WadaikoaudioClip,0.2f);
                HPBonus.GetComponent<RectTransform>().DOAnchorPosY(-12.0f, 0.5f).SetDelay(scoredelay).OnComplete(() => {
                    SoundManager_SE.m_Instane.PlaySoundEfect(WadaikoaudioClip,0.2f);

                    StartCoroutine("StartDramrall");

                    StartCoroutine("StartBGM");
                    // 指定したupdateNumberまでカウントアップ・カウントダウンする
                    finalscore = GameManager.resultScore + timebonus + hpbonus;
                    int nowNumber = 0;
                    int updateNumber = finalscore;
                    DOTween.To(() => nowNumber, (n) => nowNumber = n, updateNumber, finalscoreduration).SetDelay(1.0f)
                        .OnUpdate(() => FinalScore.text = nowNumber.ToString("#,0")).OnComplete(() => {
                            GRankingButton.SetActive(true);
                            GTitleButton.SetActive(true);
                        });
                });
            });
        });

        


        
    }
    public void ToTitle(){
        SoundManager_SE.m_Instane.PlaySoundEfect(SelectaudioClip,0.2f);
        SceneManager.LoadScene("TitleScene");
    }
    public void OpenRanking(){
        SoundManager_SE.m_Instane.PlaySoundEfect(SelectaudioClip,0.2f);
        if(IsDebug){
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(DebugScore,DebugID);    
        }else{
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking (finalscore,GameManager.PlayStageNum);
        }
    }

    //-----------------------------------------------------------------------------------------

    int CulcTimeBonus()
    {
        int bonus = (int)((float)GameManager.time / TimeBonusNumber * GameManager.resultScore);

        return bonus;
    }

    int CulcHPBonus()
    {
        int bonus = (int)((float)GameManager.hp / HPBonusNumber * GameManager.resultScore);

        return bonus;
    }

    IEnumerator StartDramrall()
    {
        yield return new WaitForSeconds(1.0f);

        SoundManager_SE.m_Instane.PlaySoundEfect(DramrallaudioClip,0.2f);
    }

    IEnumerator StartBGM()
    {
        yield return new WaitForSeconds(3.2f);

        SoundManager_SE.m_Instane.PlaySoundEfect(HakushuaudioClip,0.2f);
        SoundManager_BGM.m_Instane.PlayBackGroundMusic(BGMaudioClip,0.1f);
    }
}
