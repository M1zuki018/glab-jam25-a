using UnityEngine;
using DG.Tweening;

public class ImageUiManeg : MonoBehaviour
{
    [SerializeField] private RectTransform[] uiImages; // 複数のUI Imageを配列で管理
    [SerializeField] private Vector2 rightPos; // 右移動先のアンカー座標
    [SerializeField] private Vector2 startPos; // 中央位置（表示位置）
    [SerializeField] private Vector2 leftPos;  // 左移動先のアンカー座標
    [SerializeField] private float duration = 1f;

    private int currentIndex = 0; // 現在表示している画像のインデックス

    void Start()
    {
        // 全部非表示位置にして、現在のだけ中央に置く
        for (int i = 0; i < uiImages.Length; i++)
        {
            uiImages[i].anchoredPosition = (i == currentIndex) ? startPos : rightPos;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShowNextImage();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShowPreviousImage();
        }
    }

    void ShowNextImage()
    {
        int nextIndex = (currentIndex + 1) % uiImages.Length;

        // 現在の画像を左へ移動
        uiImages[currentIndex].DOAnchorPos(leftPos, duration).SetEase(Ease.OutCubic);

        // 次の画像を右から中央へ移動
        uiImages[nextIndex].anchoredPosition = rightPos; // 移動前の位置をセット
        uiImages[nextIndex].DOAnchorPos(startPos, duration).SetEase(Ease.OutCubic);

        currentIndex = nextIndex;
    }

    void ShowPreviousImage()
    {
        int prevIndex = (currentIndex - 1 + uiImages.Length) % uiImages.Length;

        // 現在の画像を右へ移動
        uiImages[currentIndex].DOAnchorPos(rightPos, duration).SetEase(Ease.OutCubic);

        // 前の画像を左から中央へ移動
        uiImages[prevIndex].anchoredPosition = leftPos; // 移動前の位置をセット
        uiImages[prevIndex].DOAnchorPos(startPos, duration).SetEase(Ease.OutCubic);

        currentIndex = prevIndex;
    }
}
