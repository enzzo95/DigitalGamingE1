using UnityEngine;

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
    private bool isAttacking = false;
    
    private float life = 20f;
    public float Life => life;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;

        if (!isAttacking)
        {
            if (Input.GetKey(left))
            {
                move.x -= 1f;
            }

            if (Input.GetKey(right))
            {
                move.x += 1f;
            }
        }

        Vector2 velocity = rb.linearVelocity;

        if (Input.GetKeyDown(jump) && isOnGround && !isAttacking)
        {
            velocity.y = jumpForce;
            isOnGround = false;
        }

        if (!isAttacking)
            velocity.x = move.x * speed;
        else
            velocity.x = 0;

        rb.linearVelocity = velocity;

        if (Input.GetKeyDown(attack) && !isAttacking)
        {
            isAttacking = true;
            animator.SetBool("attack3", true);
            arm.SetActive(true);
            Invoke("EndAttack", 0.25f);
        }

        if (move.x != 0 && !isAttacking)
            transform.localScale = new Vector3(Mathf.Sign(move.x), 1, 1);

        animator.SetFloat("speed", Mathf.Abs(move.x));
        animator.SetFloat("velocity", rb.linearVelocity.y);
        animator.SetBool("isOnGround", isOnGround);
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
        isAttacking = false;
        arm.SetActive(false);
        animator.SetBool("attack3", false);
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
