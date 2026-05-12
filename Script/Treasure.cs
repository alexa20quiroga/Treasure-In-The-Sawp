using UnityEngine;

public class Treasure : MonoBehaviour
{
    // Sound played when treasure is collected
    public AudioClip collectSound;

    void Start()
    {
        // Move treasure to random spawn point
        if (GameManager.instance != null)
        {
            transform.position = GameManager.instance.GetRandomPosition();
        }
        else
        {
            Debug.LogError("GameManager NOT found in scene");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Detect players touching the treasure
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            // Play collection sound
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(
                    collectSound,
                    Camera.main.transform.position,
                    0.5f
                );
            }

            // Notify GameManager about winner
            if (GameManager.instance != null)
            {
                GameManager.instance.PlayerWins(other.tag);
            }

            // Disable collider to avoid repeated triggers
            GetComponent<Collider>().enabled = false;
        }
    }
}