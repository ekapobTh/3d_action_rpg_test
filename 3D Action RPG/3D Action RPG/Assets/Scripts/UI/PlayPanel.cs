using UnityEngine;
using UnityEngine.UI;

public class PlayPanel : MonoBehaviour
{
    [SerializeField] private Button replayButton;
    [SerializeField] private Button exitButton;

    void Awake()
    {
        replayButton.onClick.RemoveAllListeners();
        replayButton.onClick.AddListener(OnClickReplay);
        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(Application.Quit);
    }

    private void OnClickReplay()
    {
        UIController.Instance.SetActiveBackground(false);
        gameObject.SetActive(false);
        GameManager.Instance.GameSetup();
        Cursor.visible = false;
    }
}