using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HighJump:ICommand{
    public IEnumerator Command(PlayerController player){
      var rg2d = player.rg2d;
      rg2d.velocity = new Vector2(rg2d.velocity.x,0);
      rg2d.AddForce(Vector2.up * 800);
      yield return new WaitForSeconds(0.3f);
    }
}
public class HighSpeed:ICommand{

    public IEnumerator Command(PlayerController player){
      player.MaxSpeed = 20;
      var rg2d = player.rg2d;
      rg2d.velocity = Vector2.zero;
      rg2d.gravityScale = 0;
      rg2d.AddForce(new Vector2(player.gameObject.transform.localScale.x,0) * 1000);
      yield return new WaitForSeconds(0.3f);
    }
}
public class Slash:ICommand{
  public IEnumerator Command(PlayerController player){
    GameObject go = player.attackManager.SetEffect((int)AttackManager.AttackType.Slash);
    //go.transform.parent = player.transform;
    //go.transform.localPosition = Vector2.zero;
    /*go.transform.position = player.transform.position;
    Vector2 Pos = player.attackManager.attackDataBase.attackDatas[(int)AttackManager.AttackType.Slash].InitPosition;
    if(player.transform.localScale.x < 0){
    go.transform.position += new Vector3(-Pos.x,Pos.y);
    }else{
      go.transform.position += new Vector3(-Pos.x,Pos.y);
    }
    go.transform.localScale = new Vector3(go.transform.localScale.x * player.transform.localScale.x,go.transform.localScale.y,go.transform.localScale.z);
    yield break;
    */
    go.transform.parent = player.transform;
    go.transform.localPosition = Vector2.zero;
    go.transform.localPosition += player.attackManager.attackDataBase.attackDatas[(int)AttackManager.AttackType.HyperSlash].InitPosition;
    go.transform.localScale = new Vector3(go.transform.localScale.x * player.transform.localScale.x,go.transform.localScale.y,go.transform.localScale.z);
    yield break;
  }
}

public class HyperSlash:ICommand{
  public IEnumerator Command(PlayerController player){
    GameObject go = player.attackManager.SetEffect((int)AttackManager.AttackType.HyperSlash);
    go.transform.parent = player.transform;
    go.transform.localPosition = Vector2.zero;
    go.transform.localPosition += player.attackManager.attackDataBase.attackDatas[(int)AttackManager.AttackType.HyperSlash].InitPosition;
    go.transform.localScale = new Vector3(go.transform.localScale.x * player.transform.localScale.x,go.transform.localScale.y,go.transform.localScale.z);
    yield break;
    }
}

public class Magic:ICommand{
  public IEnumerator Command(PlayerController player){
    GameObject go = player.attackManager.SetEffect((int)AttackManager.AttackType.Magic);
    go.transform.position = player.transform.position;
    go.transform.localPosition += player.attackManager.attackDataBase.attackDatas[(int)AttackManager.AttackType.HyperSlash].InitPosition;
    float dir = player.transform.localScale.x;
    if(dir < 0) {
      go.transform.DOMoveX(-20,2).SetRelative(true).SetEase(Ease.Linear);
      go.GetComponent<SpriteRenderer>().flipX = false;
    }
    else go.transform.DOMoveX(20,2).SetRelative(true).SetEase(Ease.Linear);
    yield break;
}
}