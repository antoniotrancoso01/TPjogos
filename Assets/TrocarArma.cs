using UnityEngine;
public class TrocarArma : MonoBehaviour
{
    public GameObject[] weapons; // Array de armas
    private int currentWeaponIndex = 0; // Índice da arma ativa

    void Start()
    {
        // Ativar apenas a arma inicial
        ActivateWeapon(currentWeaponIndex);
    }

    void Update()
    {
        // Troca de armas ao premir um botão (por exemplo, tecla "Q")
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchWeapon();
        }
    }

    void SwitchWeapon()
    {
        // Desativar a arma atual
        weapons[currentWeaponIndex].SetActive(false);

        // Incrementar o índice
        currentWeaponIndex++;
        if (currentWeaponIndex >= weapons.Length)
        {
            currentWeaponIndex = 0; // Voltar à primeira arma
        }

        // Ativar a nova arma
        ActivateWeapon(currentWeaponIndex);
    }

    void ActivateWeapon(int index)
    {
        weapons[index].SetActive(true);
    }
}
