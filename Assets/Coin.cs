using UnityEngine;

public class Coin : MonoBehaviour
{
    void Update()
    {
        // Keeps the classic lecture rotation effect for the coin
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with it is the Player
        if (other.CompareTag("Player"))
        {
            // Add score via the GameManager
            FindAnyObjectByType<GameManager>()?.AddScore(1);

            // Destroy or deactivate the coin upon collection
            gameObject.SetActive(false);
        }
    }
}