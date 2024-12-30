using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false; //Controla o estado de pausa do jogo
    public GameObject pauseMenuUI;


    void Update()
    {
        //Verifica se clicou no ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume(); //Se já estiver pausado, volta ao jogo
            }
            else
            {
                Pause(); //Caso contrário, pausa
            }
        }
    }

    //Método para voltar ao jogo
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked; //Trancar o rato 
        Cursor.visible = false; //Esconder o rato
    }

    //Método para pausar o jogo
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None; //Libertar o rato
        Cursor.visible = true; //Tornar o rato visível
    }

    //Método para carregar a cena do menu principal
    public void LoadMenu()
    {
        Time.timeScale = 1f; //Volta ao tempo normal
        SceneManager.LoadScene(0);
    }

    //Método para sair do jogo
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
