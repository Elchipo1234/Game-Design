using UnityEngine;

public class InfiniteGround : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;

    [Header("Ground Tiles")]
    public Transform[] groundTiles;
    public float groundWidth = 16f;

    void Update()
    {
        if (player == null) return;

        MoveGround();
    }

    void MoveGround()
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        // Find the leftmost and rightmost tiles
        Transform leftmost = groundTiles[0];
        Transform rightmost = groundTiles[0];

        foreach (Transform tile in groundTiles)
        {
            if (tile.position.x < leftmost.position.x)
                leftmost = tile;
            if (tile.position.x > rightmost.position.x)
                rightmost = tile;
        }

        foreach (Transform tile in groundTiles)
        {
            Vector3 pos = tile.position;

            // Moving right: tile went off left side
            if (rb.linearVelocity.x > 0 && pos.x + groundWidth / 2 < player.position.x - groundWidth)
            {
                pos.x = rightmost.position.x + groundWidth;
            }
            // Moving left: tile went off right side
            else if (rb.linearVelocity.x < 0 && pos.x - groundWidth / 2 > player.position.x + groundWidth)
            {
                pos.x = leftmost.position.x - groundWidth;
            }

            tile.position = pos;
        }
    }
}