using UnityEngine;

public class AllNormal : MonoBehaviour
{

    GameObject Ball;
    BallScript bs;
    GameObject Universal;
    UniversalScript us;
    GameObject Shop;
    ShopScript sc;

    void Start() { 

        Ball = GameObject.FindGameObjectWithTag("Player");
        bs = Ball.GetComponent<BallScript>();
        Shop = GameObject.FindGameObjectWithTag("Shop");
        sc = Shop.GetComponent<ShopScript>();
        Universal = GameObject.FindGameObjectWithTag("Empty");
        us = Universal.GetComponent<UniversalScript>();    
        int Gold = GameObject.FindGameObjectsWithTag("Gold Pingy Thing").Length;
        if (Gold == 0)
        {
            us.AllNormalPingers = true;
        }

    }

    void Update()
    {
        if (sc.PercChanged)
        {
            int Gold = GameObject.FindGameObjectsWithTag("Gold Pingy Thing").Length;
            if (Gold == 0)
            {
                us.AllNormalPingers = true;
            }
        }
    }


}
