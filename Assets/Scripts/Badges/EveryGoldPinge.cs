using UnityEngine;

public class EveryGoldPinger : MonoBehaviour
{
    GameObject Ball;
    BallScript bs;

    void Start()
    {

        Ball = GameObject.FindGameObjectWithTag("Player");
        bs = Ball.GetComponent<BallScript>();

    }

    void Update()
    {
        bs.gPinger100 = true;
    }


}
