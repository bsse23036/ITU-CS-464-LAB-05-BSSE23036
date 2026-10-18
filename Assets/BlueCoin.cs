using UnityEngine;

public class BlueCoin : MonoBehaviour
{
    void Update()
    {
        // Continuous spinning animation
        transform.Rotate(new Vector3(20, 10, 30) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Update bonus progress in the GameManager
            GameManager gm = FindAnyObjectByType<GameManager>();
            if (gm != null)
            {
                gm.AddBonusCoin();
            }

            // Destroy the blue coin upon collection
            Destroy(gameObject);
        }
    }
}