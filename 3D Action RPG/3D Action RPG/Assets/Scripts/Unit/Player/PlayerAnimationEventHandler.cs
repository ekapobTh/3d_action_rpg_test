using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventHandler : MonoBehaviour
{
    [SerializeField] private Player m_Player;

    private void Awake()
    {
        m_Player = transform.parent.GetComponent<Player>();
    }

    public void OnShowParry()
    {
        m_Player.OnShowParry();
    }

    public void OnStartAnimation()
    {
        m_Player.ClearState();
    }
}
