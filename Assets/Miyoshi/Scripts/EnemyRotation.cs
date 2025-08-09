using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [Header("レベル１の時の最大最小")]
    [SerializeField] public float max1_Interval = 10; // 回転間隔(秒)
    [SerializeField] public float min1_Interval = 7; // 回転間隔(秒)
    [Header("レベル２の時の最大最小")]
    [SerializeField] public float max2_Interval = 5; // 回転間隔(秒)
    [SerializeField] public float min2_Interval = 1; // 回転間隔(秒)
    [Header("レベル３の時の最大最小")]
    [SerializeField] public float max3_Interval = 5; // 回転間隔(秒)
    [SerializeField] public float min3_Interval = 1; // 回転間隔(秒)
    [Header("バージョン管理０，１，２")]
    [SerializeField] public int _versions = 0; // バージョン管理用の変数
    [Header("元の方向に戻る時間")]
    [SerializeField] public float _backInterval = 0.5f; // 後ろを向く間隔(秒)
    [Header("回転角度")]
    [SerializeField] public float _rotateAngleY = 45f; // 1回あたりの回転角度(Y)
    protected bool _isBack = false; // 後ろを向いているかどうか
    [Header("それぞれのレベル最初の一回目の時間")]
    [SerializeField] public float _interval1 = 3; // 回転間隔(ランダムに設定される)
    [SerializeField] public float _interval2 = 2; // 回転間隔(ランダムに設定される)
    [SerializeField] public float _interval3 = 1; // 回転間隔(ランダムに設定される)
    private float _timer;
    public bool _waitTime;

    public virtual void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _interval1 && _versions == 0)
        {
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y + _rotateAngleY,
                transform.rotation.eulerAngles.z
            );

            Debug.Log(_interval1);
            _isBack = true; // 背を向いた瞬間
            _timer = 0f;
            _interval1 = Random.Range(min1_Interval, max1_Interval);
            timerStart();
        }

        if (_timer >= _interval2 && _versions == 1)
        {
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y + _rotateAngleY,
                transform.rotation.eulerAngles.z
            );

            Debug.Log(_interval2);
            _isBack = true;
            _timer = 0f;
            _interval2 = Random.Range(min2_Interval, max2_Interval);
            timerStart();
        }

        if (_timer >= _interval3 && _versions == 2)
        {
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y + _rotateAngleY,
                transform.rotation.eulerAngles.z
            );

            Debug.Log(_interval3);
            _isBack = true;
            _timer = 0f;
            _interval3 = Random.Range(min3_Interval, max3_Interval);
            timerStart();
        }
    }
    
    public virtual void timerStart()
    {
        _waitTime = true;
    }
}
