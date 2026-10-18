using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bonusText;
    
    public GameObject coinPrefab;     
    public GameObject redCoinPrefab;  
    public GameObject blueCoinPrefab; 
    
    public int initialCoinCount = 8;
    public float spawnRangeX = 7.0f;  // Keep slightly inside your arena walls
    public float spawnRangeZ = 7.0f;  

    private int randomSpawnScore;
    private bool redCoinSpawned = false;
    
    private int bonusCoinsCollected = 0;
    private int totalBonusCoins = 50;

    void Start()
    {
        UpdateScoreText();
        
        if (bonusText != null) bonusText.gameObject.SetActive(false);

        // Pick a random score threshold between 1 and 10 for the Red Coin
        randomSpawnScore = Random.Range(1, 11);
        Debug.Log($"Red Coin will spawn when score reaches: {randomSpawnScore}");

        // Spawn initial normal coins
        for (int i = 0; i < initialCoinCount; i++)
        {
            SpawnCoin();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        // Always spawn a replacement normal coin when a normal coin is collected
        SpawnCoin();

        // Check if we hit the random threshold for the Red Coin (and hasn't spawned yet)
        if (!redCoinSpawned && score >= randomSpawnScore)
        {
            SpawnRedCoin();
        }
    }

    void SpawnCoin()
    {
        if (coinPrefab != null)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-spawnRangeX, spawnRangeX), 
                0.5f, 
                Random.Range(-spawnRangeZ, spawnRangeZ)
            );
            Instantiate(coinPrefab, randomPos, Quaternion.identity);
        }
    }

    void SpawnRedCoin()
    {
        redCoinSpawned = true;
        if (redCoinPrefab != null)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-4f, 4f), 
                0.5f, 
                Random.Range(-4f, 4f)
            );
            Instantiate(redCoinPrefab, randomPos, Quaternion.identity);
            Debug.Log("Red Coin Spawned in the arena!");
        }
    }

    public void TriggerBonusRound(Vector3 playerPosition)
    {
        bonusCoinsCollected = 0;

        if (bonusText != null)
        {
            bonusText.gameObject.SetActive(true);
            UpdateBonusText();
        }

        // Spawn 50 Blue Coins in a circular radius around the player
        float radius = 5.0f;
        for (int i = 0; i < totalBonusCoins; i++)
        {
            float angle = i * Mathf.PI * 2f / totalBonusCoins;
            Vector3 spawnOffset = new Vector3(Mathf.Cos(angle) * radius, 0.5f, Mathf.Sin(angle) * radius);
            Vector3 spawnPos = playerPosition + spawnOffset;

            if (blueCoinPrefab != null)
            {
                Instantiate(blueCoinPrefab, spawnPos, Quaternion.identity);
            }
        }
        Debug.Log("50 Blue Coins spawned in circular formation!");
    }

    public void AddBonusCoin()
    {
        bonusCoinsCollected++;
        UpdateBonusText();

        if (bonusCoinsCollected >= totalBonusCoins)
        {
            Debug.Log("Bonus Round Completed!");
            if (bonusText != null) bonusText.text = "Bonus Completed!";
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null) scoreText.text = "Score: " + score.ToString();
    }

    void UpdateBonusText()
    {
        if (bonusText != null) bonusText.text = "Bonus Coins: " + bonusCoinsCollected + "/" + totalBonusCoins;
    }
}