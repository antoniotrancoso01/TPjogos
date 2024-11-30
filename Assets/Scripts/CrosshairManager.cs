using UnityEngine;
using UnityEngine.UI;

public class CrosshairManager : MonoBehaviour
{
    public Image crosshairImage; // Refer�ncia � UI Image da mira
    public Sprite normalCrosshair; // Sprite da mira normal
    public Sprite aimingCrosshair; // Sprite da mira ao mirar
    public PlayerShoot player; // Refer�ncia ao script do jogador

    public Vector2 normalSize = new Vector2(100f, 100f); // Tamanho da mira normal
    public Vector2 aimingSize = new Vector2(50f, 50f);   // Tamanho da mira ao mirar
    public float resizeSpeed = 10f; // Velocidade de transi��o do tamanho

    void Update()
    {
        if (player != null && player.isAiming) // Verifica se o jogador est� a mirar
        {
            crosshairImage.sprite = aimingCrosshair; // Troca para a mira de aiming
            crosshairImage.rectTransform.sizeDelta = Vector2.Lerp(
                crosshairImage.rectTransform.sizeDelta,
                aimingSize,
                Time.deltaTime * resizeSpeed
            );
        }
        else
        {
            crosshairImage.sprite = normalCrosshair; // Volta para a mira normal
            crosshairImage.rectTransform.sizeDelta = Vector2.Lerp(
                crosshairImage.rectTransform.sizeDelta,
                normalSize,
                Time.deltaTime * resizeSpeed
            );
        }
    }
}