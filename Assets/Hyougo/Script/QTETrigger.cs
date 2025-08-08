using UnityEngine;

public class QTETrigger : MonoBehaviour
{
    [SerializeField] private QTEController qteController;
    [SerializeField] private float qteTimeLimit = 3f;

    private bool hasActivated = false; // 一度だけ実行する用

    private void OnTriggerEnter(Collider other)
    {
        if (hasActivated) return; // 連続発動防止

        if (other.CompareTag("Player"))
        {
            hasActivated = true;
            qteController.StartQTE(qteTimeLimit);
        }
    }
}
