using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLauncher : MonoBehaviour
{
    [SerializeField] private GameObject[] bombList;
    [SerializeField] private PhysicsShip physicsShip;
    [SerializeField] private int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchBomb(Transform launchSite)
    {
        AudioManager.instance.Play("Torpedo");

        GameObject bomb = Instantiate(bombList[selectedWeapon], launchSite.position, launchSite.rotation);
        bomb.GetComponent<Rigidbody>().velocity = physicsShip.Velocity;
    }
}
