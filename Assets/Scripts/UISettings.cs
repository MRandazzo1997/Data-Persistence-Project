using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    public ColorPicker colorPicker;

    // Start is called before the first frame update
    void Start()
    {
        colorPicker.Init();
        if (SaveManager.Instance != null)
            if (!SaveManager.Instance.selectedColor.Equals(Color.black))
                colorPicker.SelectColor(SaveManager.Instance.selectedColor);
            else
                colorPicker.SelectColor(SaveManager.Instance.LoadData<ColorData>().color);
    }

    public void SaveNewColor()
    {
        ColorData colorData = new ColorData();
        colorData.color = colorPicker.SelectedColor;
        SaveManager.Instance.selectedColor = colorData.color;
        SaveManager.Instance.SaveData(colorData);
    }

    public void BackHome()
    {
        SceneManager.LoadScene(0);
    }
}
