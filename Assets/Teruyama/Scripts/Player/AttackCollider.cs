using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour,IDamage
{
    [SerializeField,ReadOnly,Tooltip("敵に与えるダメージ数")] int damage;
    [SerializeField,ReadOnly] public float activeTime;
    
    // [SerializeField,ReadOnly,Tooltip("敵に与えるダメージ数")] public IMove moveType;

    IEnumerator Start(){
      //  StartCoroutine(moveType.MoveAction(this.gameObject));
       yield return new WaitForSeconds(activeTime);
       Destroy(this.gameObject);
    }

    public int Damage{
        get{return damage;}
        set{damage = value;}
    }

    /// <summary>
    /// 敵にダメージを与える
    /// </summary>
    /// <param name="b"></param>
    public int AddDamage(){
        return damage;
    }

}


