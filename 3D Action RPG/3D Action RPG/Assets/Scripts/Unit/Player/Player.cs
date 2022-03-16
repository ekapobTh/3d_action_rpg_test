public class Player : UnitBehavior
{
    protected override void Awake()
    {
        base.Awake();
        CameraController.Instance.SetTarget(transform);
        isContinue = true;
        PlayerUpdateHP();
        hurtAction = PlayerUpdateHP;
        deathAction = OnDeath;
    }

    void PlayerUpdateHP() => UIController.Instance.GetHpBar().UpdateHP(unitHP);

    void OnDeath()
    {
        // TODO show menu
    }
}