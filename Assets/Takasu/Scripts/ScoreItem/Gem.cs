using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IScore
{
    public int gemscore;

    public int AddScore()
    {
        Destroy(gameObject);

        return gemscore;
    }   
}
