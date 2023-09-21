using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDiamond : MonoBehaviour, IScore
{
    
    public GameObject diamondeffect;
    
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(AddScore());
            
        }

        
        
    }

    public int AddScore()
    {
        Instantiate(diamondeffect, pos, Quaternion.identity);
        Destroy(gameObject);

        return 1;
    }
}
