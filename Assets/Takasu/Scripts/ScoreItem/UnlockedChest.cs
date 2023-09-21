using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedChest : MonoBehaviour
{
    public GameObject treasure;
    void Awake()
    {
        //StartCoroutine("OpenChest");
    }

    public void Test()
    {
        StartCoroutine("OpenChest");
    }

    IEnumerator OpenChest()
    {
        yield return new WaitForSeconds(0.7f);

        //宝箱の中身を出す処理(n個のtreasureをインスタンス化したい)
        //いったん一個だけ出す処理にする
        Instantiate(treasure, new Vector3(transform.position.x, transform.position.y + 1.0f, 1.0f), Quaternion.identity);

    }
}
