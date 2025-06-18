using UnityEngine;

public class LifeBar : MonoBehaviour
{
    public SpriteRenderer lifeBar;
    public Player player;

    public Sprite lifeBar0_4;
    public Sprite lifeBar1_4;
    public Sprite lifeBar2_4;
    public Sprite lifeBar3_4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Life <= 0) 
        {
            lifeBar.sprite = lifeBar0_4;
        }

        else if (player.Life <= 5)
        {
            lifeBar.sprite = lifeBar1_4;
        }

        else if (player.Life <= 10)
        {
            lifeBar.sprite = lifeBar2_4;
        }

        else if (player.Life <= 15)
        {
            lifeBar.sprite = lifeBar3_4;
        }
    }
}
