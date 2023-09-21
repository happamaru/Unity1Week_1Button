using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bat : Enemy,IEnemy
{
   
    [SerializeField] float speed;
    void Start(){
        OnVisible = () =>{
            transform.DOMoveY(GameObject.Find("Character").transform.position.y,1).SetEase(Ease.Linear).OnComplete(() => {
                IsMove = true;
                animator.SetBool("IsMove",true);
        });
        };
    }
    public int AddDamage(){
        Character.GetComponent<Rigidbody2D>().AddForce(Vector2.left * this.transform.localScale.normalized * addForce);
        return damage;
    }

    void Update(){
        if(IsMove){
            this.transform.AddPosX(speed*Time.deltaTime);
        }
    }
}
