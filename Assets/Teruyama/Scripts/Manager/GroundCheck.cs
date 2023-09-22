using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool IsGround;
    [SerializeField] PlayerController playerController; 
     private void OnTriggerStay2D(Collider2D other) {
            if(other.gameObject.tag == "ground"){
                //if(!IsGround){
                //playerController.rg2d.velocity = new Vector2(playerController.rg2d.velocity.x,0);
                IsGround = true;
                playerController.JumpCount = 0;
                //}
            }
                
        }
       
        private void OnTriggerExit2D(Collider2D other) {
            if(other.gameObject.tag == "ground"){
                IsGround = false;
            }
        }
}
