using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
   [SerializeField] GameObject player;
   [SerializeField] UIManager uIManager;
   bool IsGameOver;
   void Update(){
    if(player.transform.position.y < -20){
        StartCoroutine(GameOver());
    }
    if(uIManager.Get_Time < 1){
        StartCoroutine(GameOver());
    }
   }

   public IEnumerator GameOver(){
        if(IsGameOver) yield break;
        IsGameOver = true;
        GameManager.resultScore = uIManager.Get_NowScore;
        GameManager.hp = 0;
        GameManager.time = uIManager.Get_Time;
        player.GetComponent<PlayerController>().IsGameOver();
        yield return new WaitForSeconds(1f);
        uIManager.FadeOut();
        yield return new WaitForSeconds(2.2f);
        SceneManager.LoadScene("ResultScene");
   }
}
