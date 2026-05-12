using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Load the main gameplay scene
    public void Jugar()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Close the application
    public void Salir()
    {
        Debug.Log("Closing game");

        Application.Quit();
    }
}