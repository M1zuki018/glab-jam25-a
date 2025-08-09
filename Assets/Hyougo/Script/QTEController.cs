using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class QTEController : EnemyRotation
{
    [Header("UI")]
    public GameObject qtePanel;// QTEのUIパネル
    public Text qteText;// QTEの指示を表示するテキスト
    [Header("QTE設定")]
    public float qteDuration = 2f;// QTEの制限時間
    public int keySequenceLength = 3; // 必要なキー入力の数
    public KeyCode[] qteKeys = new KeyCode[] { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W };// ランダムに選ばれるQTEのキー候補
    [Header("QTE対象プレイヤー")]
    public GameObject player;// QTEの対象とするプレイヤーのタグ
    private GameObject targetPlayer;// "Player1"タグを持つオブジェクトの参照
    private bool isQTEActive = false;// 現在QTE中かどうか
    private float qteTimer = 0f;// QTE中の経過時間
    private KeyCode[] currentKeys;// 今回要求するQTEキー
    private int currentIndex = 0;     // 現在押すべきキーのインデックス
    private bool _prevIsBack = true;// 直前の「背を向けている状態」の保存用



    void Start()
    {
        // 最初に Player1 タグのついたオブジェクトを検索して保存
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
    }
    public override void Update()
    {
        base.Update();

        keySequenceLength = _pickCount;
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
    // QTEを開始する処理
    private void StartQTE()
    {
        // QTE開始フラグ
        isQTEActive = true;

        // 経過時間リセット
        qteTimer = 0f;

        currentIndex = 0;

        // ランダムにキーを3つ選ぶ
        currentKeys = new KeyCode[keySequenceLength];
        for (int i = 0; i < keySequenceLength; i++)
        {
            currentKeys[i] = qteKeys[Random.Range(0, qteKeys.Length)];
        }

        // UIに「〇を押せ！」と表示
        qteText.text = string.Join("  ",currentKeys) + " を押せ！";


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
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(currentKeys[currentIndex]))
            {
                currentIndex++;
                if (currentIndex >= currentKeys.Length)
                {
                    QTESuccess();
                    return;
                }
            }
            else
            {
                QTEFailure();
                return;
            }
        }
        if (qteTimer > qteDuration) // 時間切れ
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
}
