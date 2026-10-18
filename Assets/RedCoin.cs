using UnityEngine;

public class RedCoin : MonoBehaviour
{
    void Update()
    {
        // Continuous spinning animation
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify GameManager to start the 50-blue-coin circular spawn
            GameManager gm = FindAnyObjectByType<GameManager>();
            if (gm != null)
            {
                gm.TriggerBonusRound(other.transform.position);
            }

            // Destroy the red coin upon collection
            Destroy(gameObject);
        }
    }
}