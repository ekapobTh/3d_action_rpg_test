using UnityEngine;

public class UIController : MonoBehaviour
{
    private static UIController _instance;
    public static UIController Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<UIController>();
            return _instance;
        }
    }

    [SerializeField] private HPBar m_HPBar;
    public HPBar GetHpBar() => m_HPBar;

    [SerializeField] private GameObject baseBackground;
    [SerializeField] private StartPanel startPanel;
    [SerializeField] private PlayPanel playPanel;

    private void Awake()
    {
        baseBackground.SetActive(true);
        startPanel.gameObject.SetActive(true);
        playPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && GameManager.Instance.isPlay)
        {
            var isActive = !playPanel.gameObject.activeSelf;

            Cursor.visible = isActive;
            SetActiveBackground(isActive);
            playPanel.gameObject.SetActive(isActive);
        }
    }

    public void SetActiveBackground(bool isShow) => baseBackground.SetActive(isShow);
}
