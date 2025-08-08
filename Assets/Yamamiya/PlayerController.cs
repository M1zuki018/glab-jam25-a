using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField,Tooltip("ターゲットとの距離")] float distance;

    [Header("Player")]
    [SerializeField] Transform player;
    [SerializeField,Tooltip("速度")] float speed;
    [SerializeField,Tooltip("移動させるキー")] KeyCode moveKey;

    private void Start()
    {
        player = GetComponent<Transform>();
        UpdateDistance();
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
        distance = targetPos.position.z - player.position.z;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            //リザルト表示
        }
    }
}