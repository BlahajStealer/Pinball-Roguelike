using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class ShopAnimations : MonoBehaviour
{
    public GameObject AnimMan;
    public float TimeWait;
    RectTransform rt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rt = AnimMan.GetComponent<RectTransform>();

        rt.anchoredPosition = new Vector2(2257, -124);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ShopAnim()
    {
        rt.anchoredPosition = new Vector2(2257, -124);

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

}
