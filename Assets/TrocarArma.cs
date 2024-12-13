using UnityEngine;
public class TrocarArma : MonoBehaviour
{
    public GameObject[] weapons; // Array de armas
    private int currentWeaponIndex = 0; // �ndice da arma ativa

    void Start()
    {
        // Ativar apenas a arma inicial
        ActivateWeapon(currentWeaponIndex);
    }

    void Update()
    {
        // Troca de armas ao premir um bot�o (por exemplo, tecla "Q")
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchWeapon();
        }
    }

    void SwitchWeapon()
    {
        // Desativar a arma atual
        weapons[currentWeaponIndex].SetActive(false);

        // Incrementar o �ndice
        currentWeaponIndex++;
        if (currentWeaponIndex >= weapons.Length)
        {
            currentWeaponIndex = 0; // Voltar � primeira arma
        }

        // Ativar a nova arma
        ActivateWeapon(currentWeaponIndex);
    }

    void ActivateWeapon(int index)
    {
        weapons[index].SetActive(true);
    }
}
