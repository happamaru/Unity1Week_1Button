using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] UIManager uIManager;
    void Clear(){
        GameManager.resultScore = uIManager.Get_NowScore;
        SceneManager.LoadScene("ResultScene");
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            Clear();
        }
    }
}
