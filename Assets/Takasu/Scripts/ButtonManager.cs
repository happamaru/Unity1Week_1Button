using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour,
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public System.Action onClickCallback;

    Vector2 OriginalScale;

    [SerializeField] bool Type_MoveScene;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(OriginalScale * 1.3f, 0.24f).SetUpdate(true).SetEase(Ease.OutCubic);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(OriginalScale, 0.24f).SetUpdate(true).SetEase(Ease.OutCubic);
    }
}