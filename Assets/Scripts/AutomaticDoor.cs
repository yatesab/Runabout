using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    private Animator _animator;

    public bool forceClosed = true;

    public bool Open = false;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateForceClose(bool forceClosedUpdate)
    {
        forceClosed = forceClosedUpdate;
    }

    public void OpenDoor()
    {
        _animator.SetBool("character_nearby", true);
    }

    public void CloseDoor()
    {
        _animator.SetBool("character_nearby", false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (_animator != null && !forceClosed)
        {
            OpenDoor();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (_animator != null)
        {
            CloseDoor();
        }
    }
}
