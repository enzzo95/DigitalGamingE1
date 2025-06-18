using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CountDownManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip fightSound;

    public GameObject count1;
    public GameObject count2;
    public GameObject count3;
    public GameObject fight;

    public GameObject playerP1;
    public GameObject playerP2;
    private bool hasUnfrozenPlayers = false;

    private List<GameObject> listCountdown;
    private int currentIndex = 0;
    private float timer = 0f;
    private float interval = 1f;

    void Start()
    {
        listCountdown = new List<GameObject> { count3, count2, count1, fight };

        // Désactive tout au début sauf le premier
        foreach (var obj in listCountdown)
            obj.SetActive(false);

        listCountdown[currentIndex].SetActive(true);

        playerP1.GetComponent<Player>().enabled = false;
        playerP2.GetComponent<Player>().enabled = false;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            // Désactive uniquement si currentIndex est encore valide
            if (currentIndex < listCountdown.Count)
            {
                listCountdown[currentIndex].SetActive(false);
                currentIndex++;
            }

            // Active uniquement si currentIndex est encore valide
            if (currentIndex < listCountdown.Count)
            {
                listCountdown[currentIndex].SetActive(true);
            }

            if (currentIndex >= listCountdown.Count && !hasUnfrozenPlayers)
            {
                audioSource.PlayOneShot(fightSound);
                playerP1.GetComponent<Player>().enabled = true;
                playerP2.GetComponent<Player>().enabled = true;
                hasUnfrozenPlayers = true;
            }

            timer = 0f;
        }
    }
}
