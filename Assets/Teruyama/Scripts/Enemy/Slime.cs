using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime :Enemy,IEnemy
{
    Rigidbody2D SlimeRg2d;
    [SerializeField] float speed;
     private void FixedUpdate() {
        SlimeRg2d.AddForce(speed * Vector2.left);   
    }

    public int AddDamage(){
        var rg2d = Character.GetComponent<Rigidbody2D>();
        rg2d.velocity = Vector2.zero;
        rg2d.AddForce(Vector2.left * this.transform.localScale.normalized * addForce);
        return damage;
    }

    void Start(){
        SlimeRg2d = this.GetComponent<Rigidbody2D>();
        OnVisible = () =>{
            IsMove = true;
        };
    }

}
