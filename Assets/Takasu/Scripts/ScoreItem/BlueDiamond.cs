using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDiamond : MonoBehaviour, IScore
{
    public int bluediamondscore = 5000;
    
    public GameObject diamondeffect;
    
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        
    }


    public int AddScore()
    {
        Instantiate(diamondeffect, pos, Quaternion.identity);
        Destroy(gameObject);

        return bluediamondscore;
    }
}
