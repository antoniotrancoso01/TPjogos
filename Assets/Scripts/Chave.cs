using UnityEngine;

public class Chave : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Chave coletada!");

            // Avisa � porta que o jogador pegou a chave
            Porta porta = FindObjectOfType<Porta>();
            if (porta != null)
            {
                porta.PegarChave();
            }

            // Desativa ou destr�i a chave
            gameObject.SetActive(false);
        }
    }
}
