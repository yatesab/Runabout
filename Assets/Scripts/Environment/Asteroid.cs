using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        float randomScale = Random.Range(800f, 5000f);
        transform.localScale = Vector3.one * randomScale;

        transform.localRotation = new Quaternion(Random.Range(0f, 50f), Random.Range(0f, 50f), Random.Range(0f, 50f), Random.Range(0f, 50f));

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
}
