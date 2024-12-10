using TreeEditor;
using UnityEngine;

public class seguirOplayer : MonoBehaviour
{
    public Animator Anim;
    public Transform Target;
    public float speed=2f;
    public float detectionRange = 1f;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Calcula a dist�ncia entre o inimigo e o jogador
        float distanceToTarget = Vector3.Distance(transform.position, Target.position);

        // Atualiza o par�metro do Animator com base na dist�ncia
        if (distanceToTarget <= detectionRange)
        {
           Anim.SetBool("Isperto", true);
        }
        else
        {
            Anim.SetBool("Isperto", false);
        }

        Vector3 pos =Vector3.MoveTowards(transform.position,Target.position,speed*Time.fixedDeltaTime);
        rb.MovePosition(pos);
        transform.LookAt(Target);
    }
}