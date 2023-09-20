using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeCheck : MonoBehaviour
{
    public bool IsSlope;
     private void OnTriggerStay2D(Collider2D other) {
            if(other.gameObject.tag == "ground"){
                 IsSlope = true;
                }
            }
       private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "ground"){
            IsSlope = false;
        }
       }
}
