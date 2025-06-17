using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public string opponentTag = "J2";
    public GameObject arm;
    public Animator animator;

    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode jump = KeyCode.Space;
    public KeyCode attack = KeyCode.E;

    private Rigidbody2D rb;
    private bool isOnGround = false;
    private bool isTouchingOpponent = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(left))
        {
            move.x -= 1f;
        }

        if (Input.GetKey(right))
        {
            move.x += 1f;
        }

        if (Input.GetKeyDown(jump) && isOnGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isOnGround = false;
        }

        rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y);

        if (isTouchingOpponent && Input.GetKeyDown(attack))
        {
            Debug.Log(gameObject.tag + " collide " + opponentTag);
        }

        if (Input.GetKeyDown(attack))
        {
            animator.SetBool("attack3", true);
            arm.SetActive(true);
            Invoke("EndAttack", 0.5f);
            
        }

        animator.SetFloat("speed", Mathf.Abs(move.x));

        if (move.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(move.x), 1, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (var contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isOnGround = true;
                    break;
                }
            }
        }

        if (collision.gameObject.CompareTag(opponentTag))
        {
            isTouchingOpponent = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }

        if (collision.gameObject.CompareTag(opponentTag))
        {
            isTouchingOpponent = false;
        }
    }
    void EndAttack()
    {
        arm.SetActive(false);
        animator.SetBool("attack3", false);
    }
}
