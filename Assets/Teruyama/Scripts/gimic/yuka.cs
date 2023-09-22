using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yuka : MonoBehaviour
{
    //PlayerInformation playerInformation;
    void Start(){
     //   playerInformation = GameObject.Find("CharacterInformation").GetComponent<PlayerInformation>();
    }
    void Update(){
    }
    IEnumerator a(){
        yield return new WaitForSeconds(1f);
        this.gameObject.tag = "ground";
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            StartCoroutine(a());
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            this.gameObject.tag = "Untagged";
        }
    }
}
