using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] DistanceGauge _distanceGauge;
    [SerializeField] Transform _targetPos;
    [SerializeField] Vector3 _startPos; 
    [SerializeField,Tooltip("ターゲットとの距離")] float _distance;

    [Header("Player")]
    Transform _player;
    [SerializeField,Tooltip("速度")] float _speed;
    [SerializeField,Tooltip("移動させるキー")] KeyCode _moveKey;
    GameObject _nearShelter;
    [SerializeField,Tooltip("継続時間")] float _duration;

    [SerializeField] Animator _animator;

    private void Start()
    {
        _player = GetComponent<Transform>();
        _startPos = _player.transform.position;
        _distance = _targetPos.position.z - _player.position.z;
    }

    void Update()
    {
        if (Input.GetKey(_moveKey))
        {
            MovePlayer();
        }
        else
        {
            _animator.SetBool("IsMove", false);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            HiddenShelter();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            BackOriginalPosX();
        }
    }

    /// <summary>
    /// プレイヤーの移動
    /// </summary>
    public void MovePlayer()
    {
        _animator.SetBool("IsMove", true);
        _player.position += Vector3.forward * _speed * Time.deltaTime;
        _distanceGauge.UIUpdate(GetDistance());
    }

    /// <summary>
    /// 距離の割合を取得
    /// </summary>
    /// <returns></returns>
    private float GetDistance()
    {
        return _player.position.z / _distance;
    }

    /// <summary>
    /// 遮蔽物に隠れる
    /// </summary>
    public void HiddenShelter()
    {
        _nearShelter = FindSearchNearShelter();
        if(_nearShelter != null)
        {
            _player.DOMove(_nearShelter.transform.position - Vector3.forward, _duration);
        }
    }

    /// <summary>
    /// 一番近い遮蔽物を取得する
    /// </summary>
    /// <returns></returns>
    private GameObject FindSearchNearShelter()
    {
        GameObject[] shelters;
        shelters = GameObject.FindGameObjectsWithTag("Shelter"); 
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = _player.position;
        Vector3 foward = _player.forward;
        foreach (GameObject shelter in shelters)
        {
            Vector3 directionTarget = shelter.transform.position - position;
            float curDistance = directionTarget.sqrMagnitude;
            float dotProduct = Vector3.Dot(foward, directionTarget);
            if (curDistance < distance && dotProduct > 0)
            {
                closest = shelter;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void BackOriginalPosX()
    {
        _player.DOMoveX(_startPos.x, _duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            //リザルト表示
        }
    }
}