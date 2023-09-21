using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime :Enemy,IEnemy
{
    Rigidbody2D SlimeRg2d;
    PlayerInformation playerInformation;
    [SerializeField] float speed;
 
    int dir;
    
    private void FixedUpdate() {
        SlimeRg2d.AddForce(dir * 30 * Vector2.left);   
    }
    private void Update() {
        if(!IsMove) return;
        if(playerInformation.IsPlayerLeft(this.transform.position.x)){
            dir = 1;
            transform.localScale = new Vector3(1,1,1);
        }else{
            dir = -1;
            transform.localScale = new Vector3(-1,1,1);
        }

        if(SlimeRg2d.velocity.x > speed){
            SlimeRg2d.velocity = new Vector2(speed,SlimeRg2d.velocity.y);
        }    
        
        if(SlimeRg2d.velocity.x < -speed){
            SlimeRg2d.velocity = new Vector2(-speed,SlimeRg2d.velocity.y);
        }    
    }

    public int AddDamage(){
        var rg2d = Character.GetComponent<Rigidbody2D>();
        rg2d.velocity = Vector2.zero;
        rg2d.AddForce(Vector2.left * this.transform.localScale.normalized * addForce);
        return damage;
    }

    void Start(){
        playerInformation = GameObject.Find("CharacterInformation").GetComponent<PlayerInformation>();
        SlimeRg2d = this.GetComponent<Rigidbody2D>();
        OnVisible = () =>{
            IsMove = true;
        };
    }

}
