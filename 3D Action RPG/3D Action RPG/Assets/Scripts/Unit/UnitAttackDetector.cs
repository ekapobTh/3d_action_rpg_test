using UnityEngine;

public class UnitAttackDetector : MonoBehaviour
{
    private UnitBehavior m_UnitBehavior;

    private void Awake()
    {
        m_UnitBehavior = transform.parent.GetComponent<UnitBehavior>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(UnitTag.PLAYER) || other.tag.Equals(UnitTag.ENEMY))
            m_UnitBehavior.SetAttackTarget(other.GetComponent<UnitBehavior>());
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(UnitTag.PLAYER) || other.tag.Equals(UnitTag.ENEMY))
            m_UnitBehavior.SetAttackTarget(null);
    }
}