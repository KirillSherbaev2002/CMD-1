using UnityEngine;

public class UICameraHandler : MonoBehaviour
{
    [SerializeField] private Transform _currentMount;
    [SerializeField] private float _cameraSpeed;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _currentMount.position, _cameraSpeed * Time.deltaTime); 
        transform.rotation = Quaternion.Slerp(transform.rotation, _currentMount.rotation, _cameraSpeed * Time.deltaTime);

    }

    public void setMount(Transform newMount)
    {
        _currentMount = newMount;
    }

}
