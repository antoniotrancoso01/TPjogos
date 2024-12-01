using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class shotgun : MonoBehaviour
{
    public Transform Spwanpoint;
    public float distance = 15f;
    public GameObject muzle;
    public GameObject impact;
    public Animator animator;
    Camera cam;
    public int maxBalas = 6;       // Número máximo de balas no carregador
    public int balasAtuais;       // Balas restantes no carregador
    public int balasReserva = 30; // Total de balas disponíveis para recarga
    public float tempoRecarga = 2f; // Tempo necessário para recarregar
    public bool aRecarregar = false; //bool para fazer no natal
    public TextMeshProUGUI balasUI;

    public AudioSource audioSource;
    public AudioClip disparoClip;  //Som do disparo
    public AudioClip reloadClip; //Som de reload

    void Start()
    {
        cam = Camera.main;
        balasAtuais = maxBalas;
        AtualizarUI();
    }

    void Update()
    {
        //Não processa inputs se o jogo está pausado ou se o cursor está visível
        if (PauseMenu.gameIsPaused || Cursor.lockState != CursorLockMode.Locked)
            return;

        AtualizarUI();

        if (Input.GetKeyDown(KeyCode.Mouse0) && !aRecarregar)
        {
            if (balasAtuais > 0)
            {
                animator.SetBool("disparar", true);
                Shoot();
            }
        }
        else
        {
            animator.SetBool("disparar", false);
        }

        if (Input.GetKeyDown(KeyCode.R) && !aRecarregar && balasAtuais < maxBalas && balasReserva > 0)
        {
            StartCoroutine(Recarregar());
        }
    }

    void Shoot()
    {
        balasAtuais--;
        RaycastHit hit;
        RaycastHit hit2;
        RaycastHit hit3;
        RaycastHit hit4;

        GameObject muzzleInstance = Instantiate(muzle, Spwanpoint.position, Spwanpoint.localRotation);
        muzzleInstance.transform.parent = Spwanpoint;

        // Raycast principal (frontal)
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            GameObject impactInstance = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactInstance, 2f); // Destroi após 2 segundos
            ApplyDamage(hit);
        }

        // Raycast lateral esquerdo
        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(-0.2f, 0f, 0f), out hit2, distance))
        {
            GameObject impactInstance = Instantiate(impact, hit2.point, Quaternion.LookRotation(hit2.normal));
            Destroy(impactInstance, 2f);
            ApplyDamage(hit2);
        }

        // Raycast superior
        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(0f, .1f, 0f), out hit3, distance))
        {
            GameObject impactInstance = Instantiate(impact, hit3.point, Quaternion.LookRotation(hit3.normal));
            Destroy(impactInstance, 2f);
            ApplyDamage(hit3);
        }

        // Raycast inferior
        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(0f, -.1f, 0f), out hit4, distance))
        {
            GameObject impactInstance = Instantiate(impact, hit4.point, Quaternion.LookRotation(hit4.normal));
            Destroy(impactInstance, 2f);
            ApplyDamage(hit4);
            audioSource.PlayOneShot(disparoClip);
        }
    }

    void ApplyDamage(RaycastHit hit)
    {
        Vida vidaScript = hit.collider.GetComponent<Vida>();

        if (vidaScript != null)
        {
           vidaScript.ReceberDano(10); // Aplica 10 de dano ao objeto atingido
        }
    }

    System.Collections.IEnumerator Recarregar()
    {
        aRecarregar = true;
        Debug.Log("A recarregar...");
        animator.SetTrigger("primeiroreload"); // Aciona a animação de recarga, se houver

        // Aguarda o tempo de recarga
        while (balasAtuais < maxBalas && balasReserva > 0)
        {
            animator.SetTrigger("segundoreload");
            animator.SetBool("recarregar", true);
            balasAtuais++;
            balasReserva--;
            AtualizarUI();
            audioSource.PlayOneShot(reloadClip);
            yield return new WaitForSeconds(0.5f);
        }
        animator.SetBool("recarregar", false);
        // Calcula as balas recarregadas
        int balasNecessarias = maxBalas - balasAtuais;
        int balasRecarregadas = Mathf.Min(balasNecessarias, balasReserva);

        balasAtuais += balasRecarregadas;
        balasReserva -= balasRecarregadas;

        Debug.Log($"Recarga completa! Balas no carregador: {balasAtuais}, Balas na reserva: {balasReserva}");
        animator.SetTrigger("reloadfinal");
        aRecarregar = false;
        yield return null;
    }
    void AtualizarUI()
    {
        if (balasUI != null)
        {
            balasUI.text = $"Balas: {balasAtuais}/{maxBalas} | Reserva: {balasReserva}";
        }
    }
}
