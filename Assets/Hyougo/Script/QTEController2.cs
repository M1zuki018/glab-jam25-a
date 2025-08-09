using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class QTEController2 : EnemyRotation
{
    [Header("UI")]
    public GameObject qtePanel; // QTEのUIパネル
    public Text qteText;        // QTE指示を表示するテキスト
    [Header("QTE設定")]
    public float qteDuration = 3f; // 制限時間
    public int keySequenceLength = 3; // 必要なキー入力の数
    // ランダム選択に使うキーの候補
    public KeyCode[] qteKeys = new KeyCode[] { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow };
    [Header("QTE対象プレイヤー")]
    public GameObject player;   // QTE対象のプレイヤー
    private GameObject targetPlayer2; // "Player2"タグを持つオブジェクト
    // QTE進行用フラグ・変数
    private bool isQTEActive = false; // 現在QTE中かどうか
    private float qteTimer = 0f;      // QTE開始からの経過時間
    private KeyCode[] currentKeys;    // 今回のQTEで押すべきキーの並び
    private int currentIndex = 0;     // 現在押すべきキーのインデックス
    private bool _prevIsBack = true;  // 直前の「背を向けていたか」の状態

    void Start()
    {
        // 起動時に "Player2"タグのオブジェクトを探して保持
        targetPlayer2 = GameObject.FindGameObjectWithTag("Player2");
    }
    public override void Update()
    {
        base.Update();
        keySequenceLength = _pickCount;
        if (targetPlayer2 == null) return;
        // 背を向いた瞬間かつQTEがまだ始まっていないとき → QTE開始
        if (_isBack && !_prevIsBack && !isQTEActive)
        {
            StartQTE();
        }
        // 前フレームの背向き状態を記録
        _prevIsBack = _isBack;
        // QTE中なら入力判定を行う
        if (isQTEActive)
        {
            HandleQTE();
        }
    }
    private async Task EndQTE()
    {
        isQTEActive = false;

        // 前を向く
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + 180f,
            transform.rotation.eulerAngles.z
        );
        if (_waitTime == true && _isBack == true)
        {
            await Task.Delay((int)900);

            if (player.GetComponent<PlayerController>() != null)
            {
                player.GetComponent<PlayerController>().BackOriginalPosX();
            }
            _waitTime = false;
        }
        _isBack = false; // 正面向きフラグに戻す

        Invoke(nameof(HideQTEPanel), 1f);
    }

    // QTEを開始する
    private void StartQTE()
    {
        isQTEActive = true;
        qteTimer = 0f;
        currentIndex = 0;
        // ランダムにキー列を作成
        currentKeys = new KeyCode[keySequenceLength];
        for (int i = 0; i < keySequenceLength; i++)
        {
            currentKeys[i] = qteKeys[Random.Range(0, qteKeys.Length)];
        }
        // UIにキー列を表示（例：「↑ → ↓」）
        qteText.text = KeysToText(currentKeys);
        qtePanel.SetActive(true);
        Debug.Log("QTE開始：キー列 = " + qteText.text);
    }
    // QTE中の入力処理
    private void HandleQTE()
    {
        // 経過時間を加算
        qteTimer += Time.deltaTime;
        // 何かキーが押されたとき
        if (Input.anyKeyDown)
        {
            // 今必要なキーが正しく押された場合
            if (Input.GetKeyDown(currentKeys[currentIndex]))
            {
                currentIndex++;
                // 全部押し終えたら成功
                if (currentIndex >= currentKeys.Length)
                {
                    QTESuccess();
                    return;
                }
            }
            else
            {
                // 間違ったキーを押した場合は失敗
                QTEFailure();
                return;
            }
        }
        // 制限時間を超えた場合も失敗
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
        if (player.TryGetComponent<PlayerController>(out var controller))
        {
            controller.HiddenShelter();
        }
        EndQTE();
    }
    // QTE失敗時の処理
    private void QTEFailure()
    {
        Debug.Log("QTE失敗");
        qteText.text = "失敗！";
        if (player.TryGetComponent<PlayerController>(out var controller))
        {
            controller.StunPlayer(2);

        }
        EndQTE();
    }
    // QTE終了時の共通処理
    //private void EndQTE()
    //{
    //    isQTEActive = false;
    //    // 敵を前向きに回転させる（180度回転）
    //    transform.rotation = Quaternion.Euler(
    //        transform.rotation.eulerAngles.x,
    //        transform.rotation.eulerAngles.y + 180f,
    //        transform.rotation.eulerAngles.z
    //    );
    //    _isBack = false;
    //    // 1秒後にUIパネルを非表示
    //    Invoke(nameof(HideQTEPanel), 1f);
    //}
    // QTEパネルを非表示にする
    private void HideQTEPanel()
    {
        qtePanel.SetActive(false);
    }
    // KeyCode配列を記号列に変換（例：↑ → ↓）
    private string KeysToText(KeyCode[] keys)
    {
        string result = "";
        foreach (var key in keys)
        {
            result += KeyToText(key) + " ";
        }
        return result.Trim();
    }
    // KeyCodeを矢印記号や文字列に変換
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
