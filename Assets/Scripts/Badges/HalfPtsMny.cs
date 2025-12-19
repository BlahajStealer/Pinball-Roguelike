using UnityEngine;

public class HalfPtsMny : MonoBehaviour
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
        buyValue = 9;
        Universal = GameObject.FindGameObjectWithTag("Empty");
        us = Universal.GetComponent<UniversalScript>();
        Ball = GameObject.FindGameObjectWithTag("Player");
        bs = Ball.GetComponent<BallScript>();
        Shop = GameObject.FindGameObjectWithTag("Shop");
        sc = Shop.GetComponent<ShopScript>();
    }

    void Update()
    {
        sc.DivisionPts = 5f;
        sc.Halfpts = true;
    }


}
