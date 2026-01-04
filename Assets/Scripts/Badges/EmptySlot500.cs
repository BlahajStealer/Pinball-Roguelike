using UnityEngine;

public class EmptySlot500 : MonoBehaviour
{
    GameObject Universal;
    UniversalScript us;
    GameObject Ball;
    BallScript bs;
    GameObject Shop;
    ShopScript sc;
    int ScoreAdded;
    void Start()
    {
        Universal = GameObject.FindGameObjectWithTag("Empty");
        us = Universal.GetComponent<UniversalScript>();
        Ball = GameObject.FindGameObjectWithTag("Player");
        bs = Ball.GetComponent<BallScript>();
        Shop = GameObject.FindGameObjectWithTag("Shop");
        sc = Shop.GetComponent<ShopScript>();
        ScoreAdded = 0;
        for (int i = 0; i < sc.Photos.Length; i++)
        {
            if (sc.Photos[i] == null)
            {
                ScoreAdded += 500;
            }
        }
        us.Addition += ScoreAdded;
    }

    void Update()
    {
        if (sc.BadgeChanged)
        {
            us.Addition -= ScoreAdded;
            ScoreAdded = 0;
            for (int i = 0; i < sc.Photos.Length; i++)
            {
                if (sc.Photos[i] == null)
                {
                    ScoreAdded += 500;
                }
            }
            us.Addition += ScoreAdded;
        }

    }

}
