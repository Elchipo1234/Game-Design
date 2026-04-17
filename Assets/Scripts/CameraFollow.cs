using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;  // Usually (0,0,-10) for 2D

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 newPos = player.position + offset;
        newPos.y = transform.position.y; // Keep camera Y stable if you want
        newPos.z = offset.z; // Ensure camera stays at the correct Z
        transform.position = newPos;
    }
}