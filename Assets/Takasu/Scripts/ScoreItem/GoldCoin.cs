using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    public int goldcoinscore = 500;
    public GameObject coineffect;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(AddScore());
            
        }
    }

    public int AddScore()
    {
        Instantiate(coineffect, transform.position, Quaternion.identity);

        Destroy(gameObject);

        return goldcoinscore;
    }
}
