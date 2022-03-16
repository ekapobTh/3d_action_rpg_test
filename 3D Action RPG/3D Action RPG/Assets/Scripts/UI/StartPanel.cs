using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    [SerializeField] private Button playButton;

    void Awake()
    {
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(OnClickPlay);
    }

    private void OnClickPlay()
    {
        UIController.Instance.SetActiveBackground(false);
        gameObject.SetActive(false);
        GameManager.Instance.GameSetup();
        Cursor.visible = false;
    }
}
