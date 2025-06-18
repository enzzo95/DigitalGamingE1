using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    public SpriteRenderer lifeBar;
    public Player player;

    public Sprite lifeBar0_4;
    public Sprite lifeBar1_4;
    public Sprite lifeBar2_4;
    public Sprite lifeBar3_4;
    public Sprite lifeBar4_4;

    private int respawn = 2;
    public GameObject hearth1;
    public GameObject hearth2;
    private List<GameObject> listHearth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        listHearth = new List<GameObject> { hearth2, hearth1 };
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        if (player.getLife() <= 0) 
        {
            lifeBar.sprite = lifeBar0_4;
            checkRespawn();
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

    public void checkRespawn()
    {
        if (respawn > 0)
        {
            respawn -= 1;
            Debug.Log("New Respawn: " + respawn);

            int hearthIndex = respawn;
            if (hearthIndex >= 0 && hearthIndex < listHearth.Count)
            {
                Destroy(listHearth[hearthIndex]);
                listHearth[hearthIndex] = null;
            }

            if (player != null)
            {
                Vector2 respawnPos = player.name.Contains("1") ? new Vector2(-1.5f, 0f) : new Vector2(1.5f, 0f);
                GameObject newPlayerGO = Instantiate(player.playerPrefab, respawnPos, Quaternion.identity);
                Player newPlayer = newPlayerGO.GetComponent<Player>();
                newPlayer.setLife(20f);

                Destroy(player.gameObject);
                player = newPlayer;
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
