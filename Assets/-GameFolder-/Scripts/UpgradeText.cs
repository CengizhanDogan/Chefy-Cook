using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpgradeText : MonoBehaviour
{
    [SerializeField] private GameObject textPreFab;

    private GameObject textPostFab;
    public void SpawnText()
    {
        if (!textPostFab)
        {
            var spawnPos = transform.position;
            spawnPos.y += 1f;

            textPostFab = Instantiate(textPreFab, spawnPos, textPreFab.transform.rotation, transform);

            var scale = textPostFab.transform.localScale;
            textPostFab.transform.localScale = Vector3.zero;

            textPostFab.transform.DOScale(scale, 0.5f).OnComplete(() =>
            {
                spawnPos.y += 0.25f;
                textPostFab.transform.DOLocalMoveY(spawnPos.y, 0.5f)
                .SetLoops(3, LoopType.Yoyo).OnComplete(() =>
                {
                    textPostFab.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
                    {
                        Destroy(textPostFab);
                    });
                });
            });
        }
    }
}
