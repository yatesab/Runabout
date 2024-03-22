using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        transform.localRotation = new Quaternion(Random.Range(0f, 50f), Random.Range(0f, 50f), Random.Range(0f, 50f), Random.Range(0f, 50f));

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
}
