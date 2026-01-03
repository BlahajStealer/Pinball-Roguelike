using UnityEngine;

public class AllGold : MonoBehaviour
{


    GameObject Shop;
    ShopScript sc;

    void Start()
    {
        Shop = GameObject.FindGameObjectWithTag("Shop");
        sc = Shop.GetComponent<ShopScript>();
        if (GameObject.FindGameObjectsWithTag("Pingy Thing").Length + GameObject.FindGameObjectsWithTag("AddedPinger").Length == 0)
        {
            sc.AllGoldDivide = true;
        }
    }

    void Update()
    {
        if (sc.PercChanged)
        {
            if (GameObject.FindGameObjectsWithTag("Pingy Thing").Length + GameObject.FindGameObjectsWithTag("AddedPinger").Length == 0)
            {
                sc.AllGoldDivide = true;
            }
        }
    }


}
