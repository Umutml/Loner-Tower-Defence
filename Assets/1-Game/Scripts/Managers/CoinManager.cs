using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinManager : MonoBehaviour
{
    public List<GameObject> coinList = new();
    [SerializeField] private GameObject coinPrefab;
    public LayerMask layerMask;
    public LayerMask coinMask;
    [SerializeField] float _radius = 8;
    private Collider[] nonAllocColl = new Collider[20];

    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) CollectClicked();
    }

    private void OnEnable()
    {
        GameEventManager.OnDeathEnemyEvent += SpawnCoin;
        GameEventManager.OnChangedLevelEvent += CollectAll;
    }

    private void OnDisable()
    {
        GameEventManager.OnDeathEnemyEvent -= SpawnCoin;
        GameEventManager.OnChangedLevelEvent -= CollectAll;
    }

    private void OnDrawGizmosSelected()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(hit.point, _radius);
        }
    }

    private void CollectClicked()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Physics.OverlapSphereNonAlloc(hit.point, _radius, nonAllocColl, coinMask);

            foreach (var collider in nonAllocColl)
            {
                if (collider == null) return;
                SoundManager.PlaySound(SoundManager.Sound.Coin,new Vector2(1,1.5f),0.3f);
                DOTween.Kill(collider.gameObject);
                collider.transform.DOMove(new Vector3(0, 10, 0), 0.4f).SetEase(Ease.InOutQuad).OnComplete(() =>
                {
                    GameManager.Instance.GameGold += AbilityController.Instance.GetTotalValue(Enum_Abilities.GoldPerEnemy);
                    Destroy(collider.gameObject);
                    coinList.Remove(collider.gameObject);
                });
            }
        }
    }


    public void SpawnCoin(Enemy enemy)
    {
        if (Random.Range(0, 10) < 5)
        {
            var enemyVec = enemy.transform.position;
            var cloneCoin = Instantiate(coinPrefab, new Vector3(enemyVec.x, 2, enemyVec.z), Quaternion.Euler(90, 0, 0),this.transform);
            coinList.Add(cloneCoin);
            var tempRandom = Random.Range(-3, 3);
            cloneCoin.transform.DOJump(new Vector3(enemyVec.x + tempRandom, 2, enemyVec.z + tempRandom), 10, 1, 1.5f)
                .SetEase(Ease.OutBack);
        }
    }

    public void CollectAll(int level)
    {
        StartCoroutine(IECollectAll());
    }

    private IEnumerator IECollectAll()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (var t in coinList)
        {
            SoundManager.PlaySound(SoundManager.Sound.Coin,new Vector2(1f,2f),0.3f);
            DOTween.Kill(t.transform);
            t.transform.DOMove(new Vector3(0, 10, 0), 0.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                GameManager.Instance.GameGold += AbilityController.Instance.GetTotalValue(Enum_Abilities.GoldPerEnemy);
                Destroy(t);
                coinList.Remove(t);
            });
        }
    }
}