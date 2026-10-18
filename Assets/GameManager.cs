using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int normalScore = 0;
    public int maxNormalCoins = 10;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bonusText;
    public TextMeshProUGUI gameOverText;
    
    public GameObject coinPrefab;     
    public GameObject redCoinPrefab;  
    public GameObject blueCoinPrefab; 
    
    public float spawnRangeX = 7.0f;  
    public float spawnRangeZ = 7.0f;  

    private int randomRedSpawnScore;
    private bool redCoinSpawned = false;
    
    private int bonusCoinsCollected = 0;
    private int totalBonusCoins = 50;
    private bool isGameOver = false;

    void Start()
    {
        UpdateScoreText();
        
        if (bonusText != null) bonusText.gameObject.SetActive(false);
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);

        // Pick a random threshold between 1 and 9 for the Red Coin to spawn in between
        randomRedSpawnScore = Random.Range(1, maxNormalCoins);
        Debug.Log($"Red Coin will spawn when normal score reaches: {randomRedSpawnScore}");

        // Spawn exactly 10 normal coins at the start
        for (int i = 0; i < maxNormalCoins; i++)
        {
            SpawnCoin();
        }
    }

    public void AddNormalCoinScore()
    {
        if (isGameOver) return;

        normalScore++;
        UpdateScoreText();

        // Check if it's time to spawn the Red Coin randomly in between
        if (!redCoinSpawned && normalScore == randomRedSpawnScore)
        {
            SpawnRedCoin();
        }

        // Check if game is over (all 10 normal coins collected)
        if (normalScore >= maxNormalCoins)
        {
            TriggerGameOver();
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
                Random.Range(-3f, 3f), 
                0.5f, 
                Random.Range(-3f, 3f)
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

    void TriggerGameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over! All 10 normal coins collected.");
        
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "You Win! All 10 Coins Collected!";
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null) scoreText.text = "Score: " + normalScore + " / " + maxNormalCoins;
    }

    void UpdateBonusText()
    {
        if (bonusText != null) bonusText.text = "Bonus Coins: " + bonusCoinsCollected + " / " + totalBonusCoins;
    }
}