using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : WeaponControl
{
    [SerializeField] private TorpedoLauncher torpedoLauncher;
    [SerializeField] private BombLauncher bombLauncher;

    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform launchSite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetTargetHitInfo(torpedoLauncher.MaxDistance);
    }

    protected override void UpdateTargetPosition(Vector3 newTargetPosition)
    {
        torpedoLauncher.TargetPosition = newTargetPosition;
    }

    public void FireSecondaryWeapon()
    {
        torpedoLauncher.FireWeapon(muzzle);
    }

    public void LaunchBomb()
    {
        bombLauncher.LaunchBomb(launchSite);
    }
}
