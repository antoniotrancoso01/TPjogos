using UnityEngine;

public class PlayAnimPortas : MonoBehaviour
{
    [SerializeField] private Animator portaCasa = null;

    [SerializeField] private string abrirPorta = "abrirPortaCasaD";

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            portaCasa.Play(abrirPorta, 0, 0.0f);
        }
    }
}
