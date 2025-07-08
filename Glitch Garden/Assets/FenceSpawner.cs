using UnityEngine;

public class FenceSpawner : MonoBehaviour
{
    public GameObject fencePrefab;
    public int count = 10;
    public float spacing = 2f;

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = transform.position + transform.right * i * spacing;
            Instantiate(fencePrefab, pos, transform.rotation);
        }
    }
}
