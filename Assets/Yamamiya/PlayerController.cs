using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] Transform startPos; 
    [SerializeField,Tooltip("ターゲットとの距離")] float distance;

    [Header("Player")]
    [SerializeField] Transform player;
    [SerializeField,Tooltip("速度")] float speed;
    [SerializeField,Tooltip("移動させるキー")] KeyCode moveKey;

    private void Start()
    {
        player = GetComponent<Transform>();
        startPos = player;
        distance = targetPos.position.z - player.position.z;
    }

    void Update()
    {
        if (Input.GetKey(moveKey))
        {
            MovePlayer();
        }
    }

    /// <summary>
    /// プレイヤーの移動
    /// </summary>
    public void MovePlayer()
    {
        player.position += Vector3.forward * speed * Time.deltaTime;
        UpdateDistance();
    }

    private void UpdateDistance()
    {
        Debug.Log(GetDistance());
    }

    /// <summary>
    /// 距離の割合を取得
    /// </summary>
    /// <returns></returns>
    public float GetDistance()
    {
        return player.position.z / distance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            //リザルト表示
        }
    }
}