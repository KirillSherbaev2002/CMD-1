using UnityEngine;

public class SkinAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 finalPosition;
    private Vector3 initialPosition;
    private Vector3 initialRotation;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.localEulerAngles;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, finalPosition, 0.5f);
    }

    void OnDisable()
    {
        transform.position = initialPosition;
        transform.localEulerAngles = initialRotation;
    }
}
