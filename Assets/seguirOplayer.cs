using TreeEditor;
using UnityEngine;

public class seguirOplayer : MonoBehaviour
{
    public Transform Target;
    public float speed=4f;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos=Vector3.MoveTowards(transform.position,Target.position,speed*Time.fixedDeltaTime);
        rb.MovePosition(pos);
        transform.LookAt(Target);
    }
}
