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
      //rg2d.gravityScale = 0;
      player.IsNoButton = true;
      rg2d.constraints = RigidbodyConstraints2D.FreezeRotation  //Rotationを全てオン
            | RigidbodyConstraints2D.FreezePositionY;  //PositionのYのみオン
      rg2d.AddForce(new Vector2(player.gameObject.transform.localScale.x,0) * 2000);
      yield return new WaitForSeconds(0.3f);
      player.IsNoButton = false;
      rg2d.constraints = RigidbodyConstraints2D.FreezeRotation;  //Rotationを全てオン
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
    go.transform.localScale = new Vector3(go.transform.localScale.x * player.transform.localScale.x,go.transform.localScale.y,go.transform.localScale.z);
    float dir = player.transform.localScale.x;
    if(dir < 0) {
      go.transform.DOMoveX(-20,2).SetRelative(true).SetEase(Ease.Linear);
      go.GetComponent<SpriteRenderer>().flipX = false;
    }
    else go.transform.DOMoveX(20,2).SetRelative(true).SetEase(Ease.Linear);
    yield break;
}
}

public class thief:ICommand{
  public IEnumerator Command(PlayerController player){
    GameObject go = player.InitThief();
    go.transform.position = player.transform.position;
    yield break;
}
}

public class CraftMan:ICommand{
  public IEnumerator Command(PlayerController player){
    GameObject go = player.InitBlock();
    go.transform.position = player.transform.position;
    go.transform.AddPosY(-1);
    yield break;
}
}

public class BigExploMagic:ICommand{
  public IEnumerator Command(PlayerController player){
    player.attackManager.StartCoroutine(player.attackManager.BigMagic(player.transform.position,18,player.transform.localScale.x));   
    yield break;
}
}

public class BowAttack:ICommand{
  public IEnumerator Command(PlayerController player){
    GameObject go = player.attackManager.SetEffect(6);
    go.transform.position = player.transform.position;
    go.transform.localPosition += player.attackManager.attackDataBase.attackDatas[(int)AttackManager.AttackType.HyperSlash].InitPosition;
   go.transform.localScale = new Vector3(go.transform.localScale.x * player.transform.localScale.x,go.transform.localScale.y,go.transform.localScale.z);
    float dir = player.transform.localScale.x;
    if(dir < 0) {
      go.transform.DOMoveX(-20,2).SetRelative(true).SetEase(Ease.Linear);
      go.GetComponent<SpriteRenderer>().flipX = false;
    }
    else go.transform.DOMoveX(20,2).SetRelative(true).SetEase(Ease.Linear);
    yield break;
}
}