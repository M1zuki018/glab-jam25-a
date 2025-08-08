using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CanvasManeg : MonoBehaviour // クラスを MonoBehaviour にする必要があります  
{
    private RectTransform uiRoot;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        uiRoot = GameObject.Find("UIRoot").GetComponent<RectTransform>();

        canvasGroup = uiRoot.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = uiRoot.gameObject.AddComponent<CanvasGroup>();

        // 最初は透明 & スケール0  
        canvasGroup.alpha = 0f;
        uiRoot.localScale = Vector3.zero;
    }

    // Start メソッドは Unity のライフサイクルメソッドであり、修飾子を付けることはできません。  
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);

        Vector3 overScale = Vector3.one * 1.2f;

        Sequence seq = DOTween.Sequence();
        seq.Append(uiRoot.DOScale(overScale, 0.6f).SetEase(Ease.OutCubic)) // 修正: 'Ease.OutCubic' をプロパティとして使用  
           .Join(canvasGroup.DOFade(1f, 0.6f))
           .Append(uiRoot.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutCubic));
    }
}
