using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ResultScore; 
    [SerializeField] ButtonManager buttonManager;
     [SerializeField] ButtonManager RankingButton;
     
     [SerializeField] bool IsDebug;
     [SerializeField] int DebugScore;
    void Start(){
        ResultScore.text = GameManager.resultScore.ToString();
        buttonManager.onClickCallback = ToTitle;
        RankingButton.onClickCallback = OpenRanking;
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
}
