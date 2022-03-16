public class Player : UnitBehavior
{
    protected override void Awake()
    {
        base.Awake();
        CameraController.Instance.SetTarget(transform);
        isContinue = true;
        UIController.Instance.GetHpBar().ForceUpdateHP(unitHP);
        hurtAction = PlayerUpdateHP;
    }

    void PlayerUpdateHP() => UIController.Instance.GetHpBar().UpdateHP(unitHP);
}