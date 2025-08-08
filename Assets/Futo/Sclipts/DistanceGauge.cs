using UnityEngine;
using UnityEngine.UI;

public class DistanceGauge : MonoBehaviour
{
    [SerializeField] int _MaxCount = 7;
    [SerializeField] Image _distanceGauge;

    int _successCount = 0;
    float _magnification;
    Vector3 _maxGaugeScale;

    public int SuccessCount => _successCount;

    void Start()
    {
        _maxGaugeScale = _distanceGauge.transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            QteSuccess();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            QteFailure();
        }
    }
    public void QteSuccess()
    {
        _successCount++;
        if(_successCount <= _MaxCount)
        {
            _successCount = _MaxCount;
        }
        UIUpdate();
    }

    public void QteFailure()
    {
        _successCount--;
        if(_successCount <= 0)
        {
            _successCount = 0;
        }
        UIUpdate();
    }

    void UIUpdate()
    {
        _magnification = 1.0f * (_MaxCount - _successCount) / _MaxCount;
        _distanceGauge.transform.localScale = new Vector3(_magnification * _maxGaugeScale.x, _maxGaugeScale.y, _maxGaugeScale.z);
    }
}
