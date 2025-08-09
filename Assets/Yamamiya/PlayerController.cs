using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] DistanceGauge _distanceGauge;
    [SerializeField, Tooltip("ゴールゲームオブジェクト")] GameObject _targetPos;
    [SerializeField] Vector3 _startPos;
    [SerializeField] Quaternion _startRotation;
    [SerializeField, Tooltip("ターゲットとの距離")] float _distance;

    [Header("Player")]
    Transform _player;
    [SerializeField, Tooltip("速度")] float _speed;
    [SerializeField, Tooltip("移動させるキー")] KeyCode _moveKey;
    GameObject _nearShelter;
    [SerializeField, Tooltip("継続時間")] float _duration;
    [SerializeField, Tooltip("ゴール用")] float _goalDuration;
    /// <summary>
    /// プレイヤーが隠れてるかどうか
    /// </summary>
    bool _isHidden = false;
    /// <summary>
    /// ゴールしたかどうか
    /// </summary>
    bool _isGoal = false;
    /// <summary>
    /// スタンを食らっているかどうか
    /// </summary>
    bool _isStun = false;

    [SerializeField]bool _isStartMove = false;

    [SerializeField] Animator _animator;

    private void Start()
    {
        _player = GetComponent<Transform>();
        _startPos = _player.transform.position;
        _startRotation = _player.transform.rotation;
        _distance = _targetPos.transform.position.z - _player.position.z;
        _isStartMove = true;
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
            AudioManager.Instance.StopBGM();
            _isStartMove = true;
        }

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    HiddenShelter();
        //}

        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    BackOriginalPosX();
        //}

        //if(Input.GetMouseButtonDown(0))
        //{
        //    StartCoroutine(StunPlayer(1));
        //}
    }

    /// <summary>
    /// プレイヤーの移動
    /// </summary>
    public void MovePlayer()
    {
        if (_isHidden || _isGoal || _isStun)
        {
            return;
        }
        if (_isStartMove)
        {
            AudioManager.Instance.PlayBGM(SoundDataUtility.KeyConfig.Bgm.foot);
            _isStartMove = false;
        }
        _animator.SetBool("IsMove", true);
        _player.position += Vector3.forward * _speed * Time.deltaTime;
        _distanceGauge.UIUpdate(GetDistance());
        ProceedTarget();
    }

    /// <summary>
    /// ゴールしたあと敵のところまで進む
    /// </summary>
    private void ProceedTarget()
    {
        if (GetDistance() >= 1)
        {
            _isGoal = true;
            var enemyPos = _targetPos.transform.parent;
            _player.transform.LookAt(enemyPos);
            _animator.SetBool("IsGoal", _isGoal);
            
            _player.DOMove(enemyPos.position, _goalDuration).OnComplete (() =>
            {
                GameManager.Instance.ChangeScene("ResultScene",Color.white);
            });
        }
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
        if (_nearShelter != null)
        {
            _isHidden = true;
            _animator.SetBool("IsHidden", _isHidden);
            _player.transform.LookAt(_nearShelter.transform);
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

    /// <summary>
    /// 元のx座標に戻る
    /// </summary>
    public void BackOriginalPosX()
    {
        _player.transform.rotation = _startRotation;
        _player.DOMoveX(_startPos.x, _duration);
        _isHidden = false;
        _animator.SetBool("IsHidden",_isHidden );
    }

    /// <summary>
    /// プレイヤーを動かせなくなる
    /// </summary>
    /// <param name="stunTime"></param>
    public IEnumerator StunPlayer(float stunTime)
    {
        _isStun = true;
        Debug.Log("スタン開始");
        yield return new WaitForSeconds(stunTime);
        _isStun = false;
        Debug.Log("スタン解除");
    }
}