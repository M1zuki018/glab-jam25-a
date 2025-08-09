using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QteManager : MonoBehaviour
{
    [Header("UI")]
    public Text qteText;
    public GameObject qtePanel;
    public enum KeyType { WASD, Arrow }
    public KeyType keyType = KeyType.WASD;

    // 制限時間
    private float _timeLimit;

    // 入力キーの候補
    private KeyCode[] wasdKeys = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    private KeyCode[] arrowKeys = new KeyCode[] { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };

    // QTE中に押すキー
    private KeyCode targetKey;
    // 経過時間
    private float timer;
    // QTE中かどうか
    private bool isQteActive = false;

    public void StartQte(float duration)
    {
        // キーの抽選
        KeyCode[] selectedKeys = keyType == KeyType.WASD ? wasdKeys : arrowKeys;
        targetKey = selectedKeys[UnityEngine.Random.Range(0, selectedKeys.Length)];

        // 制限時間と初期化
        _timeLimit = duration;
        timer = 0f;
        isQteActive = true;

        // パネル表示
        qtePanel.SetActive(true);
        qteText.text = $"{KeyToSymbol(targetKey)} を押せ";
    }

    private void Update()
    {
        if (!isQteActive) return;

        timer += Time.deltaTime;

        if (Input.GetKeyDown(targetKey))
        {
            QteSuccess();
        }
        else if (timer > _timeLimit)
        {
            QteFailure();
        }
    }

    void QteSuccess()
    {
        Debug.Log("QTE成功");
        qteText.text = "成功";
        QteEnd();
    }

    void QteFailure()
    {
        Debug.Log("QTE失敗");
        qteText.text = "失敗";
        QteEnd();
    }

    void QteEnd()
    {
        isQteActive = false;
        Invoke(nameof(HidePanel), 1f);
    }

    void HidePanel()
    {
        qtePanel.SetActive(false);
    }

    // キーを記号（↑↓←→など）で表示
    string KeyToSymbol(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.UpArrow: return "↑";
            case KeyCode.DownArrow: return "↓";
            case KeyCode.LeftArrow: return "←";
            case KeyCode.RightArrow: return "→";
            default: return key.ToString();
        }
    }
}
