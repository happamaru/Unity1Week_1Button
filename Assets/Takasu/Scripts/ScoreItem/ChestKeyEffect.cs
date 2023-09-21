using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestKeyEffect : MonoBehaviour
{
    public GameObject treasure;
    public GameObject UnlockedChest;
    SpriteRenderer spriteRenderer;
    GameObject go;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
        player = GameObject.Find("Character");
        StartCoroutine(effectEnd());
    }

    IEnumerator effectEnd(){
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.enabled =false;
    }

    GameObject player;
    bool once;
    void Update(){
        if(once) return; 
        if(Mathf.Abs(this.transform.GetChild(0).transform.position.x - player.transform.position.x) < 5){
            once = true;
            StartCoroutine("Destroy");      
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.25f);
        go =  Instantiate(UnlockedChest, transform.GetChild(0).transform.position, Quaternion.identity);
        go.GetComponent<UnlockedChest>().treasure = treasure;
        go.GetComponent<UnlockedChest>().Test();
             
        Destroy(gameObject);
    }
}
