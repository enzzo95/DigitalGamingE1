using UnityEngine;

public class ArmAttack : MonoBehaviour
{
    public string opponentTag = "J2";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touch");
        if (other.CompareTag(opponentTag))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("life before: " + player.getLife());
                player.TakeDamage(2);
                Debug.Log("life after: " + player.getLife());
            }

            else
            {
                Debug.Log("Player script not found on: " + other.name);
            }
        }
    }
}
