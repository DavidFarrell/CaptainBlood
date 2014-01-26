using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {

	public GameObject trapObjPlayerOne;
	public GameObject trapObjPlayerTwo;

	public enum Weapon{
		Trap,
		Trampoline,
		BlowGun,
		ElectroStunner
	};
	public Weapon weapon = Weapon.Trap;
	
	void OnTriggerEnter2D( Collider2D other ){
		if (other.transform.tag == "Player") {
			if ( other.transform.GetComponent<PlayerController>().heldWeapon == null ){

				switch ( weapon ){

				case Weapon.Trap:

					if ( other.transform.GetComponent<PlayerController>().playerNumber == 1 ){
						other.transform.GetComponent<PlayerController>().heldWeapon = trapObjPlayerOne;
						other.transform.GetComponent<PlayerController>().weaponRemaining = 3;
					}else{
						other.transform.GetComponent<PlayerController>().heldWeapon = trapObjPlayerTwo;
						other.transform.GetComponent<PlayerController>().weaponRemaining = 3;
					}

					break;

				};

				Destroy( gameObject );

			}	
		}
	}
}
