using UnityEngine;
using UnityEngine.UI;

// 敵の振り向き情報（_isBack）を使いたいため EnemyRotation を継承
public class QTEController : EnemyRotation
{
    [Header("UI")]
    public GameObject qtePanel;// QTEのUIパネル
    public Text qteText;// QTEの指示を表示するテキスト
    [Header("QTE設定")]
    public float qteDuration = 2f;// QTEの制限時間
    public KeyCode[] qteKeys = new KeyCode[]{ KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W };// ランダムに選ばれるQTEのキー候補
    [Header("QTE対象プレイヤー")]
    public string playerTag = "Player1";// QTEの対象とするプレイヤーのタグ
    private GameObject targetPlayer;// "Player1"タグを持つオブジェクトの参照
    private bool isQTEActive = false;// 現在QTE中かどうか
    private float qteTimer = 0f;// QTE中の経過時間
    private KeyCode currentKey;// 今回要求するQTEキー
    private bool _prevIsBack = true;// 直前の「背を向けている状態」の保存用

    void Start()
    {
        // 最初に Player1 タグのついたオブジェクトを検索して保存
        targetPlayer = GameObject.FindGameObjectWithTag(playerTag);
    }
    public override void Update()
    {
        // EnemyRotation の Update 処理も呼ぶ
        base.Update();
        // Player1 が存在しなければ何もしない
        if (targetPlayer == null) return;
        // 「前は背を向けていた」→「今は振り向いている」かつ QTE中でない → QTE開始
        if (_prevIsBack && !_isBack && !isQTEActive)
        {
            StartQTE();
        }
        // _prevIsBack を現在の状態で更新して次のフレームに備える
        _prevIsBack = _isBack;
        // QTE中なら処理を続ける
        if (isQTEActive)
        {
            HandleQTE();
        }
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
    private void EndQTE()
    {
        // フラグリセット
        isQTEActive = false;
        // 1秒後にパネルを非表示
        Invoke(nameof(HideQTEPanel), 1f);
    }
    // UIを非表示にする処理
    private void HideQTEPanel()
    {
        qtePanel.SetActive(false);
    }
}
