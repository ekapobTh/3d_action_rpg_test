public class Player : UnitBehavior
{
    protected override void Awake()
    {
        base.Awake();
        CameraController.Instance.SetTarget(transform);
        isContinue = true;
    }
}