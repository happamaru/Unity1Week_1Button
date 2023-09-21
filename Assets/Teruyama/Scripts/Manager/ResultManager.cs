using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ResultScore; 
    void Start(){
        ResultScore.text = GameManager.resultScore.ToString();
    }
    public void ToTitle(){
        SceneManager.LoadScene("TitleScene");
    }
}
