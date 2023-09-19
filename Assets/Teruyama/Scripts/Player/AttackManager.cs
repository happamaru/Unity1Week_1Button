using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public AttackDataBase attackDataBase;
    

    public enum AttackType{
        Slash,
        HyperSlash,
        Magic,
    }

    /// <summary>
    /// エフェクトの発生
    /// </summary>
    public GameObject SetEffect(int index){
        GameObject go = Instantiate(attackDataBase.attackDatas[index].prefab);
        go.GetComponent<AttackCollider>().Damage = attackDataBase.attackDatas[index].DamageNum;
        go.GetComponent<AttackCollider>().activeTime = attackDataBase.attackDatas[index].activeTime;
        return go;
    } 

    public void DestroyEffect(GameObject go){
        Destroy(go);
    }
}
