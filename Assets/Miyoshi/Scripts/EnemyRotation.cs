using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [Header("レベル１の時の最大最小")]
    [SerializeField] public float max1_Interval = 10;
    [SerializeField] public float min1_Interval = 7;

    [Header("レベル２の時の最大最小")]
    [SerializeField] public float max2_Interval = 5;
    [SerializeField] public float min2_Interval = 1;

    [Header("レベル３の時の最大最小")]
    [SerializeField] public float max3_Interval = 5;
    [SerializeField] public float min3_Interval = 1;

    [Header("バージョン管理０，１，２")]
    [SerializeField] public int _versions = 0;

    [Header("元の方向に戻る時間")]
    [SerializeField] public float _backInterval = 0.5f;

    [Header("回転角度")]
    [SerializeField] public float _rotateAngleY = 45f;
    protected bool _isBack = false;

    [Header("それぞれのレベル最初の一回目の時間")]
    [SerializeField] public float _interval1 = 3;
    [SerializeField] public float _interval2 = 2;
    [SerializeField] public float _interval3 = 1;

    private float _timer;
    public bool _waitTime;

    public virtual void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _interval1 && _versions == 0)
        {
            RotateEnemy();
            _interval1 = Random.Range(min1_Interval, max1_Interval);
            timerStart();
        }

        if (_timer >= _interval2 && _versions == 1)
        {
            RotateEnemy();
            _interval2 = Random.Range(min2_Interval, max2_Interval);
            timerStart();
        }

        if (_timer >= _interval3 && _versions == 2)
        {
            RotateEnemy();
            _interval3 = Random.Range(min3_Interval, max3_Interval);
            timerStart();
        }
    }
    
    public virtual void timerStart()
    {
        _waitTime = true;
    }
    private void RotateEnemy()
    {
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + _rotateAngleY,
            transform.rotation.eulerAngles.z
        );

        Debug.Log("Rotate: _versions = " + _versions);
        _isBack = true;
        _timer = 0f;
    }

    // LevelWall に接触したらレベルアップ
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LevelWall"))
        {
            if (_versions < 2) // 2 以上は上げない
            {
                _versions++;
                Debug.Log("Level Up! 現在のレベル: " + _versions);
            }
        }
    }
}
