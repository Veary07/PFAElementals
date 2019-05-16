using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] GameObject red;
    [SerializeField] GameObject green;
    [SerializeField] HealthManager health;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        green.transform.LookAt(camera);
        red.transform.LookAt(camera);

        
    }
}
