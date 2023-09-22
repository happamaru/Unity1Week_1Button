using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerInformation playerInformation;
    [SerializeField] Image SkillGauge;
    [SerializeField] Image HpGauge;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameOverManager gameOverManager;
    const int MaxHp = 100; 
    int NowHp;
    [SerializeField,ReadOnly] int NowScore;
    float delta;
    public int Get_NowScore{
        get{return NowScore;}
    }
    [SerializeField] Vector3 HpBarPos;
    GameObject Hp;
    GameObject ParentHp;
    void Start(){
        ParentHp = Instantiate(playerInformation.HpBar);
        Hp = ParentHp.transform.GetChild(0).gameObject;
        ParentHp.transform.position = playerInformation.GetPlayerPos() + HpBarPos;
        playerInformation.OnScoreChange = ChangeScore;
        playerController.OnChangeGauge = ChangeGauge;
        playerController.OnResetGauge = ResetGauge;
        playerController.OnHpChange = ChangeHp; 
        playerController.OnScoreChange = ChangeScore;
        NowHp = MaxHp;
    }

    void Update(){
         ParentHp.transform.position = playerInformation.GetPlayerPos() + HpBarPos;
       /* delta += Time.deltaTime;
        if(delta > 1.0f){
            delta = 0;
            ChangeScore(1);
        }
        */
    }

    void ChangeHp(int num){
        NowHp -= num;
        if(NowHp <= 0){
            gameOverManager.GameOver();
        }
        float Num = (float)NowHp;
        //HpGauge.DOFillAmount(Num/(float)MaxHp,0.5f);
        Hp.transform.localScale = new Vector3(Num/MaxHp,1,1);
    }

    void ChangeGauge(float interval){
        SkillGauge.DOFillAmount(1,interval).SetEase(Ease.Linear);
    }

    void ResetGauge(){
        SkillGauge.fillAmount = 0;
    }

    void ChangeScore(int score){
        NowScore += score;
        scoreText.text = NowScore.ToString();
    }
}
