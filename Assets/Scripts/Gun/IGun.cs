using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGun
{
    public GunProperties MyProperties {get;}
    public void Shoot(); 
   
}
