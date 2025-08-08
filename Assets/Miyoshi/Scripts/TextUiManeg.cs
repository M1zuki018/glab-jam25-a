using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TextUiManeg : MonoBehaviour
{
    [SerializeField] private Text[] uiTexts; // 複数のUI Textを配列で管理  

    private int currentIndex = 0; // 現在表示しているテキストのインデックス  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShowNextText();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShowPreviousText();
        }
    }

    void ShowNextText()
    {
        int nextIndex = (currentIndex + 1) % uiTexts.Length;
        // 現在のテキストを非表示にする
        uiTexts[currentIndex].DOFade(0f, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            uiTexts[currentIndex].gameObject.SetActive(false);
            // 次のテキストを表示する
            uiTexts[nextIndex].gameObject.SetActive(true);
            uiTexts[nextIndex].DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        });
        currentIndex = nextIndex;
    }

    void ShowPreviousText()
    {
        int prevIndex = (currentIndex - 1 + uiTexts.Length) % uiTexts.Length;
        // 現在のテキストを非表示にする
        uiTexts[currentIndex].DOFade(0f, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            uiTexts[currentIndex].gameObject.SetActive(false);
            // 前のテキストを表示する
            uiTexts[prevIndex].gameObject.SetActive(true);
            uiTexts[prevIndex].DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        });
        currentIndex = prevIndex;
    }
}
