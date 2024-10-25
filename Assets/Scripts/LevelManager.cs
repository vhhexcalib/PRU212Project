using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
<<<<<<< HEAD
=======
    public Transform startPoint;
>>>>>>> devhoang
    public Transform[] path;

    private void Awake()
    {
        main = this;
    }
}
