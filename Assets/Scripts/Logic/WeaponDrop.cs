using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponDrop : Item
{       

    public override void ONPickup(Collider other) {
        GunController controller = other.GetComponent<GunController>();
        if (controller != null) {
            AmmoGun selectedGun = Randomizer.instance.getRandomGun();
            controller.EquipGun(selectedGun);
        }
        
        CancelInvoke("DestroySelf");
        Invoke("DestroySelf", 0f);
    }

    
}
