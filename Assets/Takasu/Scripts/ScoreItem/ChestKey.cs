using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestKey : MonoBehaviour
{
    public GameObject treasure;
    public GameObject chestkeyeffect;

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            GetKey();        
        }
    }

    public void GetKey()
    {
        GameObject go = Instantiate(chestkeyeffect, transform.position, Quaternion.identity);
        transform.GetChild(0).transform.parent = go.transform;
        go.GetComponent<ChestKeyEffect>().treasure = treasure;

        Destroy(gameObject);
    }
}
