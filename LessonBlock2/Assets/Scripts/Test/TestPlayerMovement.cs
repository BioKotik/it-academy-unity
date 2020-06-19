using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPlayerMovement : MonoBehaviour
{
    private Vector2 direction;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float speed;
    [SerializeField] private Camera camera;
    [SerializeField] private Animator animator;

    private bool isStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            isStart = true;
        }

        if (isStart)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, transform.position.y, -10f);
            animator.SetFloat("Velocity", rigidbody.velocity.y);

            if (IsGrounded())
            {
                animator.SetBool("Blink", true);
                rigidbody.velocity = Vector2.zero;
                rigidbody.AddForce(Vector2.up * speed, ForceMode2D.Impulse);             
                return;
            }

            animator.SetBool("Blink", false);

            Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), 0f);

            Move(direction);
        }
    }

    public void Move(Vector2 direction)
    {
        Vector2 velocity = rigidbody.velocity;
        velocity.x = direction.x * speed;
        rigidbody.velocity = velocity;
    }

    private bool IsGrounded()
    {
        Vector2 point = transform.position;
        point.y -= 0.5f;//чтобы избежать рейкаста в самого себя
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.down, 0.2f);
        return hit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(3);
    }
}
