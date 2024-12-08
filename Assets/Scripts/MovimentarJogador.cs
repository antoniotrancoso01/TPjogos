using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MovimentarJogador : MonoBehaviour
{
    // Configurações gerais
    public float sensibilidadeX = 5f;
    public float sensibilidadeY = 5f;
    public GameObject objectoCamara;
    private float ratoRotacaoX = 0;
    private float ratoRotacaoY = 0;
    public float velocidadeMovimento = 1f;
    private Vector3 vetorMovimento = new Vector3();
    public float efeitoGravidade = 1f;
    public float forcaSalto = 1f;
    public GameObject pauseMenuUI;
    public static bool gameIsPaused = false;
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Camera playerCamera; // Referência à câmara do jogador
    public CharacterController characterController; // Referência ao colisor do personagem

    // Configurações de postura
    public float crouchHeight = 0.5f; // Altura ao agachar
    public float crouchCameraOffset = 0.75f; // Offset da câmera ao agachar
    public float proneHeight = 0.25f; // Altura ao deitar
    public float proneCameraOffset = 1.25f; // Offset da câmera ao deitar

    private float originalHeight; // Altura original
    private float originalCameraY; // Posição inicial da câmera
    private Vector3 originalCenter; // Centro original do CharacterController

    private bool isCrouching = false; // Estado de agachamento
    private bool isProne = false; // Estado de deitado
    private CharacterController controladorPersonagem;

    void Start()
    {
        controladorPersonagem = gameObject.GetComponent<CharacterController>();

        // Carregar configurações salvas
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

        // Rotação da câmera
        ratoRotacaoX += Input.GetAxis("Mouse X") * sensibilidadeX;
        ratoRotacaoY += Input.GetAxis("Mouse Y") * sensibilidadeY;
        transform.rotation = Quaternion.Euler(0, ratoRotacaoX, 0);
        ratoRotacaoY = Mathf.Clamp(ratoRotacaoY, -90, 90);
        objectoCamara.transform.localRotation = Quaternion.Euler(-1 * ratoRotacaoY, 0, 0);

        // Movimentação
        if (Input.GetKey(KeyCode.W)) vetorMovimento.z = velocidadeMovimento;
        if (Input.GetKey(KeyCode.S)) vetorMovimento.z = -velocidadeMovimento;
        if (Input.GetKey(KeyCode.D)) vetorMovimento.x = velocidadeMovimento;
        if (Input.GetKey(KeyCode.A)) vetorMovimento.x = -velocidadeMovimento;

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
        if (isProne) return; // Não pode agachar enquanto está deitado
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
        AdjustCameraY(0); // Retorna à posição inicial
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
}
