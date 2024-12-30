using UnityEngine;

public class FloatAndRotate : MonoBehaviour
{
    public float velocidadeRotacao = 50f;  // Velocidade da rotação em graus por segundo
    public float amplitudeFlutuacao = 0.5f;  // Amplitude do movimento vertical
    public float velocidadeFlutuacao = 2f;  // Velocidade do movimento vertical

    private Vector3 posicaoInicial;

    void Start()
    {
        // Armazena a posição inicial do objeto
        posicaoInicial = transform.position;
    }

    void Update()
    {
        // Faz o objeto girar lentamente no eixo Y
        transform.Rotate(Vector3.up * velocidadeRotacao * Time.deltaTime, Space.World);

        // Faz o objeto flutuar para cima e para baixo
        float deslocamento = Mathf.Sin(Time.time * velocidadeFlutuacao) * amplitudeFlutuacao;
        transform.position = posicaoInicial + new Vector3(0, deslocamento, 0);
    }
}

