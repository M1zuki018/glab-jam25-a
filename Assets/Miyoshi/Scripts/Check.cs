using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : EnemyRotation
{
    private float timer;

    public override void Update()
    {
        // まず親クラスのUpdateを実行して、intervalを更新
        base.Update();

        // その後、子クラス側の処理
        timer += Time.deltaTime;
        if (timer >=2)
        {
            Debug.Log("Check: " + interval);
            timer = 0f;
        }
    }
}
