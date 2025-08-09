using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] string _changeSceneName = string.Empty;
    [SerializeField] GameManager _gameManager;
    [SerializeField] Color _color;
    private void Start()
    {
        _gameManager = GameManager.FindAnyObjectByType<GameManager>();
    }
    public void OnClick()
    {
        _gameManager.ChangeScene(_changeSceneName,_color);
    }
}
