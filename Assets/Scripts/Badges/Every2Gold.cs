using UnityEngine;

public class Every2Gold : MonoBehaviour
{
    GameObject Universal;
    UniversalScript us;
    GameObject Ball;
    BallScript bs;
    GameObject Shop;
    ShopScript sc;
    float goldAmount;
    bool OneFrame = true;
    void Start()
    {

        Shop = GameObject.FindGameObjectWithTag("Shop");
        sc = Shop.GetComponent<ShopScript>();
        sc.DivisionPts += 2;
        goldAmount = GameObject.FindGameObjectsWithTag("Gold Pingy Thing").Length;

    }

    void Update()
    {
        if (sc.PercChanged)
        {
            goldAmount = GameObject.FindGameObjectsWithTag("Gold Pingy Thing").Length;
            Debug.Log("Perc Changed");
        }
        if (sc.shopMoneyStart && OneFrame)
        {
            Debug.Log("Test: " + goldAmount);
            sc.Money += Mathf.Floor(goldAmount / 2f);
            OneFrame = false;
        } if (!sc.shopMoneyStart)
        {
            OneFrame = true;
        }
    }
}
