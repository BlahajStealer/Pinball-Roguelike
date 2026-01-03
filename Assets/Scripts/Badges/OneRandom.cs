using UnityEngine;

public class OneRandom : MonoBehaviour
{

    GameObject Shop;
    ShopScript sc;

    void Start()
    {
        Shop = GameObject.FindGameObjectWithTag("Shop");
        sc = Shop.GetComponent<ShopScript>();

    }

    void Update()
    {
        sc.OneRandomActive = true;
    }

}
