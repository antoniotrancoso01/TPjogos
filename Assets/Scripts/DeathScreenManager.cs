using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenManager : MonoBehaviour
{
    public GameObject deathScreenCanvas; // Arraste o Canvas da tela de morte aqui no Inspector

    public void ShowDeathScreen()
    {
        deathScreenCanvas.SetActive(true);
        Time.timeScale = 0f; // Pausa o jogo
        Cursor.lockState = CursorLockMode.None; // Desbloqueia o cursor
        Cursor.visible = true; // Torna o cursor visível

        // Desativar controles do jogador
        var playerController = FindObjectOfType<MovimentarJogador>(); // Substitua pelo nome real do script de controle
        if (playerController != null)
        {
            playerController.enabled = false;
        }
    }


    public void Retry()
    {
        Time.timeScale = 1f; // Retoma o tempo do jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Reativar scripts de controle do jogador e da câmera, caso necessário
        var playerController = FindObjectOfType<MovimentarJogador>(); // Substitua pelo nome real do script de controle
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }



    public void MainMenu()
    {
        Time.timeScale = 1f; // Retoma o tempo do jogo
        SceneManager.LoadScene(0);
    }


    public void QuitGame()
    {
        Application.Quit(); // Fecha o jogo
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
