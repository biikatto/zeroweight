using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon {
	public SoundManager soundManager;

    public abstract float WeaponDamage{
        get;
        set;
    }

    public abstract void BeginFire();
    public abstract void EndFire();
}
