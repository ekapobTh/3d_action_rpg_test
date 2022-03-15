using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
    }

    [SerializeField] private UnitSpawner m_UnitSpawner;
    [SerializeField] private UIController m_UIController;

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        var cameraScript = Camera.main.GetComponent<CameraController>();

        Cursor.visible = false;
        // cameraScript.SetTarget(); setup player & show start button
    }

    public void SetUIHP(int hp) => m_UIController.GetHpBar().UpdateHP(hp);
}