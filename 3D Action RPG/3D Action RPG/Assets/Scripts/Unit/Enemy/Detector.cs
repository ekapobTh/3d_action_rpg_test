using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private Enemy m_Enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(UnitTag.PLAYER))
            m_Enemy.SetTarget(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(UnitTag.PLAYER))
            m_Enemy.SetTarget(null);
    }
}
