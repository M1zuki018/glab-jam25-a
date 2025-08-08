using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : EnemyRotation
{
    private float timer3;

    public override void Update()
    {
        // まず親クラスのUpdateを実行して、intervalを更新
        base.Update();

        // その後、子クラス側の処理
        timer3 += Time.deltaTime;
        if (timer3 >=3)
        {
            //Debug.Log("Check: " + interval1);
            timer3 = 0f;
        }
    }
}
