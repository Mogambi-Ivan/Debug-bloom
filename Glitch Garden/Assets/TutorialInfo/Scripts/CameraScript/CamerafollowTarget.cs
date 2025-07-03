using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // Banana Man
    public Vector3 offset = new Vector3(0f, 5f, -7f);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
