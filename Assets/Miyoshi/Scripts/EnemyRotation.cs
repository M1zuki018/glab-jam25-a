using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [Header("レベル１の時の最大最小")]
    [SerializeField] private float max1_Interval = 10; // 回転間隔(秒)
    [SerializeField] private float min1_Interval = 7; // 回転間隔(秒)
    [Header("レベル２の時の最大最小")]
    [SerializeField] private float max2_Interval = 5; // 回転間隔(秒)
    [SerializeField] private float min2_Interval = 1; // 回転間隔(秒)
    [Header("レベル３の時の最大最小")]
    [SerializeField] private float max3_Interval = 5; // 回転間隔(秒)
    [SerializeField] private float min3_Interval = 1; // 回転間隔(秒)
    [Header("バージョン管理０，１，２")]
    [SerializeField] private int _versions = 0; // バージョン管理用の変数
    [Header("元の方向に戻る時間")]
    [SerializeField] private float _backInterval = 0.5f; // 後ろを向く間隔(秒)
    [Header("回転角度")]
    [SerializeField] private float _rotateAngleY = 45f; // 1回あたりの回転角度(Y)
    bool _isBack = false; // 後ろを向いているかどうか
    [Header("それぞれのレベル最初の一回目の時間")]
    [SerializeField] public float _interval1 = 3; // 回転間隔(ランダムに設定される)
    [SerializeField] public float _interval2 = 2; // 回転間隔(ランダムに設定される)
    [SerializeField] public float _interval3 = 1; // 回転間隔(ランダムに設定される)
    private float _timer;

    public virtual void Update()
    {
        // 経過時間を加算
        _timer += Time.deltaTime;
         // 回転間隔をランダムに設定
        // 一定時間経過したら回転
        if (_timer >= _interval1 && _versions ==0)
        { 
            // 現在のRotationを取得してY軸に加算
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y + _rotateAngleY,
                transform.rotation.eulerAngles.z
            );
            Debug.Log(_interval1); // デバッグログ出力
            _isBack = true; // 後ろを向くかどうかを切り替え
            // タイマーをリセット
            _timer = 0f;
            _interval1 = 0; // 回転間隔をリセット
            _interval1 = Random.Range(min1_Interval, max1_Interval);
        }
        // interval2の処理
        if (_timer >= _interval1 && _versions == 1)
        {
            // 現在のRotationを取得してY軸に加算
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y + _rotateAngleY,
                transform.rotation.eulerAngles.z
            );
            Debug.Log(_interval2); // デバッグログ出力
            _isBack = true; // 後ろを向くかどうかを切り替え
            // タイマーをリセット
            _timer = 0f;
            _interval2 = 0; // 回転間隔をリセット
            _interval2 = Random.Range(min2_Interval, max2_Interval);
        }
        // interval3の処理
        if (_timer >= _interval1 && _versions == 2)
        {
            // 現在のRotationを取得してY軸に加算
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y + _rotateAngleY,
                transform.rotation.eulerAngles.z
            );
            Debug.Log(_interval3); // デバッグログ出力
            _isBack = true; // 後ろを向くかどうかを切り替え
            // タイマーをリセット
            _timer = 0f;
            _interval3 = 0; // 回転間隔をリセット
            _interval3 = Random.Range(min3_Interval, max3_Interval);
        }
        if (_isBack)
        { // 後ろを向いている場合は回転しない
            if (_timer >= _backInterval)
            {
                // 後ろを向く
                transform.rotation = Quaternion.Euler(
                    transform.rotation.eulerAngles.x,
                    transform.rotation.eulerAngles.y + 180f,
                    transform.rotation.eulerAngles.z
                );
                // タイマーをリセット
                _timer = 0f;
                _isBack = false; // 後ろを向く状態を解除
            }
        }
    }
}
