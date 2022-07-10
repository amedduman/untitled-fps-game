using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraEntity : MonoBehaviour
{
    private void Awake()
    {
        DependencyProvider.Instance.Register(this);
    }
}
