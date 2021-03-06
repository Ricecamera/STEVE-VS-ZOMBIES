using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : AmmoGun 
{
    [SerializeField]
    private float coneAngleWidth = 60;

    [SerializeField]
    private int bulletCount = 7;

    public LayerMask collisionMask;
    private void FireBullets() {
        float dTheta = coneAngleWidth / (bulletCount - 1);
        for (int i = 1; i <= bulletCount; i++) {
            // Calculate the radian the relativeVec have to be rotate
            float deg = dTheta * (i - (bulletCount + 1) / 2);

            // Spawn bullet
            Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.transform.Rotate(Vector3.up, deg);
            bullet.Speed = muzzleVelocity;

        }
    }

    //protected override void Update() {
    //    float dTheta = coneAngleWidth / (rayCount - 1);
    //    for (int i = 1; i <= rayCount; i++) {
    //        // Get forward vector relative to gun direction
    //        Vector3 relativeVec = transform.forward;
    //        // Calculate the radian the relativeVec have to be rotate
    //        float radian = Mathf.Deg2Rad * (dTheta * (i - (rayCount + 1) / 2));
    //        // Rotate along Y-axis
    //        float newX = relativeVec.x * Mathf.Cos(radian) + relativeVec.z * Mathf.Sin(radian);
    //        float newZ = -relativeVec.x * Mathf.Sin(radian) + relativeVec.z * Mathf.Cos(radian);
    //        Vector3 dir = new Vector3(newX, relativeVec.y, newZ);

    //        Debug.DrawRay(firePoint.position, shotDistance * dir, Color.green);
    //    }
    //    base.Update();
    //}


    public override void Shoot() {

        if (IsMagazineEmpty() && gameObject) {
            if (!IsBulletEmpty() && !isReloading) {
                StartCoroutine(Reload());
            }
            return;
        }

        // check if current Time is able to shoot
        if (CanShoot() && !IsMagazineEmpty()) {
            AudioManager.instance?.PlaySingle(fireSound);
            FireBullets();

            // reduce bullets in magazine by one
            currentMagazine--;
            OnShoot?.Invoke(currentMagazine, reserveAmmo);
            ResetTimer();
        }
    }
}
