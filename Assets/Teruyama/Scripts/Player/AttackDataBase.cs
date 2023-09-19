using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackDataBase : ScriptableObject
{
    public List<AttackData> attackDatas;
}

[System.Serializable]
public class AttackData{
    public string Name;
    public GameObject prefab;
    public int DamageNum;
    public float activeTime;
    //[SerializeReference, SubclassSelector,Tooltip("動き方")] public IMove moveType;
    public Vector3 InitPosition;
}