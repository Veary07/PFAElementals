using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour
{
    [SerializeField] GameObject summer;
    [SerializeField] GameObject winter;
    GameObject child;

    Material map;


    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Ground").GetComponent<Renderer>().material;

        if (this.transform.position.x * 0.5f < -map.GetFloat("Vector1_3ECABBA8"))
        {
            summer.SetActive(true);
            winter.SetActive(false);
        }
        else if (this.transform.position.x * 0.5f > -map.GetFloat("Vector1_3ECABBA8"))
        {
            summer.SetActive(false);
            winter.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x * 0.5f < -map.GetFloat("Vector1_3ECABBA8"))
        {
            summer.SetActive(true);
            winter.SetActive(false);
        }
        else if (this.transform.position.x * 0.5f > -map.GetFloat("Vector1_3ECABBA8"))
        {
            summer.SetActive(false);
            winter.SetActive(true);
        }
    }
}
