using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    public ColorPicker colorPicker;

    // Start is called before the first frame update
    void Start()
    {
        colorPicker.Init();
        if (SaveManager.Instance.selectedColor != null)
            colorPicker.SelectColor(SaveManager.Instance.selectedColor);
        else
            SaveManager.Instance.LoadData();
    }

    public void SaveNewColor()
    {
        ColorData colorData = new ColorData();
        colorData.color = colorPicker.SelectedColor;
        SaveManager.Instance.SaveData(colorData);
    }
}
