using UnityEngine;

public class PortasDungeonScript : MonoBehaviour
{
    public Animator portaAnimator;       // Refer�ncia ao Animator da porta
    public ObjetivosUI objetivosUI;      // Refer�ncia ao sistema de objetivos
    public string mensagemSemChave = "Voc� precisa da chave correta!";
    public string idChave;               // Identificador da chave necess�ria
    public TeamSeguirPlayer npcSeguir;   // Refer�ncia ao script de seguimento do NPC
    public Animator npcAnimator;         // Refer�ncia ao Animator do NPC

    private bool estaAberta = false;



    public bool portaFazSpawn = false; // Define se esta porta deve fazer spawn do zumbi
    public GameObject zombiePrefab; // Prefab do zumbi
    public Transform spawnPoint;    // Local onde o zumbi ser� instanciado
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

        // Ativar a anima��o da porta
        if (portaAnimator != null)
        {
            portaAnimator.SetTrigger("Abrir");
        }

        // Marcar o pr�ximo objetivo
        if (objetivosUI != null)
        {
            objetivosUI.MarcarProximoObjetivo();
        }

        // Ativar o seguimento do NPC
        if (npcSeguir != null)
        {
            npcSeguir.AtivarSeguimento();
        }

        // Ativar a anima��o de Walking
        if (npcAnimator != null)
        {
            npcAnimator.SetBool("IsWalking", true); // Ativa o par�metro "IsWalking"
        }

        // Fazer spawn do zumbi se esta porta for uma das espec�ficas e o zumbi ainda n�o foi spawnado
        if (portaFazSpawn && !zumbiSpawnado)
        {
            zumbiSpawnado = true; // Marca que o zumbi j� foi spawnado
            if (zombiePrefab != null && spawnPoint != null)
            {
                Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
                Debug.Log("Zumbi instanciado pela porta: " + gameObject.name);
            }
        }

        Debug.Log("Porta aberta!");
    }

}