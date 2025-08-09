using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneBGM : MonoBehaviour
{

    void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "GameScene":
                AudioManager.Instance.PlayBGM(SoundDataUtility.KeyConfig.Bgm.InGame);
                break;
            case "TitleScene":
                AudioManager.Instance.PlayBGM(SoundDataUtility.KeyConfig.Bgm.InTitle);
                break;
            case "ResultScene":
                AudioManager.Instance.PlayBGM(SoundDataUtility.KeyConfig.Bgm.InResult);
                break;
        }
    }
}
