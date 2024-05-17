using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtScript : MonoBehaviour
{
    // scripts
    [Header("Scripts")]
    public ControlManager controlManager;

    void OnMouseOver()
    {
        controlManager.passOnClick = false;
    }
}
