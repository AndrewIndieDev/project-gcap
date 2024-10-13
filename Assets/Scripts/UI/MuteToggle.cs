using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteToggle : MonoBehaviour
{
    public Image icon;
    [Space]
    public Sprite muteIcon;
    public Sprite unMuteIcon;

    bool _toggle = true;
    
    public void ToggleMute()
    {
        _toggle = !_toggle;
        
        if(_toggle)
            icon.sprite = muteIcon;
        else
            icon.sprite = unMuteIcon;

        AudioListener.pause = _toggle;
    }
}
