using UnityEngine;

public class Money15 : MonoBehaviour
{

    GameObject Universal;
    UniversalScript us;
    GameObject Ball;
    BallScript bs;
    GameObject Shop;
    ShopScript sc;
    int MoneyAdded = 5;
    bool ran = false;
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
        bs.hits15 = true;
        
        if (bs.hitc15 == 15)
        {
            MoneyAdded--;
        }
        if (sc.shopMoneyStart && !sc.shopMoneyStarted && !ran)
        {
            if (MoneyAdded > 0)
            {
                sc.Money += MoneyAdded;

            }
            MoneyAdded = 5;
            bs.hitc15 = 0;
            ran = true;
        } if (!sc.inShop)
        {
            ran = false;
        }
    }


}
