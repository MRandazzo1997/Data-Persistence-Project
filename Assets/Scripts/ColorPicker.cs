using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public Color[] availableColors;
    public Color SelectedColor { get; private set; }
    public Button btnColorPrefab;

    private List<Button> m_ColorButtons;

    // Start is called before the first frame update
    public void Init()
    {
        m_ColorButtons = new List<Button>();
        foreach (var color in availableColors)
        {
            var newButton = Instantiate(btnColorPrefab, transform);
            newButton.image.color = color;

            newButton.onClick.AddListener(() =>
            {
                SelectedColor = color;
                foreach (var btn in m_ColorButtons)
                    btn.interactable = true;

                newButton.interactable = false;
            });
            m_ColorButtons.Add(newButton);
        }
    }

    public void SelectColor(Color color)
    {
        for (int i = 0; i < m_ColorButtons.Count; i++)
            if (m_ColorButtons[i].image.color == color)
                m_ColorButtons[i].onClick.Invoke();
    }
}
