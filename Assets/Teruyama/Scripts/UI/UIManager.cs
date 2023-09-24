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
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameOverManager gameOverManager;
    const int MaxHp = 100; 
    int NowHp;
    public int Get_Hp{
        get{return NowHp;}
    }
    const int MaxTime = 300;
    int time = MaxTime;
    public int Get_Time{
        get{return time;}
    }
    [SerializeField,ReadOnly] int NowScore;
    float delta;
    public int Get_NowScore{
        get{return NowScore;}
        set{NowScore = value;}
    }
    [SerializeField] Vector3 HpBarPos;
    GameObject Hp;
    GameObject ParentHp;
    const int PartyNum = 4;
    int[] slotNums = new int[PartyNum];
    [SerializeField] ChracterDataBase chracterDataBase;
    [SerializeField] Transform SelectButton;
    [SerializeField] SpriteRenderer[] slots;
    float[] SelectPosx = {245,354,465,570};
    [SerializeField] AudioClip[] MainBGMs;
   public enum BGMType{
        main1,
        main2,
        main3
    }
    [SerializeField] BGMType bGMType;
    [SerializeField] Image fadeImage;
    [SerializeField] Image ClearImage;
    void Start(){
        SoundManager_BGM.m_Instane.PlayBackGroundMusic(MainBGMs[(int)bGMType],0.3f);
        ParentHp = Instantiate(playerInformation.HpBar);
        Hp = ParentHp.transform.GetChild(0).gameObject;
        ParentHp.transform.position = playerInformation.GetPlayerPos() + HpBarPos;
        playerInformation.OnScoreChange = ChangeScore;
        playerController.OnChangeGauge = ChangeGauge;
        playerController.OnResetGauge = ResetGauge;
        playerController.OnHpChange = ChangeHp; 
        playerController.OnChangeSlot = OnChangeSlot;
        playerController.OnScoreChange = ChangeScore;
        NowHp = MaxHp;
        slotNums = GameManager.team;
        for(int i=0;i<PartyNum;i++){
            slots[i].color = Vector4.one;
            slots[i].sprite = chracterDataBase.charaDatas[slotNums[i]].sprite;
        }  
        }

    void Update(){
         ParentHp.transform.position = playerInformation.GetPlayerPos() + HpBarPos;

        delta += Time.deltaTime;
        if(delta > 1.0f){
            delta = 0;
            time -= 1;
            if(time < 1){
                time = 0;
            }
            TimerText.text = time.ToString();
        }
    }

    void OnChangeSlot(int index){
        if(index >= PartyNum) return;
        SelectButton.transform.localPosition = new Vector3(SelectPosx[index],SelectButton.transform.localPosition.y,SelectButton.transform.localPosition.z);
    } 
    public void FadeOut(){
        fadeImage.DOFade(1,1.5f).SetEase(Ease.Linear);
    }
    public void ClearFade(){
        ClearImage.DOFade(1,1.5f).SetEase(Ease.Linear);
    }

    void ChangeHp(int num){
        NowHp -= num;
        if(NowHp <= 0){
            NowHp = 0;
           StartCoroutine(gameOverManager.GameOver());
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
