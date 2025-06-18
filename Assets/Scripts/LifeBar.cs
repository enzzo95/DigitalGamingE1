using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    // References to display and player
    public SpriteRenderer lifeBar;  // SpriteRenderer displaying life bar images
    public Player player;           // Reference to the player to track life

    // Different sprites representing life bar states (0/4 to 4/4)
    public Sprite lifeBar0_4;
    public Sprite lifeBar1_4;
    public Sprite lifeBar2_4;
    public Sprite lifeBar3_4;
    public Sprite lifeBar4_4;

    // Respawn system variables and references to life indicators (hearts)
    private int respawn = 2;    // Number of allowed respawns left
    public GameObject hearth1;  // Visual heart 1
    public GameObject hearth2;  // Visual heart 2
    private List<GameObject> listHearth;    // List to manage hearts for respawn UI

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initiate the hearts list
        listHearth = new List<GameObject> { hearth2, hearth1 };
    }

    // Update is called once per frame
    void Update()
    {
        // Early exit if player reference is lost
        if (player == null)
        {
            return;
        }

        // Change life bar sprite based on player's current life value
        if (player.getLife() <= 0) 
        {
            lifeBar.sprite = lifeBar0_4;
            checkRespawn();     // Trigger respawn when life is zero
        }

        else if (player.getLife() <= 5)
        {
            lifeBar.sprite = lifeBar1_4;
        }

        else if (player.getLife() <= 10)
        {
            lifeBar.sprite = lifeBar2_4;
        }

        else if (player.getLife() <= 15)
        {
            lifeBar.sprite = lifeBar3_4;
        }

        else
        {
            lifeBar.sprite = lifeBar4_4;
        }
    }

    // Respawn logic: decreases respawn count, updates hearts, respawns player or ends game
    public void checkRespawn()
    {
        if (respawn > 0)
        {
            respawn -= 1;
            Debug.Log("New Respawn: " + respawn);

            // Remove a heart visually corresponding to the remaining respawns
            int hearthIndex = respawn;
            if (hearthIndex >= 0 && hearthIndex < listHearth.Count)
            {
                Destroy(listHearth[hearthIndex]);
                listHearth[hearthIndex] = null;
            }

            // Respawn the player at a fixed position based on player's name (player 1 or other)
            if (player != null)
            {
                Vector2 respawnPos = player.name.Contains("1") ? new Vector2(-1.5f, 0f) : new Vector2(1.5f, 0f);
                GameObject newPlayerGO = Instantiate(player.playerPrefab, respawnPos, Quaternion.identity);
                Player newPlayer = newPlayerGO.GetComponent<Player>();
                
                newPlayer.setLife(20f);     // Reset life on respawn

                Destroy(player.gameObject); // Remove old player object
                player = newPlayer;         // Update reference to new player
            }

            Debug.Log("Player respawned with life: " + player.getLife());
        }

        else
        {
            Debug.Log("No more respawns left!");
            if (player != null)
            {
                Destroy(player.gameObject);
                player = null;
            }
        }
    }

}
