using UnityEngine;

public class Every15 : MonoBehaviour
{
    GameObject Universal;
    UniversalScript us;    
    GameObject Ball;
    BallScript bs;    
    GameObject Shop;
    ShopScript sc;
    public int Hits = 0;
    public int sellValue;
    public int buyValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sellValue = 2;
        buyValue = 4;

        Universal = GameObject.FindGameObjectWithTag("Empty");
        us = Universal.GetComponent<UniversalScript>();        
        Ball = GameObject.FindGameObjectWithTag("Player");
        bs = Ball.GetComponent<BallScript>();        
        Shop = GameObject.FindGameObjectWithTag("Shop");
        sc = Shop.GetComponent<ShopScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bs.JustHit)
        {
            Hits += 1;
            bs.JustHit = false;
        }
        if (Hits >= 15)
        { 
            Debug.Log("Fifteen hits!");
            sc.Money += 1;
            Hits = 0;
            us.AddOnAct();
        }
    }
}
