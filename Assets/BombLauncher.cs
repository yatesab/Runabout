using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLauncher : MonoBehaviour
{
    [SerializeField] private PhysicsShip physicsShip;
    [SerializeField] private Transform launchSite;
    [SerializeField] private GameObject projectile;
    public float MaxDistance;

    public void FireWeapon(int selectedWeaponType)
    {
        AudioManager.instance.Play("Torpedo");

        GameObject bomb = Instantiate(projectile, launchSite.position, launchSite.rotation);
        bomb.GetComponent<Rigidbody>().velocity = physicsShip.Velocity;
    }
}
