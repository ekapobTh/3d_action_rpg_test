using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private HPBar m_HPBar;
    public HPBar GetHpBar() => m_HPBar;
}
