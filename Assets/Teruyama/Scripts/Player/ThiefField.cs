using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThiefField : MonoBehaviour
{
    PlayerInformation playerInformation;
    
    // [SerializeField,ReadOnly,Tooltip("敵に与えるダメージ数")] public IMove moveType;

    IEnumerator Start(){
       playerInformation = GameObject.Find("CharacterInformation").GetComponent<PlayerInformation>();
      //  StartCoroutine(moveType.MoveAction(this.gameObject));
       yield return new WaitForSeconds(0.2f);
       Destroy(this.gameObject);

    }
    void OnTriggerEnter2D(Collider2D other){
        var coin = other.GetComponent<IScore>();
        if(coin != null){
            other.gameObject.transform.DOMove(playerInformation.GetPlayerPos(),0.2f);
        }
    }
}
