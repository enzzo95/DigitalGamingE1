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

    public AudioSource audioSource;
    public AudioClip runSound;
    public AudioClip jumpSound;
    public AudioClip attackSound;

    [Range(0f, 1f)] public float runVolume = 0.5f;
    [Range(0f, 1f)] public float jumpVolume = 0.4f;
    [Range(0f, 1f)] public float attackVolume = 0.8f;

    private Rigidbody2D rb;
    private bool isOnGround = false;
    private bool isTouchingOpponent = false;
    private bool isAttacking = false;
    private bool isRunning = false;

    private float life = 20f;
    public GameObject playerPrefab;
    private Vector2 respawnPosition;

    void Start()
    {
        respawnPosition = new Vector2(1.5f, 2f);
        rb = GetComponent<Rigidbody2D>();
    }

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

            if (isRunning)
            {
                audioSource.Stop();
                isRunning = false;
            }
            audioSource.PlayOneShot(jumpSound, jumpVolume);
        }

        if (!isAttacking)
            velocity.x = move.x * speed;
        else
            velocity.x = 0;
        rb.linearVelocity = velocity;

        if (Input.GetKeyDown(attack) && !isAttacking && isOnGround)
        {
            isAttacking = true;
            animator.SetBool("attack3", true);
            arm.SetActive(true);

            if (isRunning)
            {
                audioSource.Stop();
                isRunning = false;
            }
            audioSource.PlayOneShot(attackSound, attackVolume);
            Invoke("EndAttack", 0.50f);
        }


        if (move.x != 0 && isOnGround && !isAttacking)
        {
            if (!isRunning)
            {
                audioSource.clip = runSound;
                audioSource.volume = runVolume;
                audioSource.loop = true;
                audioSource.Play();
                isRunning = true;
            }
        }
        else
        {
            if (isRunning)
            {
                audioSource.Stop();
                isRunning = false;
            }
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
        animator.SetBool("getHurt", true);
        life -= damage;
        if (life <= 0)
        {
            animator.SetBool("dead", true);
            Invoke("DestroyPlayer", 0.8f);
        }
        Invoke("EndTakeDamage", 0.2f);
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }

    public void EndTakeDamage()
    {
        animator.SetBool("getHurt", false);
    }

    public float getLife()
    {
        return life;
    }

    public void setLife(float value)
    {
        life = value;
    }
}