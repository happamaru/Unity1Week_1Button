using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : Enemy
{
    [SerializeField] float canonSpan;
    [SerializeField] float canonSpeed;
    [SerializeField] GameObject canonPrefab;
    [SerializeField] Animator aanimator;
    float delta;
    enum CanonType{
        Side,
        Vertical
    }
    [SerializeField] CanonType canonType;
    private void Update() {
        if(!IsMove) return;
        delta += Time.deltaTime;
        if(delta > canonSpan){
            delta = 0;
            GameObject go = Instantiate(canonPrefab);
            if(canonType == CanonType.Side){
                go.transform.position = this.transform.position;
                go.transform.AddPosY(-0.3f);
                go.transform.localScale = new Vector3(Mathf.Sign(this.transform.localScale.x)*go.transform.localScale.x,go.transform.localScale.y,go.transform.localScale.z);
                go.GetComponent<Rigidbody2D>().velocity = this.transform.localScale.x * Vector2.left * canonSpeed; 
            }else{
                go.transform.position = this.transform.position;
                go.transform.AddPosX(0.3f);
                go.transform.localScale = new Vector3(go.transform.localScale.x,Mathf.Sign(this.transform.localScale.y)*go.transform.localScale.y,go.transform.localScale.z);
                go.GetComponent<Rigidbody2D>().velocity = this.transform.localScale.x * Vector2.down * canonSpeed; 
            }
            }
        }
    void Start(){
        
        Rigidbody2D rg2d = GetComponent<Rigidbody2D>();
        aanimator = GetComponent<Animator>();
        OnVisible = () =>{
            IsMove = true;
            aanimator.enabled = true;
        };
        OnDisable = () =>{
            Destroy(this.gameObject);
        };
    }
}


