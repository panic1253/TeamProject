using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cost : MonoBehaviour {
    float costTime = 0;
    float timer=.5f;
    float coin =0;
    public Text teee;
	// Use this for initialization
	void Start ()
    {
        Debug.Log("start");
        StartCoroutine(test());
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(coin);
    }
    IEnumerator test()
    {
        while (true)
        {
            costTime += Time.deltaTime;
            if (costTime > timer)
            {
                coin += 2;
                timer += .3f;
            }
            //teee.text = ""+Mathf.Round(costTime);
            yield return null;
        }
    }
}
