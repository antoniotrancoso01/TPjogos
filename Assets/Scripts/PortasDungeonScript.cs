using UnityEngine;

public class PortasDungeonScript : MonoBehaviour
{
    public Animator portaAnimator;       // Referência ao Animator da porta
    public ObjetivosUI objetivosUI;      // Referência ao sistema de objetivos
    public string mensagemSemChave = "Você precisa da chave correta!";
    public string idChave;               // Identificador da chave necessária
    public TeamSeguirPlayer npcSeguir;   // Referência ao script de seguimento do NPC
    public Animator npcAnimator;         // Referência ao Animator do NPC

    private bool estaAberta = false;



    public bool portaFazSpawn = false; // Define se esta porta deve fazer spawn do zumbi
    public GameObject zombiePrefab; // Prefab do zumbi
    public Transform spawnPoint;    // Local onde o zumbi será instanciado
    public static bool zumbiSpawnado = false; // Flag global para verificar o spawn


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            var inventario = other.GetComponent<PlayerInventario>();

            if (inventario != null && inventario.TemChave(idChave) && !estaAberta)
            {
                AbrirPorta();
            }
            else if (inventario != null && !inventario.TemChave(idChave))
            {
                Debug.Log(mensagemSemChave);
            }
        }
    }

    private void AbrirPorta()
    {
        estaAberta = true;

        // Ativar a animação da porta
        if (portaAnimator != null)
        {
            portaAnimator.SetTrigger("Abrir");
        }

        // Marcar o próximo objetivo
        if (objetivosUI != null)
        {
            objetivosUI.MarcarProximoObjetivo();
        }

        // Ativar o seguimento do NPC
        if (npcSeguir != null)
        {
            npcSeguir.AtivarSeguimento();
        }

        // Ativar a animação de Walking
        if (npcAnimator != null)
        {
            npcAnimator.SetBool("IsWalking", true); // Ativa o parâmetro "IsWalking"
        }

        // Fazer spawn do zumbi se esta porta for uma das específicas e o zumbi ainda não foi spawnado
        if (portaFazSpawn && !zumbiSpawnado)
        {
            zumbiSpawnado = true; // Marca que o zumbi já foi spawnado
            if (zombiePrefab != null && spawnPoint != null)
            {
                Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
                Debug.Log("Zumbi instanciado pela porta: " + gameObject.name);
            }
        }

        Debug.Log("Porta aberta!");
    }

}