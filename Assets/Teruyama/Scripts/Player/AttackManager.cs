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

    public IEnumerator BigMagic(Vector2 pos,int num,float scaleX){
        float x,y;
        for(int i=0;i < num;i++){
            int index = Random.Range(3,6);
            if(scaleX > 0){
            x = Random.Range(pos.x+3,pos.x+9);
            y = Random.Range(pos.y-1,pos.y+3);
            }else{
            x = Random.Range(pos.x-9,pos.x-3);
            y = Random.Range(pos.y-1,pos.y+3);     
            }
            GameObject go = Instantiate(attackDataBase.attackDatas[index].prefab);
            go.GetComponent<AttackCollider>().Damage = attackDataBase.attackDatas[index].DamageNum;
            go.GetComponent<AttackCollider>().activeTime = attackDataBase.attackDatas[index].activeTime;;
            go.transform.position = new Vector3(x,y,0);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void DestroyEffect(GameObject go){
        Destroy(go);
    }
}
