using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Image SkillGauge;

    void Start(){
        playerController.OnChangeGauge = ChangeGauge;
        playerController.OnResetGauge = ResetGauge; 
    }

    void ChangeGauge(float interval){
        SkillGauge.DOFillAmount(1,interval).SetEase(Ease.Linear);
    }

    void ResetGauge(){
        SkillGauge.fillAmount = 0;
    }
}
