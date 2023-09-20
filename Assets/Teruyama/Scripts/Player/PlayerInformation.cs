using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    [SerializeField] GameObject player;

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
