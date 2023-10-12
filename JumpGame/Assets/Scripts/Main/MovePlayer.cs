using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer characterRenderer;
    public float jumpForce = 10f;
    public bool isJump = false;
    [SerializeField] private Animator animator;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Horizontal"))
            characterRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        if (rb.velocity.normalized.x == 0)
            animator.SetBool("IsRun", false);
        else
            animator.SetBool("IsRun", true);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            animator.SetBool("IsJump", true);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (!isJump)
            rb.AddForce(Vector2.right * h * 0.5f, ForceMode2D.Impulse);

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(!isJump)
            {
                isJump = true;
                rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Rand"))
        {
            isJump = false;
        }
    }
}
