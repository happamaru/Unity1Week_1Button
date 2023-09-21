using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{
    void Awake()
    {
        transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);

        StartCoroutine("Destroy");
    }

    void Start()
    {
        
    }

    IEnumerator Destroy() {
        yield return new WaitForSeconds(0.25f);

        Destroy(gameObject);
    }
}
