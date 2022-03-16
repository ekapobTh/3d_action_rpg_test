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

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;

    private bool _isPlay = false;
    public bool isPlay => _isPlay;

    void Awake()
    {
        _instance = this;
    }

    public void GameSetup()
    {
        m_UnitSpawner.ClearUnit();
        m_UnitSpawner.SpawnUnit(player, UnitSpawner.UnitType.Player);
        m_UnitSpawner.SpawnUnit(enemy, UnitSpawner.UnitType.Enemy);
        _isPlay = true;
    }
    
    public void GameEnd()
    {
        _isPlay = false;
        Cursor.visible = true;
        UIController.Instance.ShowEndMenu();
    }
}