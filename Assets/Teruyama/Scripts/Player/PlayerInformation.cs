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

    public AudioClip damage;
    public AudioClip coin;
    public AudioClip enemyDie;
    public AudioClip hit;
    public AudioClip nockBack;
    public AudioClip slash;
    public AudioClip magic;
    public AudioClip highJump;
    public AudioClip highSpeed;
    public AudioClip HyperSlash;


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
