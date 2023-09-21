using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverCoin : MonoBehaviour, IScore
{
    public int silvercoinscore = 500;
    public GameObject coineffect;

    public int AddScore()
    {
        Instantiate(coineffect, transform.position, Quaternion.identity);

        Destroy(gameObject);

        return silvercoinscore;
    }
}
