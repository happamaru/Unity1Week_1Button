using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
   [SerializeField] GameObject player;
   [SerializeField] UIManager uIManager;
   void Update(){
    if(player.transform.position.y < -20){
        GameOver();
    }
   }

   public void GameOver(){
        GameManager.resultScore = uIManager.Get_NowScore;
        SceneManager.LoadScene("ResultScene");
   }
}
