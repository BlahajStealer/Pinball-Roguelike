using UnityEngine;

public class AddPts : MonoBehaviour
{
    GameObject Universal;
    UniversalScript us;
    GameObject Ball;
    BallScript bs;
    GameObject Shop;
    ShopScript sc;
    public int sellValue;
    public int buyValue;
    void Start()
    {
        sellValue = 5;
        buyValue = 8;
        
        Universal = GameObject.FindGameObjectWithTag("Empty");
        us = Universal.GetComponent<UniversalScript>();
        Ball = GameObject.FindGameObjectWithTag("Player");
        bs = Ball.GetComponent<BallScript>();
        Shop = GameObject.FindGameObjectWithTag("Shop");
        sc = Shop.GetComponent<ShopScript>();
    }

    void Update()
    {
        if (sc.Leaving) {
            us.score = us.target / 3;
            sc.Leaving = false;
            us.AddOnAct();

        }
        sc.DivisionPts = 3;
    }
}
