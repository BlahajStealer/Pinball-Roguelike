using UnityEngine;

public class Percent20 : MonoBehaviour
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
        Shop = GameObject.FindGameObjectWithTag("Shop");
        sc = Shop.GetComponent<ShopScript>();
    }

    void Update()
    {
        if (sc.shopMoneyStart && !sc.shopMoneyStarted)
        {
            sc.Money *= Mathf.RoundToInt(1.2f);

        }
    }
}
