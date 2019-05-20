using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decor : MonoBehaviour
{
    public GameObject objectToDestroy;
    public GameObject effect;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Instantiate(effect, objectToDestroy.transform.position, objectToDestroy.transform.rotation);
            Destroy(objectToDestroy);
    }

}
