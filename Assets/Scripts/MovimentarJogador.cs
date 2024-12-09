using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MovimentarJogador : MonoBehaviour
{
    // Configura��es gerais
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
    public Camera playerCamera; // Refer�ncia � c�mara do jogador
    public CharacterController characterController; // Refer�ncia ao colisor do personagem

    // Configura��es de postura
    public float crouchHeight = 0.5f; // Altura ao agachar
    public float crouchCameraOffset = 0.75f; // Offset da c�mera ao agachar
    public float proneHeight = 0.25f; // Altura ao deitar
    public float proneCameraOffset = 1.25f; // Offset da c�mera ao deitar

    private float originalHeight; // Altura original
    private float originalCameraY; // Posi��o inicial da c�mera
    private Vector3 originalCenter; // Centro original do CharacterController

    private bool isCrouching = false; // Estado de agachamento
    private bool isProne = false; // Estado de deitado
    private CharacterController controladorPersonagem;
    // Vari�veis para o dash
    public float dashDistance = 5f; // Dist�ncia do dash
    public float dashSpeed = 15f; // Velocidade do dash
    private bool isDashing = false; // Estado de dash
    private Vector3 dashDirection; // Dire��o do dash

    void Start()
    {
        controladorPersonagem = gameObject.GetComponent<CharacterController>();

        // Carregar configura��es salvas
        sensibilidadeX = PlayerPrefs.GetFloat("MouseSensitivity", sensibilidadeX);
        sensibilidadeY = sensibilidadeX;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0f);

        // Configurar valores iniciais
        originalHeight = characterController.height;
        originalCenter = characterController.center;
        originalCameraY = playerCamera.transform.localPosition.y;
    }

    void Update()
    {
        // Verificar pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused) ResumeGame();
            else PauseGame();
        }

        if (gameIsPaused) return;

        // Atualizar sensibilidade
        sensibilidadeX = PlayerPrefs.GetFloat("MouseSensitivity", sensibilidadeX);
        sensibilidadeY = sensibilidadeX;

        // Rota��o da c�mera
        ratoRotacaoX += Input.GetAxis("Mouse X") * sensibilidadeX;
        ratoRotacaoY += Input.GetAxis("Mouse Y") * sensibilidadeY;
        transform.rotation = Quaternion.Euler(0, ratoRotacaoX, 0);
        ratoRotacaoY = Mathf.Clamp(ratoRotacaoY, -90, 90);
        objectoCamara.transform.localRotation = Quaternion.Euler(-1 * ratoRotacaoY, 0, 0);


        // Determinar a velocidade de movimento (normal ou corrida)
        float velocidadeAtual = Input.GetKey(KeyCode.LeftShift) ? velocidadeCorrida : velocidadeMovimento;

        // Movimenta��o do jogador
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

        // Agachar
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isProne) Levantar();
            else if (isCrouching) Levantar();
            else Agachar();
        }

        // Deitar
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isProne) Levantar();
            else Deitar();
        }

        // Aplicar gravidade
        vetorMovimento = transform.TransformDirection(vetorMovimento);
        if (controladorPersonagem.isGrounded)
        {
            vetorMovimento.y = -1f;
            if (Input.GetKeyDown(KeyCode.Space)) vetorMovimento.y = forcaSalto;
        }
        else vetorMovimento.y += Physics.gravity.y * efeitoGravidade * Time.deltaTime;

        // Movimentar
        controladorPersonagem.Move(vetorMovimento * Time.deltaTime);

        // Resetar o movimento
        vetorMovimento.x = 0;
        vetorMovimento.z = 0;
    }

    void Agachar()
    {
        if (isProne) return; // N�o pode agachar enquanto est� deitado
        isCrouching = true;
        isProne = false;

        characterController.height = crouchHeight;
        characterController.center = new Vector3(originalCenter.x, crouchHeight / 2, originalCenter.z);
        AdjustCameraY(-crouchCameraOffset);
    }

    void Deitar()
    {
        isProne = true;
        isCrouching = false;

        characterController.height = proneHeight;
        characterController.center = new Vector3(originalCenter.x, proneHeight / 2, originalCenter.z);
        AdjustCameraY(-proneCameraOffset);
    }

    void Levantar()
    {
        isCrouching = false;
        isProne = false;

        characterController.height = originalHeight;
        characterController.center = originalCenter;
        AdjustCameraY(0); // Retorna � posi��o inicial
    }

    void AdjustCameraY(float offset)
    {
        Vector3 cameraPosition = playerCamera.transform.localPosition;
        cameraPosition.y = originalCameraY + offset;
        playerCamera.transform.localPosition = cameraPosition;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void StartDash()
    {
        isDashing = true;
        dashDirection = transform.forward; // Define a dire��o do dash
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

            yield return null; // Espera pelo pr�ximo frame
        }

        isDashing = false; // Termina o dash
    }

}
