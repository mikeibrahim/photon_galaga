using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct weaponType {
    public string weaponName;

    public weaponType (string n) {
        this.weaponName = n;
    }
}

public static class Weapons {
	public static int RIFLE = 0;

    public static weaponType[] weapons = new weaponType[] {
        //          weaponName
        new weaponType("Rifle"),
    };
}

public class Weapon : MonoBehaviour {
	int currentGun;

	public void SetUpGun() {

	}

    void Start() {
        
    }

    void Update() {
        
    }
}
