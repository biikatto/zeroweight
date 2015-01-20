public interface ICooldown{
    float CooldownTime{
        get;
        set;
    }

    float MinimumCooldown{
        get;
    }

    bool CooldownReady{
        get;
    }

    void BeginCooldown();
}
