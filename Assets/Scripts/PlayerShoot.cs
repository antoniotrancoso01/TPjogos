using UnityEngine;
public class PlayerShoot : MonoBehaviour
{
    public Animator animator;
    public Camera playerCamera; // Refer�ncia � c�mara do jogador
    public float aimFov = 40f; // FOV ao mirar
    public float normalFov = 60f; // FOV padr�o
    public float aimTransitionSpeed = 5f; // Velocidade da transi��o do FOV

    public bool isAiming = false; // Indica se o jogador est� a mirar
    public bool isShooting = false;

    void Update()
    {
        // Verifica se o bot�o direito do rato est� pressionado
        if (Input.GetMouseButton(1))
        {
            isAiming = true;
            //animator.SetBool("isAiming", true);

        }
        else
        {
            isAiming = false;
            //animator.SetBool("isAiming", false);
        }

        // Atualiza o FOV
        UpdateCameraFov();
    }

    private void UpdateCameraFov()
    {
        // Define o FOV alvo com base no estado de mira
        float targetFov = isAiming ? aimFov : normalFov;
        // Transi��o suave entre os valores de FOV
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFov, Time.deltaTime * aimTransitionSpeed);
    }
}