using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[Serializable]
public abstract class Enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected GameObject Character;
    protected PlayerInformation playerInformation;
    Sequence _seq;
    protected Action OnVisible;
    protected Action OnDisable;
    [SerializeField] protected int damage;
    [SerializeField] protected int HP;
    [SerializeField] protected float MovePosX;
    [SerializeField] protected int score;
    [SerializeField] protected float addForce;
     protected bool IsMove;
     bool VisibleOnce;
     bool DisableOnce;
    
    private void Awake() 
    {
        playerInformation = GameObject.Find("CharacterInformation").GetComponent<PlayerInformation>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Character = GameObject.Find("Character");
    }
     private void OnTriggerEnter2D(Collider2D other) {
        var Damage = other.gameObject.GetComponent<IDamage>();
        if(Damage != null){
            HitBlink();
            HP -= Damage.AddDamage();
            playerInformation.SetHit(this.transform.position);
            if(HP <= 0){
                this.transform.DOKill();
                StartCoroutine(playerInformation.SetDie(this.transform.position));
                Destroy(this.gameObject);
            }
        }     
    }
    protected IEnumerator StopInterval(float time){
        IsMove = false;
        yield return new WaitForSeconds(time);
        IsMove = true;
    }

     /// <summary> 点滅によるダメージ演出再生 </summary>
    private void HitBlink()
    {
        if(spriteRenderer == null) return;
        _seq?.Kill();
        _seq = DOTween.Sequence();
        _seq.AppendCallback(() => spriteRenderer.color = Vector4.zero);
        _seq.AppendInterval(0.05f);
        _seq.AppendCallback(() => spriteRenderer.color = Vector4.one);
        _seq.AppendInterval(0.05f);
        _seq.SetLoops(2);
        _seq.Play();
    }

     private void LateUpdate() {
        if(!VisibleOnce){
        if(IsMoveCheck(MovePosX)){
            VisibleOnce = true;
            if(OnVisible != null){
                OnVisible?.Invoke();
            }
        }
        }
        if(VisibleOnce){
            if(!DisableOnce){
                if(IsEnemyOut()){
                    if(OnDisable != null){
                       OnDisable?.Invoke();
                    }       
                }
            }
        }
    }

    /// <summary>
    /// Rendererが任意のカメラから見えると呼び出される
    /// </summary>
    /*private void OnBecameVisible()
    {
        if(OnVisible != null){
           OnVisible.Invoke();
        }
    }
    */
    /// <summary>
    /// Rendererがカメラから見えなくなると呼び出される
    /// </summary>
   /* private void OnBecameInvisible()
    {
        if(OnDisable != null){
            OnDisable.Invoke();
        }
    }
    */
    
    
     bool IsMoveCheck(float x){
        float posX = this.transform.position.x - Character.transform.position.x;
        if(posX < x){
            return true;
        }else{
            return false;
        }
    }

    bool IsEnemyOut(){
        float posX = Mathf.Abs(this.transform.position.x - Character.transform.position.x);
        if(posX > 15){
            return true;
        }else{
            return false;
        }
    }

   
}
