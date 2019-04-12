using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class GameObjectExtension
{
    public static void SetActiveTrueWithAnimation(this GameObject go)
    {
        var old_y_pos = go.transform.position.y;
        var old_scale = go.transform.localScale;
        var new_scale = old_scale * 1.085f;
        //go.transform.DOLocalMoveY(go.transform.position.y + 2.5f, 0.5f);
        //
        go.SetActive(true);
        go.transform.localScale = new Vector3(0, 0, 0);
        go.transform.DOScale(new_scale, 0.50f).OnComplete(() => { go.transform.DOScale(old_scale, 0.12f); });
        //go.transform.DOLocalMoveY(old_y_pos, 0.5f).
        //    OnComplete(() => { go.transform.DOScale(old_scale, 0.5f); });        
    }
    
}
