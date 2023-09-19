using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour,IDamage
{
    [SerializeField] public List<ColliderData> colliderData;

    [System.NonSerialized] public int NowIndex;

    public void ActiveCollider(bool b){
        this.gameObject.SetActive(b);
    }

    public int AddDamage(){
        return colliderData[NowIndex].DamageNum;
    }
}

[System.Serializable]
public class ColliderData{
    public string Name;
    public int DamageNum;
    public Vector2 Scale;
}
