using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRig : MonoBehaviour
{

    [SerializeField] private GameObject leftXRInteractor;
    [SerializeField] private GameObject rightXRInteractor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateXRInteractors()
    {
        leftXRInteractor.SetActive(true);
        rightXRInteractor.SetActive(true);
    }

    public void DeactivateXRInteractors()
    {
        leftXRInteractor.SetActive(false);
        rightXRInteractor.SetActive(false);
    }
}
