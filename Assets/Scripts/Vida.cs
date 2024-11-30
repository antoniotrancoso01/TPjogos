using UnityEngine;

public class Vida : MonoBehaviour
{
    // Variável que guarda a vida inicial que o gameObject tem
    public int vidaInicial = 100;

    // Variável do tipo booleana que indica se o gameObject pode fazer respawn ou não
    public bool fazerRespawn = true;

    // Variável do tipo Vector3 que indica a posição onde irá fazer respawn
    private Vector3 posicaoRespawn;

    // Variável que guarda a vida que o jogador tem a cada momento
    private int vidaAtual;

    // Start is called before the first frame update
    void Start()
    {
        // Quando o jogo iniciar, guarda na variável posicaoRespawn a posição atual do gameObject
        posicaoRespawn = gameObject.transform.position;

        // Quando o jogo iniciar, coloca a vidaAtual com o mesmo valor que a vidaInicial
        vidaAtual = vidaInicial;
    }

    // Função pública para receber dano de qualquer fonte (inclusive do ZombieDamage)
    public void ReceberDano(int dano)
    {
        // Reduz a vida atual com base no dano recebido
        vidaAtual -= dano;

        Debug.Log($"{gameObject.name} recebeu {dano} de dano. Vida atual: {vidaAtual}");

        // Verifica se a vida atual é inferior ou igual a 0
        if (vidaAtual <= 0)
        {
            // Se o gameObject puder fazer respawn
            if (fazerRespawn)
            {
                Respawn();
            }
            else
            {
                Morrer();
            }
        }
    }

    // Lógica de respawn
    private void Respawn()
    {
        gameObject.transform.SetPositionAndRotation(posicaoRespawn, this.transform.rotation);
        vidaAtual = vidaInicial; // Restaura a vida inicial
        Debug.Log($"{gameObject.name} fez respawn.");
    }

    // Lógica para "morte" definitiva
    public void Morrer()
    {
        gameObject.SetActive(false); // Desativa o objeto
        Debug.Log($"{gameObject.name} morreu.");
    }
}
