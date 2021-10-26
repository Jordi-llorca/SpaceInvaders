using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerScript : MonoBehaviour
{
    public float speed = 2f;
    public MeshRenderer renderer;

    void Update()
    {
        Vector2 offset = new Vector2(0, Time.time * speed);
        renderer.material.mainTextureOffset = offset;
    }
}
