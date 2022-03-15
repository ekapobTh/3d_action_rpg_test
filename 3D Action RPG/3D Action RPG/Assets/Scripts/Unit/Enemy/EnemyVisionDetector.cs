using UnityEngine;

public class EnemyVisionDetector : MonoBehaviour
{
    [SerializeField] private Enemy m_Enemy;
    private bool _isDetected = false;

    private void Awake()
    {
        m_Enemy.DetectorRefresh = OnRefresh;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDetected || m_Enemy.isOutOfSafeArea)
            return;
        if (other.tag.Equals(UnitTag.PLAYER))
        {
            m_Enemy.SetTarget(other.transform);
            _isDetected = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isDetected || m_Enemy.isOutOfSafeArea)
            return;
        if (other.tag.Equals(UnitTag.PLAYER))
        {
            m_Enemy.SetTarget(other.transform);
            _isDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!_isDetected)
            return;
        if (other.tag.Equals(UnitTag.PLAYER))
        {
            m_Enemy.SetTarget(null);
            _isDetected = false;
        }
    }

    private void OnRefresh() => _isDetected = false;
}