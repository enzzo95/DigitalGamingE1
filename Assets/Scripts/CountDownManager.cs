using System.Collections.Generic;
using UnityEngine;

public class CountDownManager : MonoBehaviour
{
    // Get GameObject for display countdown
    public GameObject count1;   
    public GameObject count2;
    public GameObject count3;
    public GameObject fight;

    // Get Player for freeze while countdown is working
    public GameObject playerP1; 
    public GameObject playerP2;
    private bool hasUnfrozenPlayers = false;

    // Initialization of variable
    private List<GameObject> listCountdown; // Création of list for countdown object
    private int currentIndex = 0;
    private float timer = 0f;
    private float interval = 1f;

    void Start()
    {
        // Initialization of the display order of GameObject
        listCountdown = new List<GameObject> { count3, count2, count1, fight };

        // Hide all countdown objects initially
        foreach (var obj in listCountdown)
        {
            obj.SetActive(false);
        }

        listCountdown[currentIndex].SetActive(true); // Show the first countdown object

        // Freeze both players at the start
        playerP1.GetComponent<Player>().enabled = false;
        playerP2.GetComponent<Player>().enabled = false;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // When the interval passes, move to the next countdown object
        if (timer >= interval)
        {
            if (currentIndex < listCountdown.Count)
            {
                listCountdown[currentIndex].SetActive(false);
                currentIndex++;
            }

            if (currentIndex < listCountdown.Count)
            {
                listCountdown[currentIndex].SetActive(true);
            }

            // When the countdown is over, unfreeze the players
            if (currentIndex >= listCountdown.Count && !hasUnfrozenPlayers)
            {
                playerP1.GetComponent<Player>().enabled = true;
                playerP2.GetComponent<Player>().enabled = true;
                hasUnfrozenPlayers = true;
            }

            timer = 0f;
        }
    }
}
