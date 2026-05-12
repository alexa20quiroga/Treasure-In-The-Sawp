using UnityEngine;
using UnityEngine.SceneManagement;

public class OpcionesMenu : MonoBehaviour
{
    // Reference to the options panel
    public GameObject panelOpciones;

    // Open options menu and pause game
    public void AbrirOpciones()
    {
        // Show options panel
        panelOpciones.SetActive(true);

        // Pause game time
        Time.timeScale = 0f;
    }

    // Close options menu and resume game
    public void CerrarOpciones()
    {
        // Hide options panel
        panelOpciones.SetActive(false);

        // Resume game time
        Time.timeScale = 1f;
    }

    // Return to main menu scene
    public void VolverAlMenu()
    {
        // Make sure game time returns to normal
        Time.timeScale = 1f;

        // Load menu scene
        SceneManager.LoadScene("Menu");
    }

    // Close application
    public void Salir()
    {
        Application.Quit();
    }
}