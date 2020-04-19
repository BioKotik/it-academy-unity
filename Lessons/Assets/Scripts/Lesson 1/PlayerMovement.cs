using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb_;
    private Vector3 change;

    public int speed;

    public int jumpPower;
    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody>();       
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxis("Horizontal");
        change.z = Input.GetAxis("Vertical");
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb_.AddForce(0,jumpPower,0,ForceMode.Impulse);
        }
    }

    void Move()
    {
        rb_.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
}
