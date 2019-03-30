using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float bgScrollSpeedY = 0.5f;
    [SerializeField] float bgScrollSpeedX = 0f;
    Material myMaterial;
    Vector2 offset;

    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(bgScrollSpeedX, bgScrollSpeedY);
    }


    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
