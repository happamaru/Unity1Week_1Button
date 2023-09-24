using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
   [SerializeField] GameObject player;
   [SerializeField] float LimitLeftPosX;
   [SerializeField] float LimitRightPosX;
    private void Update() {
        this.transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-10);

        if(this.transform.position.x < LimitLeftPosX){
            this.transform.SetPosX(LimitLeftPosX);
        }
        if(this.transform.position.x > LimitRightPosX){
            this.transform.SetPosX(LimitRightPosX);
        }        
        if(this.transform.position.y < 0){
            this.transform.SetPosY(0);
        }
   }
}
