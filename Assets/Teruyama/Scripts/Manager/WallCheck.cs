using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public bool IsWall;
     private void OnTriggerStay2D(Collider2D other) {
            if(other.gameObject.tag == "ground"){
                IsWall = true;
                }
            }
       private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "ground"){
            IsWall = false;
        }
       }
}
