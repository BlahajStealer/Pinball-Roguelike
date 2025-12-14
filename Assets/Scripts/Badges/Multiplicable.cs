using UnityEngine;

public class Multiplicable : MonoBehaviour
{
    GameObject Universal;
    UniversalScript us;
    GameObject Ball;
    BallScript bs;
    GameObject Shop;
    ShopScript sc;

    void Start()
    {
        Universal = GameObject.FindGameObjectWithTag("Empty");
        us = Universal.GetComponent<UniversalScript>();
        Ball = GameObject.FindGameObjectWithTag("Player");
        bs = Ball.GetComponent<BallScript>();
        Shop = GameObject.FindGameObjectWithTag("Player");
        sc = Shop.GetComponent<ShopScript>();
    }

    void Update()
    {
        int numOBadges = 0;
        float Markiplier = 1;
        for (int i = 0; i < sc.Photos.Length; i++)
        {
            if (sc.Photos[i] != null)
            {
                numOBadges++;
            }
        }
        for (int i = 0; i < numOBadges; i++)
        {
            Markiplier += .2f;
        }
        sc.NumberBadges = Markiplier;
    }

}
