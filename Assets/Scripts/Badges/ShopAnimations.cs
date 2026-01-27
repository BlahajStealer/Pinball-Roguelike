using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class ShopAnimations : MonoBehaviour
{
    public GameObject AnimMan;
    public GameObject LevelMan;
    public float TimeWait;
    RectTransform rt;
    RectTransform rtLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rt = AnimMan.GetComponent<RectTransform>();
        rtLevel = LevelMan.GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ShopAnim()
    {

        Vector2 end = new Vector2(222, -124);
        Vector2 start = rt.anchoredPosition;
        float timer = 0;
        while (Vector2.Distance(rt.anchoredPosition , end) > .1f) {
            timer += Time.deltaTime;
            rt.anchoredPosition = Vector2.Lerp(start, end, timer / TimeWait);
            yield return null;
        }
        yield return null;
    }      
    public IEnumerator NextLevelAnim()
    {
        rtLevel.anchoredPosition = new Vector2(0, 1020);
        Vector2 endShop = new Vector2(222, -1400);
        Vector2 startShop = rt.anchoredPosition;
        Vector2 endLevel = new Vector2(0, 0);
        Vector2 startLevel = rtLevel.anchoredPosition;
        float timer = 0;
        while (Vector2.Distance(rtLevel.anchoredPosition , endLevel) > .1f) {
            timer += Time.deltaTime;
            rt.anchoredPosition = Vector2.Lerp(startShop, endShop, timer / TimeWait);
            rtLevel.anchoredPosition = Vector2.Lerp(startLevel, endLevel, timer / TimeWait);
            yield return null;
        }
        rtLevel.anchoredPosition = new Vector2(0, 0);
        yield return null;
    }    

}
