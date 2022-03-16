using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private float smoothFactor = 5f;
    [SerializeField] private Slider m_Slider;
    [SerializeField] private Text m_Text;
    [SerializeField] private GameObject fillGameObject;
    private float currentHp;
    private float newHp;
    private bool isUpdate = false;
    private float t = 0f;

    public void UpdateHP(int hp)
    {
        currentHp = m_Slider.value;
        newHp = ((float)hp) / 100f;
        isUpdate = true;
        t = 0f;
    }

    public void ForceUpdateHP(int hp)
    {
        fillGameObject.SetActive(true);
        m_Slider.value = ((float)hp) / 100f;
        m_Text.text = (m_Slider.value * 100f).ToString("F0");
    }

    protected virtual void Update()
    {
        if (!isUpdate)
            return;
        t += smoothFactor * Time.deltaTime;
        if (t >= 1f)
        {
            t = 1f;
            isUpdate = false;
        }
        m_Slider.value = Mathf.Lerp(currentHp, newHp, t);
        m_Text.text = (m_Slider.value * 100f).ToString("F0");
        if (m_Slider.value == 0f)
        {
            m_Text.text = "DEAD";
            fillGameObject.SetActive(false);
        }
    }
}