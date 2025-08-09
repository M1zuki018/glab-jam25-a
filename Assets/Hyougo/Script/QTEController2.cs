using UnityEngine;
using UnityEngine.UI;

public class QTEController2 : EnemyRotation
{
    [Header("UI")]
    public GameObject qtePanel;// QTEのUIパネル
    public Text qteText;// QTEの指示を表示するテキスト
    [Header("QTE設定")]
    public float qteDuration = 2f;// QTEの制限時間
    public KeyCode[] qteKeys = new KeyCode[] { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };// ランダムに選ばれるQTEのキー候補
    [Header("QTE対象プレイヤー")]
    public GameObject player;// QTEの対象とするプレイヤーのタグ
    private GameObject targetPlayer;// "Player1"タグを持つオブジェクトの参照
    private bool isQTEActive = false;// 現在QTE中かどうか
    private float qteTimer = 0f;// QTE中の経過時間
    private KeyCode currentKey;// 今回要求するQTEキー
    private bool _prevIsBack = true;// 直前の「背を向けている状態」の保存用

    void Start()
    {
        // 最初に Player1 タグのついたオブジェクトを検索して保存
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
    }
    public override void Update()
    {
        base.Update();

        if (targetPlayer == null) return;

        // 「背を向いた瞬間」かつ QTE中でないなら開始
        if (_isBack && !_prevIsBack && !isQTEActive)
        {
            StartQTE();
        }

        _prevIsBack = _isBack;

        if (isQTEActive)
        {
            HandleQTE();
        }
    }

    private void EndQTE()
    {
        isQTEActive = false;

        // 前を向く
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + 180f,
            transform.rotation.eulerAngles.z
        );

        _isBack = false; // 正面向きフラグに戻す

        Invoke(nameof(HideQTEPanel), 1f);
    }
    // QTEを開始する処理
    private void StartQTE()
    {
        // QTE開始フラグ
        isQTEActive = true;
        // 経過時間リセット
        qteTimer = 0f;
        // ランダムにキーを1つ選ぶ
        currentKey = qteKeys[Random.Range(0, qteKeys.Length)];
        // UIに「〇を押せ！」と表示
        qteText.text = $"{currentKey} を押せ！";
        // パネル表示
        qtePanel.SetActive(true);
        Debug.Log("QTE開始：Player1に対してのみ");
    }
    // QTE中の入力処理
    private void HandleQTE()
    {
        // 経過時間を加算
        qteTimer += Time.deltaTime;
        // 正しいキーを押したら成功
        if (Input.GetKeyDown(currentKey))
        {
            QTESuccess();
        }
        else
        {
            // 他のキーを押した場合は失敗
            foreach (KeyCode key in qteKeys)
            {
                if (key != currentKey && Input.GetKeyDown(key))
                {
                    QTEFailure();
                    break;
                }
            }
        }
        // 時間切れも失敗とする
        if (qteTimer > qteDuration)
        {
            QTEFailure();
        }
    }
    // QTE成功時の処理
    private void QTESuccess()
    {
        Debug.Log("QTE成功");
        qteText.text = "成功！";
        EndQTE();
    }
    // QTE失敗時の処理
    private void QTEFailure()
    {
        Debug.Log("QTE失敗");
        qteText.text = "失敗！";
        EndQTE();
    }
    // QTE終了時の処理（1秒後にUIを非表示にする）
    //private void EndQTE()
    //{
    //    // フラグリセット
    //    isQTEActive = false;

    //    _isBack = false; // QTE終了後は背を向けている状態に戻す
    //    // 1秒後にパネルを非表示
    //    Invoke(nameof(HideQTEPanel), 1f);
    //}
    // UIを非表示にする処理
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
