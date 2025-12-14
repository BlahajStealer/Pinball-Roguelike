using UnityEngine;

public class Every15 : MonoBehaviour
{
    GameObject Universal;
    UniversalScript us;    
    GameObject Ball;
    BallScript bs;    
    GameObject Shop;
    ShopScript sc;
    int Hits;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        }
        if (Hits >= 15)
        {
            sc.Money += 1;
            us.AddOnAct();
        }
    }
}
