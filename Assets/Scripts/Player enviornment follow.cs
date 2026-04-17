using UnityEngine;
using ScriptableObject = UnityEngine.ScriptableObject;

public class EnvironmentFollow : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Parallax Layers")]
    public Transform[] farBackgroundTiles;
    public float farWidth = 19.27f;
    public float farParallax = 0.2f;

    public Transform[] midBackgroundTiles;
    public float midWidth = 16.06f;
    public float midParallax = 0.5f;

    void Update()
    {
        if (player == null) return;

        MoveLayer(farBackgroundTiles, farWidth, farParallax);
        MoveLayer(midBackgroundTiles, midWidth, midParallax);
    }

    void MoveLayer(Transform[] tiles, float width, float parallax)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb == null || tiles.Length == 0) return;

        // find leftmost & rightmost tile in THIS LAYER ONLY
        Transform leftmost = tiles[0];
        Transform rightmost = tiles[0];

        foreach (Transform t in tiles)
        {
            if (t.position.x < leftmost.position.x)
                leftmost = t;

            if (t.position.x > rightmost.position.x)
                rightmost = t;
        }

        foreach (Transform tile in tiles)
        {
            Vector3 pos = tile.position;

            // move tile
            pos.x -= rb.linearVelocity.x * parallax * Time.deltaTime;

            // wrap correctly
            if (rb.linearVelocity.x > 0 && pos.x + width < player.position.x - width)
            {
                pos.x = rightmost.position.x + width;
            }
            else if (rb.linearVelocity.x < 0 && pos.x - width > player.position.x + width)
            {
                pos.x = leftmost.position.x - width;
            }

            tile.position = pos;
        }
    }
}