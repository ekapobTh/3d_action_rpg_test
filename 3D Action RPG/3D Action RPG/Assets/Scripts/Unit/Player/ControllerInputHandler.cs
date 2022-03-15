using UnityEngine;

public class ControllerInputHandler : MonoBehaviour
{
    private Player m_player;

    // Start is called before the first frame update
    void Awake()
    {
        m_player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        if (m_player.IsDeath())
            return;
        Vector2 inputV = Vector2.zero;
        var inputX = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");

        inputV.x = inputX;
        inputV.y = inputY;

        m_player.SetInputVector(inputV);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            m_player.Attack();
        else if (Input.GetKeyDown(KeyCode.Mouse1))
            m_player.Parry();

        if (Input.GetKeyUp(KeyCode.Mouse0))
            m_player.UnAttack();
        if (Input.GetKeyUp(KeyCode.Mouse1))
            m_player.UnParry();
    }
}