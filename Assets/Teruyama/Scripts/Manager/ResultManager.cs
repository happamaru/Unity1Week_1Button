using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ResultScore; 
    [SerializeField] ButtonManager buttonManager;
    void Start(){
        ResultScore.text = GameManager.resultScore.ToString();
        buttonManager.onClickCallback = ToTitle;
    }
    public void ToTitle(){
        SceneManager.LoadScene("TitleScene");
    }
}
