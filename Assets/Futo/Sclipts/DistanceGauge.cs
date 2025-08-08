using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DistanceGauge : MonoBehaviour
{
    [SerializeField] Image _distanceGauge;
    [SerializeField] PlayerController _player;

    [SerializeField] private float _duration = 1;

    float _magnification;

    void Start()
    {
        _distanceGauge.fillAmount = 0f;
    }

    private void Update()
    {
        UIUpdate();
    }
    
    void UIUpdate()
    {
        _magnification = _player.GetDistance();
        DOTween.To(
            () => _distanceGauge.fillAmount,
            amount => _distanceGauge.fillAmount = amount,
            _magnification,
            _duration);

    }
}
