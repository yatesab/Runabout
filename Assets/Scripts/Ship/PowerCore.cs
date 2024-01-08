using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCore : MonoBehaviour
{
    [SerializeField] private PowerPanel powerPanel;
    [SerializeField] private float powerCanisters = 2f;
    [Header("Transporter System")]
    [SerializeField] private TransporterSystem transporterSystem;

    [Header("Propulsion System")]
    [SerializeField] private PropulsionSystem propulsionSystem;

    public void PatchSystemsTogether(string test)
    {
        Debug.Log(test);
    }
}
