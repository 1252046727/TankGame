using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttack : BaseItem
{

    public override void OnInit()
    {
        skinPath = "ItemAttack";
        category = 2;
    }
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        skin.transform.Rotate(0, Time.deltaTime * rotatespeed, 0);
    }
}
