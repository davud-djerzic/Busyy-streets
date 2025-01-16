using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAnimation : MonoBehaviour
{
    public void disableBlackImage()
    {
        gameObject.SetActive(false); // koristi se zbog animacije za pause panel
    }


}
