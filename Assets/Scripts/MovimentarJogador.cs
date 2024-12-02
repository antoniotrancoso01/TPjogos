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
         public float velocidadeCorrida = 2f; // Velocidade ao correr

    private Vector3 vetorMovimento = new Vector3();

    public float efeitoGravidade = 1f;

    public float forcaSalto = 1f;

    public GameObject pauseMenuUI;

    public static bool gameIsPaused = false;

    public AudioMixer audioMixer;

    public Slider volumeSlider;

    public Camera playerCamera; // Referência à câmara do jogador

    public CharacterController characterController; // Referência ao colisor do personagem

    public float crouchHeight = 0.5f; // Altura do CharacterController ao agachar
    public float crouchCameraOffset = 0.75f; // Ajuste no eixo Y ao agachar

    private float originalHeight; // Altura original do CharacterController
    private float originalCameraY; // Posição original da câmara
    private Vector3 originalCenter; // Centro original do CharacterController

    private bool isCrouching = false; // Estado de agachamento

    private CharacterController controladorPersonagem;
    // Variáveis para o dash
    public float dashDistance = 5f; // Distância do dash
    public float dashSpeed = 15f; // Velocidade do dash
    private bool isDashing = false; // Estado de dash
    private Vector3 dashDirection; // Direção do dash

    void Start()
    {
        controladorPersonagem = gameObject.GetComponent<CharacterController>();

        // Carregar sensibilidade e volume guardados pelo "PlayerPrefs"
        sensibilidadeX = PlayerPrefs.GetFloat("MouseSensitivity", sensibilidadeX);
        sensibilidadeY = sensibilidadeX; // Usamos o mesmo valor para ambas as direções
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0f);

        // Inicializa os valores originais
        originalHeight = characterController.height;
        originalCenter = characterController.center;
        originalCameraY = playerCamera.transform.localPosition.y; // Guarda a posição inicial da câmara
    }

    void Update()
    {
        // Verificar se o jogador clicou ESC para pausar/despausar o jogo
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

        // Se o jogo estiver pausado, não lê os movimentos
        if (gameIsPaused) return;

        // Atualizar a sensibilidade em tempo real, caso tenha sido alterada
        sensibilidadeX = PlayerPrefs.GetFloat("MouseSensitivity", sensibilidadeX);
        sensibilidadeY = sensibilidadeX;

        // Rotação da câmera com o rato
        ratoRotacaoX += Input.GetAxis("Mouse X") * sensibilidadeX;
        ratoRotacaoY += Input.GetAxis("Mouse Y") * sensibilidadeY;

        gameObject.transform.rotation = Quaternion.Euler(0, ratoRotacaoX, 0);
        ratoRotacaoY = Mathf.Clamp(ratoRotacaoY, -90, 90);
        objectoCamara.transform.localRotation = Quaternion.Euler(-1 * ratoRotacaoY, 0, 0);


        // Determinar a velocidade de movimento (normal ou corrida)
        float velocidadeAtual = Input.GetKey(KeyCode.LeftShift) ? velocidadeCorrida : velocidadeMovimento;

        // Movimentação do jogador
        if (Input.GetKey(KeyCode.W))
        {
            vetorMovimento.z = velocidadeAtual;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vetorMovimento.z = -velocidadeAtual;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vetorMovimento.x = velocidadeAtual;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vetorMovimento.x = -velocidadeAtual;
        }
    
        // Dash
        if (Input.GetKeyDown(KeyCode.Q) && !isDashing)
        {
            StartDash();
        }
        // Agachamento (Crouch)
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                Levantar(); // Volta ao estado normal
            }
            else
            {
                Agachar(); // Agacha o personagem
            }
        }

        // Aplica movimentação e gravidade
        vetorMovimento = transform.TransformDirection(vetorMovimento);

        if (controladorPersonagem.isGrounded)
        {
            vetorMovimento.y = -1f; // Para manter o jogador preso ao chão
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vetorMovimento.y = forcaSalto;
            }
        }
        else
        {
            vetorMovimento.y += Physics.gravity.y * efeitoGravidade * Time.deltaTime;
        }

        controladorPersonagem.Move(vetorMovimento * Time.deltaTime);

        // Resetar os movimentos no eixo X e Z após aplicação
        vetorMovimento.x = 0;
        vetorMovimento.z = 0;
    }

    void Agachar()
    {
        isCrouching = true;

        // Define altura reduzida e ajusta o centro
        characterController.height = crouchHeight;
        characterController.center = new Vector3(originalCenter.x, crouchHeight / 2, originalCenter.z);

        // Ajusta a câmera para a nova posição
        AdjustCameraY(-crouchCameraOffset);
    }

    void Levantar()
    {
        isCrouching = false;

        // Volta à altura original e restaura o centro
        characterController.height = originalHeight;
        characterController.center = originalCenter;

        // Retorna a câmera para a posição inicial
        AdjustCameraY(crouchCameraOffset);
    }

    void AdjustCameraY(float offset)
    {
        Vector3 cameraPosition = playerCamera.transform.localPosition;
        cameraPosition.y = originalCameraY + offset; // Ajusta a posição vertical da câmera
        playerCamera.transform.localPosition = cameraPosition;
    }

    // Método para voltar ao jogo
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked; // Trancar o rato
        Cursor.visible = false; // Esconder o rato
    }

    // Método para pausar o jogo
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None; // Libertar o rato
        Cursor.visible = true; // Tornar o rato visível
    }
    void StartDash()
    {
        isDashing = true;
        dashDirection = transform.forward; // Define a direção do dash
        StartCoroutine(PerformDash());
    }

    System.Collections.IEnumerator PerformDash()
    {
        float distanceCovered = 0f;

        while (distanceCovered < dashDistance)
        {
            float dashStep = dashSpeed * Time.deltaTime;
            distanceCovered += dashStep;

            Vector3 dashMove = dashDirection * dashStep;
            controladorPersonagem.Move(dashMove);

            yield return null; // Espera pelo próximo frame
        }

        isDashing = false; // Termina o dash
    }

}
