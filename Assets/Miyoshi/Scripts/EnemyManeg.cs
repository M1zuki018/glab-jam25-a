using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManeg : MonoBehaviour
{
    // 自分自身
    [SerializeField] private Transform _self;

    // ターゲット
    [SerializeField] private Transform _target;

    // 障害物として判定するレイヤー
    [SerializeField] private LayerMask _obstacleLayer;

    // 視野角（度数法）
    [SerializeField] private float _sightAngle;

    // 視界の最大距離
    [SerializeField] private float _maxDistance = float.PositiveInfinity;

    #region Logic

    /// <summary>
    /// ターゲットが見えているかどうか
    /// </summary>
    public bool IsVisible()
    {
        // 自身の位置
        var selfPos = _self.position;
        // ターゲットの位置
        var targetPos = _target.position;

        // 自身の向き
        var selfDir = _self.forward;

        // ターゲットまでの向きと距離
        var targetDir = targetPos - selfPos;
        var targetDistance = targetDir.magnitude;

        // 視野角判定
        var cosHalf = Mathf.Cos(_sightAngle / 2 * Mathf.Deg2Rad);
        var innerProduct = Vector3.Dot(selfDir, targetDir.normalized);
        if (!(innerProduct > cosHalf && targetDistance < _maxDistance))
        {
            return false;
        }

        // 障害物判定（Raycast）
        if (Physics.Raycast(selfPos, targetDir.normalized, out RaycastHit hit, _maxDistance, _obstacleLayer))
        {
            // Rayがターゲット以外に当たったら見えない
            if (hit.transform != _target)
            {
                return false;
            }
        }

        return true;
    }

    #endregion

    #region Debug

    private void OnGUI()
    {
        var isVisible = IsVisible();
        GUI.Box(new Rect(20, 20, 180, 23), $"isVisible = {isVisible}");
    }

    #endregion
}
