using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Global singleton instance
    public static GameManager instance;

    // ================= UI =================

    // Text that shows the winner
    public TextMeshProUGUI winText;

    // Text that shows the timer
    public TextMeshProUGUI timerText;

    // Panel displayed when someone wins
    public GameObject winPanel;

    // ================= TREASURE SPAWN =================

    // List of spawn points for the treasure
    public Transform[] spawnPoints;

    // ================= TIMER =================

    // Prevents multiple winners
    private bool gameEnded = false;

    // Stores elapsed game time
    private float timer = 0f;

    void Awake()
    {
        // Assign singleton instance
        instance = this;

        Debug.Log("GameManager created");
    }

    void Start()
    {
        // Hide win panel at start
        if (winPanel != null)
            winPanel.SetActive(false);

        // Clear winner text
        if (winText != null)
            winText.text = "";
    }

    void Update()
    {
        // Stop timer if game ended
        if (gameEnded) return;

        // Increase timer every frame
        timer += Time.deltaTime;

        // Update timer UI
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Called when a player wins
    public void PlayerWins(string playerName)
    {
        // Prevent multiple calls
        if (gameEnded) return;

        gameEnded = true;

        Debug.Log("PLAYER WINS FUNCTION CALLED");

        // Show victory panel
        if (winPanel != null)
            winPanel.SetActive(true);

        // Calculate final time
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        // Display winner information
        if (winText != null)
        {
            winText.text = "YOU WIN\n" +
                           playerName +
                           "\nTime: " +
                           string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        // Restart game after delay
        StartCoroutine(RestartGame());
    }

    // Restart current scene
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Return random treasure spawn position
    public Vector3 GetRandomPosition()
    {
        // Check if spawn points exist
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return Vector3.zero;
        }

        // Choose random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);

        // Return selected position
        return spawnPoints[randomIndex].position;
    }
}