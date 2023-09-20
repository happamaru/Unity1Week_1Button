using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
   bool IsMove;
   GameObject Player;

    [SerializeField] private float GhostSpeed;     //幽霊の速度
    [SerializeField] private float limitSpeed;      //幽霊の制限速度
    private Rigidbody2D rb;                         //幽霊のRigidbody2D

    private void FixedUpdate()
    {
        if(IsMove){
        Vector3 vector3 = Player.transform.position - this.transform.position;  //弾から追いかける対象への方向を計算
        rb.AddForce(vector3.normalized * GhostSpeed);                  //方向の長さを1に正規化、任意の力をAddForceで加える
        float speedXTemp = Mathf.Clamp(rb.velocity.x, -limitSpeed, limitSpeed);//X方向の速度を制限
        float speedYTemp = Mathf.Clamp(rb.velocity.y, -limitSpeed, limitSpeed);  //Y方向の速度を制限
        rb.velocity = new Vector3(speedXTemp, speedYTemp);//実際に制限した値を代入
        }
    }
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        OnVisible = () =>{
            Player = GameObject.Find("Character");
            IsMove = true;
        };
        OnDisable = () =>{
            Destroy(this.gameObject);
        };
    }


}
