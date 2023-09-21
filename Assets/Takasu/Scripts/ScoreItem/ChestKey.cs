using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestKey : MonoBehaviour
{
    public GameObject chestkeyeffect;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            GetKey();
    }

    public void GetKey()
    {
        GameObject go = Instantiate(chestkeyeffect, transform.position, Quaternion.identity);
        transform.GetChild(0).transform.parent = go.transform;

        Destroy(gameObject);
    }
}
