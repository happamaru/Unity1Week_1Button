using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour,IScore
{
    public int coinscore;

    public int AddScore()
    {
        //ゲームオブジェクトを削除
        Destroy(gameObject);

        return coinscore;
    }
}
