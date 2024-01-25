using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        float randomScale = Random.Range(1f, 20f);
        transform.localScale = Vector3.one * randomScale;

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
}
