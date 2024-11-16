using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MovimentarJogador : MonoBehaviour
{
    public float sensibilidadeX = 5f;
    public float sensibilidadeY = 5f;

    public GameObject objectoCamara;

    private float ratoRotacaoX = 0;
    private float ratoRotacaoY = 0;

    public float velocidadeMovimento = 1f;

    private Vector3 vetorMovimento = new Vector3();

    private CharacterController controladorPersonagem;

    public float efeitoGravidade = 1f;

    public float forcaSalto = 1f;

    public GameObject pauseMenuUI;
    
    public static bool gameIsPaused = false;

    public AudioMixer audioMixer;

    public Slider volumeSlider;


    void Start()
    {
        controladorPersonagem = gameObject.GetComponent<CharacterController>();

        //Carregar sensibilidade guardada pelo "PlayerPrefs"
        sensibilidadeX = PlayerPrefs.GetFloat("MouseSensitivity", sensibilidadeX);
        sensibilidadeY = sensibilidadeX; // Usamos o mesmo valor para ambas as direções

        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0f);

    }

    void Update()
    {
        //Verificar se o jogador clicou ESC para pausar/despausar o jogo
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        //Se o jogo estiver pausado, não lê os movimentos
        if (gameIsPaused) return;


        //Atualizar a sensibilidade em tempo real, caso tenha sido alterada
        sensibilidadeX = PlayerPrefs.GetFloat("MouseSensitivity", sensibilidadeX);
        sensibilidadeY = sensibilidadeX;


        ratoRotacaoX += Input.GetAxis("Mouse X") * sensibilidadeX;
        ratoRotacaoY += Input.GetAxis("Mouse Y") * sensibilidadeY;

        gameObject.transform.rotation = Quaternion.Euler(0, ratoRotacaoX, 0);
        ratoRotacaoY = Mathf.Clamp(ratoRotacaoY, -90, 90);
        objectoCamara.transform.localRotation = Quaternion.Euler(-1 * ratoRotacaoY, 0, 0);


        if (Input.GetKey(KeyCode.W))
        {
            vetorMovimento.z = velocidadeMovimento;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vetorMovimento.z = -1 * velocidadeMovimento;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vetorMovimento.x = velocidadeMovimento;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vetorMovimento.x = -1 * velocidadeMovimento;
        }

        vetorMovimento = gameObject.transform.forward * vetorMovimento.z +
                         gameObject.transform.right * vetorMovimento.x +
                         gameObject.transform.up * vetorMovimento.y;

        if (controladorPersonagem.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vetorMovimento.y = forcaSalto;
            }
        }
        else
        {
            vetorMovimento += Physics.gravity * efeitoGravidade * Time.deltaTime;
        }

        controladorPersonagem.Move(vetorMovimento * Time.deltaTime);

        vetorMovimento.x = 0;
        vetorMovimento.z = 0;
    }

    //Método para voltar ao jogo
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked; //Trancar o rato 
        Cursor.visible = false; //Esconder o rato
    }

    //Método para pausar o jogo
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None; //Libertar o rato
        Cursor.visible = true; //Tornar o rato visível
    }

}