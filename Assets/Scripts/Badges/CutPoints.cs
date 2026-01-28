using UnityEngine;

public class CutPoints : MonoBehaviour
{
    GameObject Universal;
    UniversalScript us;

    GameObject Shop;
    ShopScript sc;
    public int sellValue;
    public int buyValue;
    void Start()
    {
        sellValue = 3;
        buyValue = 5;
        Universal = GameObject.FindGameObjectWithTag("Empty");
        us = Universal.GetComponent<UniversalScript>();

        Shop = GameObject.FindGameObjectWithTag("Shop");
        sc = Shop.GetComponent<ShopScript>();
    }

    void Update()
    {
        sc.cutPts = true;
    }


}
