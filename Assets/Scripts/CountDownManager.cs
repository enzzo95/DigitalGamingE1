using System.Collections.Generic;
using UnityEngine;

public class CountDownManager : MonoBehaviour
{
    public GameObject count1;
    public GameObject count2;
    public GameObject count3;
    public GameObject fight;

    private List<GameObject> listCountdown;
    private int currentIndex = 0;
    private float timer = 0f;
    private float interval = 1f;

    void Start()
    {
        listCountdown = new List<GameObject> { count3, count2, count1, fight };

        // D�sactive tout au d�but sauf le premier
        foreach (var obj in listCountdown)
            obj.SetActive(false);

        listCountdown[currentIndex].SetActive(true);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            // D�sactive uniquement si currentIndex est encore valide
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

            timer = 0f;
        }
    }
}
