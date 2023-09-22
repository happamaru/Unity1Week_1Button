using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BlueDiamond : MonoBehaviour, IScore
{
    public int bluediamondscore = 5000;
    
    public GameObject diamondeffect;

    public GameObject scoretext;
    public float scoresize = 1.5f;
    
    Vector3 pos;

    GameObject canvas;
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;


        text = scoretext.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddScore();
        }
    }


    public int AddScore()
    {
        Instantiate(diamondeffect, pos, Quaternion.identity);

        DisplayScore(bluediamondscore);

        Destroy(gameObject);

        return bluediamondscore;
    }

    void DisplayScore(int score)
    {
        GameObject go = Instantiate(scoretext, transform.position, quaternion.identity);
        go.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.5f, 1.0f);
        go.GetComponent<RectTransform>().localScale = go.transform.GetComponent<RectTransform>().localScale * scoresize;
        text.text = "+" + score;

        go.transform.DOMoveY(transform.position.y + 0.5f, 0.5f, false).SetEase(Ease.OutBack).OnComplete(() => {
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOFade(0.0f, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                Destroy(go);
            });
        });
    }
}
