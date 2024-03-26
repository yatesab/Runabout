using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoLauncher : Weapon
{
    [SerializeField] private PhysicsShip physicsShip;
    [SerializeField] private GameObject[] torpedoList;
    [SerializeField] private int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeTorpedoType(float stage)
    {
        selectedWeapon = (int)stage;
    }

    public override void FireWeapon(Transform muzzle)
    {
        AudioManager.instance.Play("Torpedo");

        GameObject torpedo = Instantiate(torpedoList[selectedWeapon], muzzle.position, muzzle.rotation);
        torpedo.GetComponent<Rigidbody>().velocity = physicsShip.Velocity;

        Torpedo torpedoComponent = torpedo.GetComponent<Torpedo>();
        torpedoComponent.Target = HitPoint.transform;
        torpedoComponent.TargetPosition = TargetPosition;
    }
}
