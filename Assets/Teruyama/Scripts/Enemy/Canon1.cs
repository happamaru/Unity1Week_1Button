using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon1 : MonoBehaviour,IEnemy
{
    [SerializeField] float activeTime;
    [SerializeField] int damage;
    GameObject player;
    IEnumerator Start(){
        yield return new WaitForSeconds(activeTime);
        Destroy(this.gameObject);
    }

    public int AddDamage(){
        Destroy(this.gameObject);
        return damage;
    }
}
