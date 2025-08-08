using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class QTEController : MonoBehaviour
{
    //制限時間
    [Header("QTE設定")]
    public float _timeLimit = 3.0f;
    [Header("UI")] 
    public Text qteText;
    public GameObject qtePanel;
    //入力キーの候補
    private KeyCode[] keyCodes = new KeyCode[] { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W };
    //今回のQTE中に押すキー
    private KeyCode targetkey;
    //経過時間
    private float timer;
    //QTE中かどうかの判定
    private bool isQTEActive = false;
    public void StartQTE(float duration)
    {
        //キーの抽選
        targetkey = keyCodes[UnityEngine.Random.Range(0,keyCodes.Length)];
        //制限時間のセット
        _timeLimit = duration;
        //経過時間の初期化
        timer = 0f;
        //QTEを有効にする
        isQTEActive = true;
        //パネルの表示
        qtePanel.SetActive(true);
        //押すキーのアナウンス
        qteText.text = $"{targetkey}を押す";
    }
    private void Update()
    {
        //QTE中でないなら何もしない
        if (!isQTEActive) return;
        //時間の進行
        timer += Time.deltaTime;
        //成功時
        if (Input.GetKeyDown(targetkey)) 
        {
            QTEsuccess();
        }
        //失敗時（制限時間切れ)
        else if (Time.deltaTime > timer)
        {
            QTEfailure();
        }

    }
    void QTEsuccess()//成功時の処理
    {
        Debug.Log("QTE成功");
        qteText.text = "成功";
        QTEend();
    }
    void QTEfailure()//失敗時の処理
    {
        Debug.Log("QTE失敗");
        qteText.text = "失敗";
        QTEend();
    }
    void QTEend()//QTE終了時の処理
    {
        isQTEActive = false;
        Invoke(nameof(Hidepanel), 1f);//1秒後にHidepanelを呼ぶ
    }
    void Hidepanel()
    {
        qtePanel.SetActive(false);//パネルを非表示にする
    }
}
