using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[Serializable]
public abstract class Enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Sequence _seq;
    protected Action OnVisible;
    protected Action OnDisable;
    [SerializeField] protected int damage;
    [SerializeField] protected int HP;
    [SerializeField] protected int score;
    
    private void Awake() 
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
     private void OnTriggerEnter2D(Collider2D other) {
        var Damage = other.GetComponent<IDamage>();
        if(Damage != null){
            HitBlink();
            HP -= Damage.AddDamage();
            if(HP <= 0){
                Destroy(this.gameObject);
            }
        }     
    }

     /// <summary> 点滅によるダメージ演出再生 </summary>
    private void HitBlink()
    {
        _seq?.Kill();
        _seq = DOTween.Sequence();
        _seq.AppendCallback(() => spriteRenderer.color = Vector4.zero);
        _seq.AppendInterval(0.05f);
        _seq.AppendCallback(() => spriteRenderer.color = Vector4.one);
        _seq.AppendInterval(0.05f);
        _seq.SetLoops(2);
        _seq.Play();
    }

        /// <summary>
    /// Rendererが任意のカメラから見えると呼び出される
    /// </summary>
    private void OnBecameVisible()
    {
        if(OnVisible != null){
           OnVisible.Invoke();
        }
    }
    
    
    /// <summary>
    /// Rendererがカメラから見えなくなると呼び出される
    /// </summary>
    private void OnBecameInvisible()
    {
        if(OnDisable != null){
            OnDisable.Invoke();
        }
    }
    

}
