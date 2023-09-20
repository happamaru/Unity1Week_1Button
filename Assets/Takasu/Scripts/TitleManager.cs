using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject TitleGroup;
    public GameObject TeamCompositionGroup;
    public TeamCompositionManager teamcompositionmanager;
    

    // Start is called before the first frame update
    void Start()
    {
        TeamCompositionGroup.GetComponent<CanvasGroup>().alpha = 0;
        TeamCompositionGroup.GetComponent<CanvasGroup>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //チーム編成画面に切り替わる
    public void OnTeamComposion()
    {
        Debug.Log("チーム編成");

        TitleGroup.GetComponent<CanvasGroup>().alpha = 0;
        TitleGroup.GetComponent<CanvasGroup>().interactable = false;

        teamcompositionmanager.TeamImageGroup.SetActive(true);
        teamcompositionmanager.CharaImageGroup.SetActive(true);
        teamcompositionmanager.CharaExplanationObject.SetActive(true);
        TeamCompositionGroup.GetComponent<CanvasGroup>().alpha = 1;
        TeamCompositionGroup.GetComponent<CanvasGroup>().interactable = true;

        //編成画面の更新
        teamcompositionmanager.SetTeamCompose();
    }

    //タイトル画面に切り替わる
    public void OnToTitle()
    {
        Debug.Log("タイトル画面");

        TeamCompositionGroup.GetComponent<CanvasGroup>().alpha = 0;
        TeamCompositionGroup.GetComponent<CanvasGroup>().interactable = false;
        teamcompositionmanager.TeamImageGroup.SetActive(false);
        teamcompositionmanager.CharaImageGroup.SetActive(false);
        teamcompositionmanager.CharaExplanationObject.SetActive(false);
        
        

        TitleGroup.GetComponent<CanvasGroup>().alpha = 1;
        TitleGroup.GetComponent<CanvasGroup>().interactable = true;

    }

    
}
