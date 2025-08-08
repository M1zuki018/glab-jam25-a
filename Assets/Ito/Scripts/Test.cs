using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlayBGM(SoundDataUtility.KeyConfig.Bgm.InGame);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            AudioManager.Instance.PhaseBGM();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            AudioManager.Instance.RestartBGM();
        }
    }
}
