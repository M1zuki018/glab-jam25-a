using UnityEngine;
using UnityEngine.UI;

// プレイヤー2に対してのみQTE（Quick Time Event）を発動するクラス。
// 敵が振り向いたタイミングで、指定された矢印キーをプレイヤー2に押させる。
public class QTEController2 : EnemyRotation
{
    [Header("UI")]
    public GameObject qtePanel;// QTEのUIパネル
    public Text qteText;// QTEで表示されるテキスト
    [Header("QTE設定")]
    public float qteDuration = 2f;// QTEの制限時間
    public KeyCode[] qteKeys = new KeyCode[]{KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow};// QTEで使用するキー
    [Header("QTE対象プレイヤー")]
    public string playerTag = "Player2";// QTEの対象とするプレイヤーのタグ
    private GameObject targetPlayer;// Player2の参照（タグで取得）
    private bool isQTEActive = false;// QTEが現在アクティブかどうか
    private float qteTimer = 0f;// QTEの経過時間
    private KeyCode currentKey;// 今回のQTEで要求されるキー
    private bool _prevIsBack = true;// 前のフレームで敵が後ろを向いていたかどうか

    void Start()
    {
        // タグ「Player2」のオブジェクトを探す
        targetPlayer = GameObject.FindGameObjectWithTag(playerTag);
    }
    public override void Update()
    {
        // EnemyRotationのUpdateを呼ぶ
        base.Update();
        // Player2が見つからない場合は何もしない
        if (targetPlayer == null) return;
        // 敵が後ろを向いていた → 振り向いた瞬間にQTEを開始
        if (_prevIsBack && !_isBack && !isQTEActive)
        {
            StartQTE();
        }
        // 現在の向き状態を次回比較用に保存
        _prevIsBack = _isBack;
        // QTE中なら入力処理を行う
        if (isQTEActive)
        {
            HandleQTE();
        }
    }
    // QTEを開始する
    private void StartQTE()
    {
        isQTEActive = true;
        qteTimer = 0f;
        // ランダムで押させるキーを選択
        currentKey = qteKeys[Random.Range(0, qteKeys.Length)];
        // テキストに矢印記号で指示を表示
        qteText.text = $"{KeyToText(currentKey)} を押せ！";
        // UIパネル表示
        qtePanel.SetActive(true);
        Debug.Log("QTE開始：Player2に対してのみ");
    }
    // QTE中の入力処理
    private void HandleQTE()
    {
        qteTimer += Time.deltaTime;
        // 正解のキーを押したら成功
        if (Input.GetKeyDown(currentKey))
        {
            QTESuccess();
        }
        else
        {
            // 間違ったキーを押したら失敗
            foreach (KeyCode key in qteKeys)
            {
                if (key != currentKey && Input.GetKeyDown(key))
                {
                    QTEFailure();
                    break;
                }
            }
        }
        // 時間切れで失敗
        if (qteTimer > qteDuration)
        {
            QTEFailure();
        }
    }
    // QTE成功時の処理
    private void QTESuccess()
    {
        Debug.Log("QTE成功 (Player2)");
        qteText.text = "成功！";
        EndQTE();
    }
    // QTE失敗時の処理
    private void QTEFailure()
    {
        Debug.Log("QTE失敗 (Player2)");
        qteText.text = "失敗！";
        EndQTE();
    }
    // QTE終了処理（UIを非表示にし、状態リセット）
    private void EndQTE()
    {
        isQTEActive = false;
        // 1秒後にパネルを非表示
        Invoke(nameof(HideQTEPanel), 1f); 
    }
    // QTEのUIパネルを非表示にする
    private void HideQTEPanel()
    {
        qtePanel.SetActive(false);
    }
    // KeyCodeを矢印記号（↑ ↓ ← →）の文字列に変換する
    private string KeyToText(KeyCode key)
    {
        return key switch
        {
            KeyCode.UpArrow => "↑",
            KeyCode.DownArrow => "↓",
            KeyCode.LeftArrow => "←",
            KeyCode.RightArrow => "→",
            _ => key.ToString()
        };
    }
}
