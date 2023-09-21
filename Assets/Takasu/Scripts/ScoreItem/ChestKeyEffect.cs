using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestKeyEffect : MonoBehaviour
{
    public GameObject UnlockedChest;

    void Awake()
    {
        transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);

        StartCoroutine("Destroy");
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.25f);

        GameObject go =  Instantiate(UnlockedChest, transform.GetChild(0).transform.position, Quaternion.identity);

        go.GetComponent<UnlockedChest>().Test();

        Destroy(gameObject);
    }
}
