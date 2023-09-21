using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlashGhost : Enemy,IEnemy
{
    PlayerInformation Player;
    [SerializeField] private float GhostSpeed;     //幽霊の速度
     [SerializeField] private float ShakeInterval;     
     [SerializeField] float WaitInterval;
    private Rigidbody2D rb;                        //幽霊のRigidbody2D
    float scale;

    public int AddDamage(){
        var rg2d = Character.GetComponent<Rigidbody2D>();
        rg2d.velocity = Vector2.zero;
        rg2d.AddForce(Vector2.left * this.transform.localScale.normalized * addForce);
        StartCoroutine(StopInterval(0.5f));
        return damage;
    }

    void Update(){
        

            if(Player.IsPlayerLeft(this.transform.position.x)){
                transform.localScale = new Vector3(scale,scale,scale);
            }else{
                transform.localScale = new Vector3(-scale,scale,scale);
            }
            if(IsMove){
            delta += Time.deltaTime;
        if(delta > GhostSpeed + ShakeInterval + WaitInterval){
            delta = 0;
            StartCoroutine(ChaseAttack());
        }
            }
        }


    void Start(){
        delta = GhostSpeed + ShakeInterval;
        Player = GameObject.Find("CharacterInformation").GetComponent<PlayerInformation>();
        rb = GetComponent<Rigidbody2D>();
        scale = this.transform.localScale.x;
        OnVisible = () =>{
            StartCoroutine(ChaseAttack());
        };
        OnDisable = () =>{
            Destroy(this.gameObject);
        };
    }
    float delta;


    IEnumerator ChaseAttack(){
        transform.DOShakePosition(duration:ShakeInterval,strength:0.2f,fadeOut:false);
        yield return new WaitForSeconds(ShakeInterval);
        animator.SetBool("IsMove",true);
        transform.DOMove(Player.GetPlayerPos(),GhostSpeed).SetEase(Ease.Linear);
        yield return new WaitForSeconds(GhostSpeed);
        animator.SetBool("IsMove",false);
        IsMove = true;
    }


}
