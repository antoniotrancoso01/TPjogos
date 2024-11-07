using UnityEngine;

public class MovimentarJogador : MonoBehaviour
{
    public float sensibilidadeX = 5f;
    public float sensibilidadeY = 5f;

    public GameObject objectoCamara;

    private float ratoRotacaoX = 0;
    private float ratoRotacaoY = 0;

    public float velocidadeMovimento = 1f;

    private Vector3 vetorMovimento = new Vector3();

    private CharacterController controladorPersonagem;

    public float efeitoGravidade = 1f;

    public float forcaSalto = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controladorPersonagem = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ratoRotacaoX += Input.GetAxis("Mouse X") * sensibilidadeX;
        ratoRotacaoY += Input.GetAxis("Mouse Y") * sensibilidadeY;


        gameObject.transform.rotation = Quaternion.Euler(0, ratoRotacaoX, 0);

        ratoRotacaoY = Mathf.Clamp(ratoRotacaoY, -90, 90);

        objectoCamara.transform.localRotation = Quaternion.Euler(-1 * ratoRotacaoY, 0, 0); 

        if(Input.GetKey(KeyCode.W))
        {
            vetorMovimento.z = velocidadeMovimento;
        }

        if (Input.GetKey(KeyCode.S))
        {
            vetorMovimento.z = -1 * velocidadeMovimento;
        }

        if(Input.GetKey(KeyCode.D))
        {
            vetorMovimento.x = velocidadeMovimento;
        }

        if( Input.GetKey(KeyCode.A))
        {
            vetorMovimento.x = -1 * velocidadeMovimento;
        }


        vetorMovimento = gameObject.transform.forward * vetorMovimento.z + gameObject.transform.right * vetorMovimento.x + gameObject.transform.up * vetorMovimento.y;

        if (controladorPersonagem.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vetorMovimento.y = forcaSalto;
            }

        }
        else
        {
            vetorMovimento = vetorMovimento + Physics.gravity * efeitoGravidade * Time.deltaTime;
        }

        controladorPersonagem.Move(vetorMovimento * Time.deltaTime);

        vetorMovimento.x = 0;
        vetorMovimento.z = 0;
    }
}
