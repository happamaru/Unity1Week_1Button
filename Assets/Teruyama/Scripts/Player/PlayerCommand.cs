using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighJump:ICommand{
    public IEnumerator Command(PlayerController player){
     
      var rg2d = player.rg2d;
      rg2d.AddForce(Vector2.up * 1000);
      yield return new WaitForSeconds(0.3f);
    }
}
public class HighSpeed:ICommand{
    public IEnumerator Command(PlayerController player){
      player.MaxSpeed = 15;
      var rg2d = player.rg2d;
      rg2d.velocity = Vector2.zero;
      rg2d.gravityScale = 0;
      rg2d.AddForce(new Vector2(player.gameObject.transform.localScale.x,0) * 1000);
      yield return new WaitForSeconds(0.3f);
    }
}