using UnityEngine;

public class ArmAttack : MonoBehaviour
{
    public string opponentTag = "J2"; // Tag of the opponent to detect

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called when another collider enters this trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the opponent tag
        if (other.CompareTag(opponentTag))
        {
            Player player = other.GetComponent<Player>(); // Get the player in contact
            if (player != null)
            {
                player.TakeDamage(2);   // Deal damage to opponent
            }

            else
            {
                Debug.Log("Player script not found on: " + other.name);
            }
        }
    }
}
