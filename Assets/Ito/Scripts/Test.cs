using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Test : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioManager.Instance.PlayBGM(SoundDataUtility.KeyConfig.Se.Success1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            AudioManager.Instance.PlayBGM(SoundDataUtility.KeyConfig.Se.Success2);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.Instance.PlayBGM(SoundDataUtility.KeyConfig.Se.Success3);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            AudioManager.Instance.PlayBGM(SoundDataUtility.KeyConfig.Se.Success4);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            AudioManager.Instance.PlayBGM(SoundDataUtility.KeyConfig.Se.Success5);
        }
    }


}
