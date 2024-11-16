using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHp : BaseItem
{

    public override void OnInit()
    {
        skinPath = "ItemHp";
        category = 0;
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
        skin.transform.Rotate(Time.deltaTime * rotatespeed, 0, 0);
    }
}
