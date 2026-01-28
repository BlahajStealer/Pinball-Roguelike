using UnityEngine;

public class BallManager : MonoBehaviour
{
    public float amtOfBalls = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (amtOfBalls == 0)
        {
            Debug.Log("You lose");
            amtOfBalls++;
        }
    }
}
