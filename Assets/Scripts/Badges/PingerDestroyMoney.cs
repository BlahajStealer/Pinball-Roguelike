using UnityEngine;

public class PingerDestroyMoney : MonoBehaviour
{
    GameObject Universal;
    UniversalScript us;
    GameObject Ball;
    BallScript bs;
    GameObject Shop;
    ShopScript sc;
    GameObject Cam;
    CameraScript cs;
    void Start()
    {
        Universal = GameObject.FindGameObjectWithTag("Empty");
        us = Universal.GetComponent<UniversalScript>();
        Ball = GameObject.FindGameObjectWithTag("Player");
        bs = Ball.GetComponent<BallScript>();
        Shop = GameObject.FindGameObjectWithTag("Shop");
        sc = Shop.GetComponent<ShopScript>();
        Cam = GameObject.FindGameObjectWithTag("MainCamera");
        cs = Cam.GetComponent<CameraScript>();
    }

    void Update()
    {
        
        cs.DestroyPinger = true;
    }
}
