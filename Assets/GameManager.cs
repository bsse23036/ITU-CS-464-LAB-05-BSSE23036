using UnityEngine;
using TMPro; // Required if using TextMeshPro for your score UI

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText; // Drag your Score Text UI here in the Inspector
    
    public GameObject redCoinPrefab;  // Drag your Red Coin Prefab here in the Inspector
    
    private int randomSpawnScore;
    private bool redCoinSpawned = false;

    void Start()
    {
        // Task 1: Pick a random score threshold between 1 and 10
        randomSpawnScore = Random.Range(1, 11);
        Debug.Log($"Red Coin will spawn when score reaches: {randomSpawnScore}");
        
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        // Check if we hit the random threshold and haven't spawned the red coin yet
        if (score >= randomSpawnScore && !redCoinSpawned)
        {
            SpawnRedCoin();
        }
    }

    void SpawnRedCoin()
    {
        redCoinSpawned = true;

        // Choose a random position on your floor plane bounds (adjust X and Z ranges to fit your arena)
        Vector3 spawnPosition = new Vector3(Random.Range(-4f, 4f), 0.5f, Random.Range(-4f, 4f));

        if (redCoinPrefab != null)
        {
            Instantiate(redCoinPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Red Coin Spawned in the arena!");
        }
        else
        {
            Debug.LogWarning("Please assign the Red Coin Prefab in the GameManager Inspector!");
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}