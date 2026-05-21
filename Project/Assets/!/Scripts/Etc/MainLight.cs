using System.Collections;
using UnityEngine;

public class MainLight : MonoBehaviour
{
    [SerializeField] private GameObject lightObj;
    [SerializeField] private int repeatCount;
    [SerializeField] private float duration;

    void Start()
    {
        StartCoroutine(Light());
    }

    private IEnumerator Light()
    {
        int rndNum;
        while (true)
        {
            for(int i = 0; i < repeatCount; i++)
            {
                yield return StartCoroutine(LightChange(true));    
                yield return StartCoroutine(LightChange(false));
            }
            rndNum = Random.Range(0, 2);

            if(rndNum == 0)
                lightObj.SetActive(false);
            else 
                lightObj.SetActive(true);
            yield return new WaitForSeconds(1.5f);    
        }
    }

    private IEnumerator LightChange(bool flag)
    {   
        lightObj.SetActive(flag);
        yield return new WaitForSeconds(duration);
    }
}
