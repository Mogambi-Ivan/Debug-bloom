using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // Banana Man
    public Vector3 offset = new Vector3(0f, 5f, -7f);
    public float smoothSpeed = 5f;
    public float rotationSmoothSpeed = 5f;

    void LateUpdate()
    {
        // Smooth follow position
        Vector3 desiredPosition = target.position + target.rotation * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Smooth follow rotation
        Quaternion desiredRotation = Quaternion.LookRotation(target.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
    }
}
