using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    bool IsMove;
    [SerializeField] float speed;
    void Start(){
        OnVisible = () =>{
            IsMove = true;
        };
        OnDisable = () =>{
            Destroy(this.gameObject);
        };
    }

    void Update(){
        if(IsMove){
            this.transform.AddPosX(speed*Time.deltaTime);
        }
    }
}
