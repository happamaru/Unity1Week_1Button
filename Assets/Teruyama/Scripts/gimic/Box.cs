using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Animator animator;
    [SerializeField] int HP;
    void Start(){
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        var Damage = other.gameObject.GetComponent<IDamage>();
        if(Damage != null){
            HP -= Damage.AddDamage();
            StartCoroutine(HitAnimation());
            if(HP <= 0){  
                StartCoroutine(DestroyAnimation());
            }
        }     
    }
    IEnumerator DestroyAnimation(){
        animator.SetBool("IsDestroy",true);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    IEnumerator HitAnimation(){
        animator.SetBool("IsHit",true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("IsHit",false);
    }

}
