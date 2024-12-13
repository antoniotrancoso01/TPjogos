using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pistola : MonoBehaviour
{
    public Transform spawnPoint;
    public float distance = 20f;
    public GameObject muzzleFlash;
    public GameObject impactEffect;
    public Animator animator;
    Camera cam;
    public int maxBullets = 12;       // Número máximo de balas no carregador
    public int currentBullets;       // Balas restantes no carregador
    public int bulletReserve = 36;   // Total de balas disponíveis para recarga
    public float reloadTime = 1.5f;  // Tempo necessário para recarregar
    public bool isReloading = false;
    public TextMeshProUGUI bulletsUI;

    public AudioSource audioSource;
    public AudioClip shootClip;      // Som do disparo
    public AudioClip reloadClip;     // Som de recarga

    void Start()
    {
        cam = Camera.main;
        currentBullets = maxBullets;
        UpdateUI();
    }

    void Update()
    {
        // Não processa inputs se o jogo está pausado ou se o cursor está visível
        if (PauseMenu.gameIsPaused || Cursor.lockState != CursorLockMode.Locked)
            return;

        UpdateUI();

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isReloading)
        {
            if (currentBullets > 0)
            {
                animator.SetBool("disparar", true);
                Shoot();
            }
        }
        else
        {
            animator.SetBool("disparar", false);
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentBullets < maxBullets && bulletReserve > 0)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        currentBullets--;
        RaycastHit hit;

        // Instancia o efeito de disparo
        GameObject muzzleInstance = Instantiate(muzzleFlash, spawnPoint.position, spawnPoint.localRotation);
        Destroy(muzzleInstance, 1f);

        // Raycast para detectar colisão
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            GameObject impactInstance = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactInstance, 2f); // Destroi após 2 segundos
            ApplyDamage(hit);
        }

        audioSource.PlayOneShot(shootClip);
    }

    void ApplyDamage(RaycastHit hit)
    {
        Vida vidaScript = hit.collider.GetComponent<Vida>();

        if (vidaScript != null)
        {
            vidaScript.ReceberDano(15); // Aplica 15 de dano ao objeto atingido
        }
    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Recarregando...");
        animator.SetTrigger("Recarregar"); // Aciona a animação de recarga
        animator.SetBool("recarregar", true);
        audioSource.PlayOneShot(reloadClip);
        yield return new WaitForSeconds(reloadTime);

        // Recarga total em uma única etapa
        int bulletsToReload = Mathf.Min(maxBullets - currentBullets, bulletReserve);
        currentBullets += bulletsToReload;
        bulletReserve -= bulletsToReload;

        Debug.Log($"Recarga completa! Balas no carregador: {currentBullets}, Balas na reserva: {bulletReserve}");
        isReloading = false;
        animator.SetBool("recarregar", false);
        UpdateUI();
    }


    void UpdateUI()
    {
        if (bulletsUI != null)
        {
            bulletsUI.text = $"Balas: {currentBullets}/{maxBullets} | Reserva: {bulletReserve}";
        }
    }

    public void AddAmmo(int amount)
    {
        bulletReserve += amount;
        UpdateUI(); // Atualiza a interface
        Debug.Log($"Munição adicionada! Balas na reserva: {bulletReserve}");
    }
}
