using UnityEngine;

public class EnemyHPBar : HPBar
{
    [SerializeField] private Enemy m_Enemy;
    private Transform target;

    private void Awake()
    {
        target = Camera.main.transform;   
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (target)
            transform.LookAt(target);
    }
}