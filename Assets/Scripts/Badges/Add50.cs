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
    void Start()
    {
        
        Universal = GameObject.FindGameObjectWithTag("Empty");
        us = Universal.GetComponent<UniversalScript>();
        Ball = GameObject.FindGameObjectWithTag("Player");
        bs = Ball.GetComponent<BallScript>();
        Shop = GameObject.FindGameObjectWithTag("Player");
        sc = Shop.GetComponent<ShopScript>();
        us.Add50 = true;
    }

    void Update()
    {

    }


}
