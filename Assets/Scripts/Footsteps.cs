using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource footstepsSound;
    public AudioSource runSound;
    public float raycastDistance = 0.1f;
    public Transform raycastOrigin;

    void Update()
    {
        // Verifica se o jogador est� se movendo (W, A, S, D)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Ray ray = new Ray(raycastOrigin.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (hit.collider.tag == "Untagged" || hit.collider.tag == "Platform")
                {
                    // Verifica se o Shift est� pressionado para alternar o som
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        // Som de corrida
                        if (!runSound.isPlaying) // Toca o som de corrida se ainda n�o estiver tocando
                        {
                            footstepsSound.Stop(); // Para o som de passos padr�o
                            runSound.enabled = true;
                            runSound.Play();
                        }
                    }
                    else
                    {
                        // Som de passos normais
                        if (!footstepsSound.isPlaying) // Toca o som de passos se ainda n�o estiver tocando
                        {
                            runSound.Stop(); // Para o som de corrida
                            footstepsSound.enabled = true;
                            footstepsSound.Play();
                        }
                    }
                }
                else
                {
                    // Desativa sons se o jogador n�o estiver em uma superf�cie v�lida
                    footstepsSound.enabled = false;
                    footstepsSound.Stop();
                    runSound.enabled = false;
                    runSound.Stop();
                }
            }
            else
            {
                // Desativa sons se o jogador n�o estiver no ch�o
                footstepsSound.enabled = false;
                footstepsSound.Stop();
                runSound.enabled = false;
                runSound.Stop();
            }
        }
        else
        {
            // Desativa sons quando o jogador n�o est� se movendo
            footstepsSound.enabled = false;
            footstepsSound.Stop();
            runSound.enabled = false;
            runSound.Stop();
        }
    }
}
