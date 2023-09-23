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
    [SerializeField] Vector3 HpPos;
     protected bool IsMove;
     bool VisibleOnce;
     bool DisableOnce;
    GameObject ParentHp;
    GameObject hp;
    float hpNum;
    private void Awake() 
    {
        hpNum = (float)HP;
        playerInformation = GameObject.Find("CharacterInformation").GetComponent<PlayerInformation>();
        ParentHp = Instantiate(playerInformation.HpBar);
        hp = ParentHp.transform.GetChild(0).gameObject;
        ParentHp.transform.position = this.transform.position + HpPos;

        spriteRenderer = this.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Character = GameObject.Find("Character");
    }
     private void OnTriggerEnter2D(Collider2D other) {
        var Damage = other.gameObject.GetComponent<IDamage>();
        if(Damage != null){
            HitBlink();
            SoundManager_SE.m_Instane.PlaySoundEfect(playerInformation.hit,0.1f);
            HP -= Damage.AddDamage();
            hp.transform.localScale = new Vector3((float)HP/hpNum,1,1);
            playerInformation.SetHit(this.transform.position);
            if(HP <= 0){
                this.transform.DOKill();
                StartCoroutine(playerInformation.SetDie(this.transform.position));
                playerInformation.OnScoreChange(score);  
                SoundManager_SE.m_Instane.PlaySoundEfect(playerInformation.enemyDie,0.1f);
                Destroy(this.gameObject);
            }
        }     
    }

     private void OnDestroy() {
        Destroy(ParentHp);    
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
        if(ParentHp != null){
            ParentHp.transform.position = this.transform.position + HpPos;
        }

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
