using UnityEngine;

public class ColliderFollower : MonoBehaviour
{
    [SerializeField] private Transform targetTransform; // 敵本体など

    void Update()
    {
        transform.rotation = targetTransform.rotation;
    }
}
