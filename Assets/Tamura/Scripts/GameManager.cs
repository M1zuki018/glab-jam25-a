using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class GameManager : MonoBehaviour
{
    // シーン名
    [SerializeField] public string TitleSceneName       = "TitleScene";          // タイトルシーン
    [SerializeField] public string GuideSceneName       = "GuideScene";          // 遊び方説明シーン
    [SerializeField] public string GameMainSceneName    = "GameMainScene";       // ゲームシーン

    [SerializeField] public float FadeEffectTime        = 2.0f;                  // フェード時間[sec] (フェードイン + フェードアウト)

    private Image FadeImage;                                                     // フェード用の黒画像

    public static GameManager Instance;                                         // 自身のインスタンス


    // シングルトンパターンのインスタンスを取得するプロパティ
    // アプリケーション起動時、シングルトンパターンで自身のインスタンスを生成する
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    // ゲームマネージャーのインスタンスを生成するメソッド
    private static void CreateGameManager()
    {
        new GameObject("GameManager", typeof(GameManager));
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // シーンを超えて保持
        }
        else
        {
            Destroy(gameObject);            // 重複するインスタンスを削除
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // フェードエフェクト画像生成
        if (FadeImage == null)
        {
            // Canvasオブジェクトの生成
            GameObject canvasObj = new GameObject("FadeCanvas");
            DontDestroyOnLoad(canvasObj);                               // シーン遷移で保持
            Canvas canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            // Imageオブジェクトの生成
            GameObject fadeObject = new GameObject("FadeImage");
            fadeObject.AddComponent<Image>();
            fadeObject.transform.SetParent(canvasObj.transform, false);

            FadeImage = fadeObject.GetComponent<Image>();
            FadeImage.color = new Color(0, 0, 0, 0);                    // 初期設定で完全透明にしておく

            // 全画面にサイズ変更
            RectTransform rect = FadeImage.rectTransform;
            rect.sizeDelta = new Vector2(Screen.width, Screen.height);      // 画面サイズに合わせる
            rect.anchoredPosition = Vector2.zero;                           // 中央配置
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // シーンを切り替えるメソッド(トランジション処理付き)
    // string sceneName : 遷移先のシーン名
    // Color colorTransition : フェードの色
    public void ChangeScene(string sceneName, Color colorTransition)
    {
        FadeImage.color = colorTransition;
        StartCoroutine(SceneTransition(sceneName));
    }

    // シーン切り替えのコルーチン
    private IEnumerator SceneTransition(string sceneName)
    {
        yield return FadeIn(); // フェードインを実行
        SceneManager.LoadScene(sceneName); // シーン切り替え
        yield return FadeOut(); // フェードアウトを待機
    }

    // フェードインエフェクトを再生する
    private IEnumerator FadeIn()
    {
        // アルファ値
        float currentTime = 0;
        while (currentTime < FadeEffectTime / 2)                // フェードエフェクト時間の半分の時間まで繰り返す
        {
            float alpha = currentTime / (FadeEffectTime / 2);   // 徐々に暗くする
                                                                // 
            Color currentColor = FadeImage.color;   // 現在の色を取得
            FadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);            // 新しい色を作成（アルファ値だけ変更）

            currentTime += Time.deltaTime;
            yield return null;                                  // 次フレームまでウェイト
        }
    }

    // フェードアウトエフェクトを再生する
    private IEnumerator FadeOut()
    {
        float currentTime = 0;
        while (currentTime < FadeEffectTime / 2)                // フェードエフェクト時間の半分の時間まで繰り返す
        {
            float alpha = 1 - (currentTime / (FadeEffectTime / 2));     // 徐々に透明にする
            
            Color currentColor = FadeImage.color;   // 現在の色を取得
            FadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);            // 新しい色を作成（アルファ値だけ変更）

            currentTime += Time.deltaTime;
            yield return null;                               // 次フレームまでウェイト
        }

        // フェードアウト後、完全に透明になるよう担保する
        Color afterColor = FadeImage.color;
        FadeImage.color = new Color(afterColor.r, afterColor.g, afterColor.b, 0);
    }
}
