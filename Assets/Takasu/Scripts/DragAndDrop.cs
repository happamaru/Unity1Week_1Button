using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 initialPosition; // ドラッグ前の初期位置

    public Image dropTargetImage; // ドロップ先となるUIImageをアサイン

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition; // 初期位置を保持
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // ドロップ先が有効で、ドロップ位置がドロップ先内であるかをチェック
        if (dropTargetImage != null && RectTransformUtility.RectangleContainsScreenPoint(dropTargetImage.rectTransform, eventData.position))
        {
            // ドロップがドロップ先内で終了した場合の処理
            // ここにドロップが成功した際の処理を追加
            Debug.Log("ドラッグアンドドロップ！");
        }
        else
        {
            // ドロップがドロップ先外で終了した場合の処理
            // 初期位置に戻すなどの処理を追加
            rectTransform.anchoredPosition = initialPosition;
        }
    }
}
