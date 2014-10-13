using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public GameObject weaponBeam;
	public float cooldownTime = 0.75f;
	public float weaponDamage = 10f;
	public float projectileForce = 1000f;
	public float projectileSize = 1f;
	public float projectileMass = 1f;
	
	private float cooldown;
	private bool checkForHit;
	private bool destroyed = false;
	private GameObject projectilePrefab;

	public bool debug = true;

	void Start(){
		projectilePrefab = Resources.Load("Prefabs/Projectile") as GameObject;
	}

	public void Fire(){
		if(!destroyed){
			if(cooldown == 0){
				GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation) as GameObject;
				projectile.rigidbody.mass = projectileMass;
				projectile.transform.localScale = Vector3.one * projectileSize;
				projectile.transform.Translate(Vector3.forward * projectileSize);
				projectile.GetComponent<KineticProjectile>().kineticDamage = weaponDamage;

				projectile.rigidbody.AddRelativeForce(transform.parent.parent.rigidbody.velocity + (Vector3.forward * 10 * projectileMass * projectileForce));

				cooldown += cooldownTime;

				checkForHit = true;
			}
		}
	}

	void ManageTimers(){
		if(cooldown > 0){
			cooldown -= Time.deltaTime;
		}
		if(cooldown < 0){
			cooldown = 0;
		}
	}
	
	void Update(){
		ManageTimers();
	}

	public void Destruct(){
		destroyed = true;
	}
}
