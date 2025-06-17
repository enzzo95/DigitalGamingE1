using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(left))
        {
            move.x -= 1f;
        }

        if (Input.GetKey(right))
        {
            move.x += 1f;
        }

        transform.Translate(move * speed * Time.deltaTime);
    }
}
