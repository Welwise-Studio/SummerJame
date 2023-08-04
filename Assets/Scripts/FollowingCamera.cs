using UnityEngine;
public class FollowingCamera : MonoBehaviour
{
    #region Editor Fields
    [Header("Object for following")]
    [SerializeField] private Transform _target;

    [Header("Camera propertys")]
    [SerializeField] private float _returnSpeed;
    [SerializeField] private float _height;
    [SerializeField] private float _rearDistance;
    #endregion

    private Vector3 currentVector;

    void Start()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y + _height, _target.position.z - _rearDistance);
        transform.rotation = Quaternion.LookRotation(_target.position - transform.position);
    }
    void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        currentVector = new Vector3(_target.position.x, _target.position.y + _height, _target.position.z - _rearDistance);
        transform.position = Vector3.Lerp(transform.position, currentVector, _returnSpeed * Time.deltaTime);
    }
}
