using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoLauncher : MonoBehaviour
{
    [SerializeField] private PhysicsShip physicsShip;
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject[] projectile;

    public float MaxDistance;

    public void FireWeapon(int selectedWeaponType, Vector3 targetPosition)
    {
        AudioManager.instance.Play("Torpedo");

        GameObject torpedo = Instantiate(projectile[selectedWeaponType], muzzle.position, muzzle.rotation);
        torpedo.GetComponent<Rigidbody>().velocity = physicsShip.Velocity;

        Torpedo torpedoComponent = torpedo.GetComponent<Torpedo>();
        torpedoComponent.TargetPosition = targetPosition;
        torpedoComponent.maxDistance = MaxDistance;
    }
}
