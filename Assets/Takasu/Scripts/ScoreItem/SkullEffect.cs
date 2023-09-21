using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullEffect : MonoBehaviour
{
    void Awake()
    {
        transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);

        StartCoroutine("Destroy");
    }


    IEnumerator Destroy() {
        yield return new WaitForSeconds(0.42f);

        Destroy(gameObject);
    }
}
