using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] UIManager uIManager;
    Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }
    IEnumerator Clear(){
        animator.SetBool("IsPush",true);
        yield return new WaitForSeconds(2);
        GameManager.resultScore = uIManager.Get_NowScore;
        SceneManager.LoadScene("ResultScene");
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            StartCoroutine(Clear());
        }
    }
}
