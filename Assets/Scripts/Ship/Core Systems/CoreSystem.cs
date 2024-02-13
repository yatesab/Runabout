using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSystem : MonoBehaviour
{
    [Header("Propulsion System")]
    [SerializeField] protected PropulsionSystem propulsionSystem;

    [Header("Shield System")]
    [SerializeField] protected ShieldSystem shieldSystem;

    [Header("Weapon System")]
    [SerializeField] protected WeaponSystem weaponSystem;

    public enum SystemType
    {
        None,
        Propulsion,
        Shield,
        Weapon
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
