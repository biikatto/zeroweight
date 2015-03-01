/* * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * RapidFire.cs
 *
 * Rapid fire weapon. Hold down the fire button to
 * fire as quickly as possible.
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class RapidFire : Weapon,
                         ICooldown,
                         IEnergyCost{

    public bool debug = true;

    private bool active = true;

    [SerializeField]
    private float weaponDamage = 10f;

    private float energyCost = 0.1f;

    private bool firing = false;

    [SerializeField]
    private float projectileHomingRadius = 1000f;
    [SerializeField]
    private float projectileHomingForce = 1000f;
    [SerializeField]
    private string projectileColor = "Red";
    private GameObject projectilePrefab;

    private bool cooldownReady = true;
    private float minimumCooldown = 0.001f;
    [SerializeField]
    private float cooldownTime = 0.2f;

    [SerializeField]
    private float projectileMass = 0.2f;
    [SerializeField]
    private float projectileSize = 0.5f;
    [SerializeField]
    private float projectileForce = 1000f;

    private PlayerDelegate pDelegate;
    //-------------------------------------------
    //*********** Functions *********************
    //-------------------------------------------
    void Start(){
        if(projectileColor == "Blue"){
            projectilePrefab = Resources.Load(
            "Prefabs/Bullet_Blue") as GameObject;
        }else if(projectileColor == "Green"){
            projectilePrefab = Resources.Load(
            "Prefabs/Bullet_Green") as GameObject;
        }else if(projectileColor == "Red"){
            projectilePrefab = Resources.Load(
            "Prefabs/Bullet_Red") as GameObject;
        }else{  // default to yellow
            projectilePrefab = Resources.Load(
            "Prefabs/Bullet_Yellow") as GameObject;
        }
        pDelegate = gameObject.GetComponentsInParent<PlayerDelegate>()[0];
    }


    //----------- Firing functions --------------

    // Begin fully-automatic fire
    public override void BeginFire(){
        if(active){
            firing = true;
        }
    }

    // End fully-automatic fire
    public override void EndFire(){
        firing = false;
    }

    // Fire a single shot
    private void FireShot(){
    	soundManager.PlayLaserSound();
        GameObject projectile = Instantiate(
                projectilePrefab,
                transform.position,
                transform.rotation) as GameObject;
        projectile.rigidbody.mass = projectileMass;
        projectile.transform.localScale = Vector3.one * projectileSize;
        projectile.transform.Translate(Vector3.forward * projectile.transform.localScale.x * 4);
        projectile.GetComponent<KineticProjectile>().kineticDamage = weaponDamage;
        projectile.GetComponent<KineticProjectile>().Origin = transform.parent.parent.gameObject;
        projectile.GetComponent<KineticProjectile>().homingForce = projectileHomingForce;
        projectile.GetComponent<KineticProjectile>().homingRadius = projectileHomingRadius;

        projectile.rigidbody.AddRelativeForce(
                transform.parent.parent.rigidbody.velocity + (
                    Vector3.forward * 10 * projectileMass * projectileForce));
        BeginCooldown();
    }

    public void Update(){
        if((firing)&&(cooldownReady)){
            FireShot();
        }
    }

    //----------- Cooldown functions ------------
    public void BeginCooldown(){
        StartCoroutine(ExecuteCooldown());
    }

    private IEnumerator ExecuteCooldown(){
        cooldownReady = false;
        yield return new WaitForSeconds(cooldownTime);
        cooldownReady = true;
    }

    //----------- Active ------------------------
    public void Activate(){
        active = true;
    }

    public void Deactivate(){
        active = false;
    }

    // Legacy
    public void Destruct(){
        Deactivate();
    }

    //-------------------------------------------------
    //*********** Property implementations ************
    //-------------------------------------------------

    public bool Active{
		get{
			return active;
		}
		set{
			active = value;
		}
    }


	public float ProjectileForce{
		get{
			return projectileForce;
		}
		set{
			projectileForce = value;
		}
	}

	public float ProjectileSize{
		get{
			return projectileSize;
		}
		set{
			projectileSize = value;
		}
	}

	public float ProjectileMass{
		get{
			return projectileMass;
		}
		set{
			projectileMass = value;
		}
	}

    public string ProjectileColor{
        get{
            return projectileColor;
        }
        set{
            projectileColor = value;
            if(projectileColor == "Blue"){
                projectilePrefab = Resources.Load(
                "Prefabs/Bullet_Blue") as GameObject;
            }else if(projectileColor == "Green"){
                projectilePrefab = Resources.Load(
                "Prefabs/Bullet_Green") as GameObject;
            }else if(projectileColor == "Red"){
                projectilePrefab = Resources.Load(
                "Prefabs/Bullet_Red") as GameObject;
            }else{  // default to yellow
                projectilePrefab = Resources.Load(
                "Prefabs/Bullet_Yellow") as GameObject;
            }
        }
    }

    // Minimum value is 0.
    public float ProjectileHomingForce{
        get{
            return projectileHomingForce;
        }
        set{
            projectileHomingForce = Math.Max(value, 0);
        }
    }

    // Minimum value is 0.
    public float ProjectileHomingRadius{
        get{
            return projectileHomingRadius;
        }
        set{
            projectileHomingRadius = Math.Max(value, 0);
        }
    }

    // Minimum value is 0.
    public float EnergyCost{
        get{
            return energyCost;
        }
        set{
            energyCost = Math.Max(value, 0);
        }
    }

    //----------- MinimumCooldown----------------
    // Minimum value is 0.001f.
    public float MinimumCooldown{
        get{
            return minimumCooldown;
        }
        set{
            minimumCooldown = Math.Max(value, 0.001f);
        }
    }

    // Minimum value is minimumCooldown.
    // Absolute minimum is 0.001f.
    public float CooldownTime{
        get{
            return cooldownTime;
        }
        set{
            cooldownTime = Math.Max(value, minimumCooldown);
        }
    }

    public bool CooldownReady{
        get{
            return cooldownReady;
        }
    }

    public override float WeaponDamage{
        get{
            return weaponDamage;
        }
        set{
            weaponDamage = Math.Max(value, 0f);
        }
    }
}
