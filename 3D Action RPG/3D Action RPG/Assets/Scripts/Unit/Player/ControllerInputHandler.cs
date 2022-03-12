using System.Collections;
using System.Collections.Generic;
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
        Vector2 inputV = Vector2.zero;
        var inputX = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");

        if (inputX > 0f)
            inputV.x = 1f;
        else if (inputX < 0f)
            inputV.x = -1f;
        else
            inputV.x = 0f;
        inputV.y = inputY;

        m_player.SetInputVector(inputV);

        if (Input.GetKeyDown(KeyCode.Space))
            m_player.Attack();
    }
}
