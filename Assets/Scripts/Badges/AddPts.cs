using UnityEngine;

public class AddPts : MonoBehaviour
{
    GameObject Universal;
    UniversalScript us;
    GameObject Shop;
    ShopScript sc;
    public int sellValue;
    public int buyValue;
    void Start()
    {

        
        Universal = GameObject.FindGameObjectWithTag("Empty");
        us = Universal.GetComponent<UniversalScript>();

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
    }
}
