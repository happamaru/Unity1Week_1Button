using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Image SkillGauge;
    [SerializeField] Image HpGauge;
    const int MaxHp = 100; 
    int NowHp;

    void Start(){
        playerController.OnChangeGauge = ChangeGauge;
        playerController.OnResetGauge = ResetGauge;
        playerController.OnHpChange = ChangeHp; 
        NowHp = MaxHp;
    }

    void ChangeHp(int num){
        NowHp -= num;
        float Num = (float)NowHp;
        HpGauge.DOFillAmount(Num/(float)MaxHp,0.5f);
    }

    void ChangeGauge(float interval){
        SkillGauge.DOFillAmount(1,interval).SetEase(Ease.Linear);
    }

    void ResetGauge(){
        SkillGauge.fillAmount = 0;
    }
}
