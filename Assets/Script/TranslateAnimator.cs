using UnityEngine;

public class TranslateAnimator : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction = Vector3.right;
    void Update()
    {
        transform.Translate(direction * (speed * Time.deltaTime));
    }
}