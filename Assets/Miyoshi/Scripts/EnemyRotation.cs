using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [SerializeField] private float max_Interval = 5; // 回転間隔(秒)
    [SerializeField] private float min_Interval = 1; // 回転間隔(秒)
    [SerializeField] private float backInterval = 0.5f; // 後ろを向く間隔(秒)
    [SerializeField] private float rotateAngleY = 45f; // 1回あたりの回転角度(Y)
    bool isBack = false; // 後ろを向いているかどうか

    public float interval = 3; // 回転間隔
    private float timer;

    public virtual void Update()
    {
        // 経過時間を加算
        timer += Time.deltaTime;
         // 回転間隔をランダムに設定
        // 一定時間経過したら回転
        if (timer >= interval)
        { 
            // 現在のRotationを取得してY軸に加算
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y + rotateAngleY,
                transform.rotation.eulerAngles.z
            );
            Debug.Log(interval); // デバッグログ出力
            isBack = true; // 後ろを向くかどうかを切り替え
            // タイマーをリセット
            timer = 0f;
            interval = 0; // 回転間隔をリセット
            interval = Random.Range(min_Interval, max_Interval);
        }
        if (isBack)
        { // 後ろを向いている場合は回転しない
            if (timer >= backInterval)
            {
                // 後ろを向く
                transform.rotation = Quaternion.Euler(
                    transform.rotation.eulerAngles.x,
                    transform.rotation.eulerAngles.y + 180f,
                    transform.rotation.eulerAngles.z
                );
                // タイマーをリセット
                timer = 0f;
                isBack = false; // 後ろを向く状態を解除
            }
        }
    }
}
