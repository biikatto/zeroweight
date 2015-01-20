public interface IWeapon{
    float WeaponDamage{
        get;
        set;
    }

    void BeginFire();
    void EndFire();
}
