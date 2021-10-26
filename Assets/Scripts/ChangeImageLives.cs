using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageLives : MonoBehaviour
{

    public void UpdateImage(Sprite m_Sprite)
    {
        GetComponent<Image>().sprite = m_Sprite;
    }
}
