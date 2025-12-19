using UnityEngine;

public class Add50 : MonoBehaviour
{
    GameObject Universal;
    UniversalScript us;
    GameObject Ball;
    BallScript bs;
    GameObject Shop;
    ShopScript sc;
    public int totalPoints;
    public int sellValue;
    public int buyValue;

    void Start()
    {
        sellValue = 4;
        buyValue = 6;
        Universal = GameObject.FindGameObjectWithTag("Empty");
        us = Universal.GetComponent<UniversalScript>();
        Ball = GameObject.FindGameObjectWithTag("Player");
        bs = Ball.GetComponent<BallScript>();
        Shop = GameObject.FindGameObjectWithTag("Shop");
        sc = Shop.GetComponent<ShopScript>();
        us.Add50 = true;
    }

    void Update()
    {

    }


}
