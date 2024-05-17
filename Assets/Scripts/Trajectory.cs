using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    // trajectory settings
    [Header("Shot Arc Trajectory Settings")]
    [SerializeField] int dotsNumber;
    [SerializeField] GameObject dotsParent;
    [SerializeField] GameObject dotsPrefab;
    [SerializeField] float dotSpacing;
    [SerializeField] [Range(0.01f, 1.5f)] float dotMinScale;
    [SerializeField] [Range(0.5f, 2f)] float dotMaxScale;

    // trajectory variables
    Transform[] dotsList;
    Vector2 pos;
    float TimeStamp;

    void Start()
    {
        PrepareDots();
        Hide();
    }

    void PrepareDots()
    {
        dotsList = new Transform[dotsNumber];
        dotsPrefab.transform.localScale = Vector3.one * dotMaxScale;

        float scale = dotMaxScale;
        float scalefactor = scale / dotsNumber;
        for(int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotsPrefab, null).transform;
            dotsList[i].parent = dotsParent.transform;

            dotsList[i].localScale = Vector3.one * scale;
            if(scale > dotMinScale)
            {
                scale -= scalefactor;
            }
        }
    }

    public void UpdateDots(Vector3 ballPos, Vector2 forceApplied)
    {
        TimeStamp = dotSpacing;
        for(int i = 0; i < dotsNumber; i++)
        {
            pos.x = ballPos.x + forceApplied.x * TimeStamp;
            pos.y = (ballPos.y + forceApplied.y * TimeStamp) - (Physics2D.gravity.magnitude * TimeStamp * TimeStamp) / 2f;
            dotsList[i].position = pos;
            TimeStamp += dotSpacing;
        }
    }

    // arc trajectory visibility functions
    public void Show()
    {
        dotsParent.SetActive(true);
    }

    public void Hide()
    {
        dotsParent.SetActive(false);
    }
}
