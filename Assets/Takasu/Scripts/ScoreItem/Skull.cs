using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour, IScore
{
    public GameObject coineffect;
    public GameObject skulleffect;
    public int[] skullscore = new int[4];


    public int AddScore()
    {
        int score;

        int scorerand = Random.Range(0, 100);

        if(scorerand > 80)// 81 ~ 99で10000点
        {
            score = skullscore[0];
        }
        else if(scorerand > 40)//41 ~ 80で1000点
        {
            score = skullscore[1];
                
        }
        else if(scorerand > 10)//11～40で500点
        {
            score = skullscore[2];     
        }
        else//0～10で-5000点
            score = skullscore[3];
        
        if(scorerand > 10)
            Instantiate(coineffect, transform.position, Quaternion.identity);
        else
            Instantiate(skulleffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
            
        return score;
    }
}
