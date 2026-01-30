using UnityEngine;
using UnityEngine.InputSystem;

public class BallManager : MonoBehaviour
{
    public float amtOfBalls = 0;
    public GameObject Ball;
    public GameObject Transform;
    public Transform Sub;
    public GameObject UniversalObject;
    UniversalScript us;
    public bool down = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UniversalObject = GameObject.FindGameObjectWithTag("Empty");
        us = UniversalObject.GetComponent<UniversalScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (amtOfBalls == 0)
        {
            Debug.Log("You lose");
            amtOfBalls++;
        }
        if (Keyboard.current.fKey.wasPressedThisFrame && us.Lives > 0 && !us.Respawning)
        {
            Debug.Log("Deployed the Package; Mission Acomplished!");
            Instantiate(Ball, new Vector3(2.5f, 12, 0), Transform.transform.rotation);
            us.Lives--;
            
        }
    }
    
}
