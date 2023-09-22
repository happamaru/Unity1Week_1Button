using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInformation : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject DieEffect;
    [SerializeField] GameObject HitEffect;
    public GameObject HpBar;
    public Action<int> OnScoreChange;


   public IEnumerator SetDie(Vector2 pos){
        GameObject go = Instantiate(DieEffect);
        go.transform.position = pos;
        yield break;
    }
    public void SetHit(Vector2 pos){
        GameObject go = Instantiate(HitEffect);
        go.transform.position = pos;
    }

    public bool IsPlayerLeft(float x){
        if(player.transform.position.x < x){
            return true;
        }else{
            return false;
        }
    }

    public Vector3 GetPlayerPos(){
        return player.transform.position;
    }
}
