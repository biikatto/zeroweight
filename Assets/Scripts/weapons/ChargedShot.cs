/* * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * ChargedShot.cs
 *
 * Charged shot weapon. Fire quickly for minimum power,
 * hold and release for charged shot.
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class ChargedShot : Weapon,
                           ICooldown,
                           IEnergyCost{

    public bool debug = true;

    private bool active = true;

    [SerializeField]
    private float weaponDamage = 10f;

    private float energyCost = 0.1f;

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

    private bool charging = false;
    private float chargeBeginTime;
    [SerializeField]
    private float maxChargeTime = 3f;
    [SerializeField]
    private float chargeDamage = 10f;
    [SerializeField]
    private float chargeMass = 2.8f;
    [SerializeField]
    private float chargeSize = 1f;
    [SerializeField]
    private float chargeForce = 1000f;

    [SerializeField]
    private float projectileMass = 0.2f;
    [SerializeField]
    private float projectileSize = 0.5f;
    [SerializeField]
    private float projectileForce = 1000f;
    [SerializeField]
    private float projectileRange = 100f;

    //private PlayerDelegate pDelegate;

    //-------------------------------------------
    //*********** Functions *********************
    //-------------------------------------------
    public void Start(){
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
        //pDelegate = gameObject.GetComponentsInParent<PlayerDelegate>()[0];
    }

    //----------- Firing functions --------------
    public override void BeginFire(){
        if((active)&&(cooldownReady == true)){
            charging = true;
            chargeBeginTime = Time.time;
            soundManager.PlayChargeSound();
        }
    }

    public override void EndFire(){
        if((active)&&(charging)){
            soundManager.StopChargeSound();
            soundManager.PlayLaserSound();
            float chargeTime = Time.time - chargeBeginTime;
            chargeTime = Math.Min(chargeTime, maxChargeTime);

            float chargeLevel = 0f;
            if(chargeTime > 0){
                chargeLevel = Mathf.Min(chargeTime/maxChargeTime);
            }

            GameObject projectile = Instantiate(
                    projectilePrefab,
                    transform.position,
                    transform.rotation) as GameObject;
            projectile.GetComponent<Rigidbody>().mass = projectileMass + (chargeMass * chargeLevel);
            projectile.transform.localScale = Vector3.one * (projectileSize + (chargeSize * chargeLevel));
            projectile.transform.Translate(Vector3.forward * projectile.transform.localScale.x * 4);

            // Set projectile fields
            projectile.GetComponent<KineticProjectile>().kineticDamage = weaponDamage + (chargeDamage * chargeLevel);
            projectile.GetComponent<KineticProjectile>().Origin = transform.parent.parent.gameObject;
            projectile.GetComponent<KineticProjectile>().homingForce = projectileHomingForce;
            projectile.GetComponent<KineticProjectile>().homingRadius = projectileHomingRadius;
            projectile.GetComponent<KineticProjectile>().range = projectileRange;

            projectile.GetComponent<Rigidbody>().AddRelativeForce(
                transform.parent.parent.GetComponent<Rigidbody>().velocity + (
                    Vector3.forward * 10 * projectileMass * projectileForce));
            charging = false;
            BeginCooldown();
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

	public bool Charging{
	    get{
	        return charging;
	    }
	}

	public float ChargeForce{
		get{
			return chargeForce;
		}
		set{
			chargeForce = value;
		}
	}

	public float ChargeSize{
		get{
			return chargeSize;
		}
		set{
			chargeSize = value;
		}
	}

	public float ChargeMass{
		get{
			return chargeMass;
		}
		set{
			chargeMass = value;
		}
	}

	public float ChargeDamage{
		get{
			return chargeDamage;
		}
		set{
			chargeDamage = value;
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
    public float ProjectileRange{
    	get{
    		return projectileRange;
    	}
    	set{
    		projectileRange = Math.Max(value, 0);
    	}
    }

    // Minimum value is 0.
    public float MaxChargeTime{
        get{
            return maxChargeTime;
        }
        set{
            maxChargeTime = Math.Max(value, 0);
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
