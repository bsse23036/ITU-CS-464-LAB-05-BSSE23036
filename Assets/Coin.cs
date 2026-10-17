using UnityEngine;

public class Coin : MonoBehaviour
{
    void Update()
    {
        // Make the coin spin continuously for visual flair
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with it is the Player
        if (other.CompareTag("Player"))
        {
            // Optional: Call a scoring function if you have a game manager set up
            // FindObjectOfType<GameManager>()?.AddScore(1);

            // Destroy the coin object
            Destroy(gameObject);
        }
    }
}