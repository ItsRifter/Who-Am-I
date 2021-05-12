using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Options : MonoBehaviour
{

    public Dropdown resDropdown;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }

        resDropdown.AddOptions(options);
        resDropdown.value = currentResolution;
        resDropdown.RefreshShownValue();
    }

    public void SetResolution(int resIndex)
    {
        Debug.Log("Setting Resolution");

        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        Debug.Log(res.width + " x " + res.height);
    }

    public void SetFullScreen(bool shouldBeFull)
    {
        Debug.Log("Setting full screen to " + shouldBeFull);
        Screen.fullScreen = shouldBeFull;
    }
}
