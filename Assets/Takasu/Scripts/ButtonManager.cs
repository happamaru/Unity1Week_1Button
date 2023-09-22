using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour,
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public System.Action onClickCallback;

    Vector2 OriginalScale;

    [SerializeField] bool Type_MoveScene;

   [SerializeField] AudioClip StartaudioClip;
    bool isClick;

    void OnEnable()
    {
        isClick = false;
    }

    void Start()
    {
        OriginalScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isClick) return;
        //SoundManager.soundManager.ButtonsoundPlay(0, 1);
        if (Type_MoveScene)
        {
            isClick = true;
        }
        onClickCallback?.Invoke();

            
    }

    public void StartButton(){
       SoundManager_SE.m_Instane.PlaySoundEfect(StartaudioClip,0.2f);
       // SoundManager_BGM.m_Instane.PlayBackGroundMusic(audioClip,0.2f);
        SceneManager.LoadScene("MainScene");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(OriginalScale * 1.3f, 0.24f).SetUpdate(true).SetEase(Ease.OutCubic);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(OriginalScale, 0.24f).SetUpdate(true).SetEase(Ease.OutCubic);
    }
}