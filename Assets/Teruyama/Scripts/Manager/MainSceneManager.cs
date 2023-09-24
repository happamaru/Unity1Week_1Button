using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] UIManager uIManager;
    [SerializeField] PlayerController playerController;
    Animator animator;
    bool IsClear;

    void Start(){
        animator = GetComponent<Animator>();
    }
    IEnumerator Clear(){
        if(IsClear) yield break;
        IsClear = true;
        GameManager.resultScore = uIManager.Get_NowScore;
        GameManager.hp = uIManager.Get_Hp;
        GameManager.time = uIManager.Get_Time;
        animator.SetBool("IsPush",true);
        playerController.IsClear();
        yield return new WaitForSeconds(1);
        uIManager.ClearFade();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("ResultScene");
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            StartCoroutine(Clear());
        }
    }
}
