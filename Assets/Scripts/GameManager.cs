using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> spawnPoints = new List<GameObject>();

    [SerializeField]
    GameObject monsterPrefab;

    [SerializeField]
    GameObject rootObject; // 스폰된 몬스터들을 이 오브젝트의 자식으로 설정

    [SerializeField]
    float spawnCooldown = 10f;
    float nowCooldown = 0f;

    private void Update()
    {
        if (spawnPoints.Count > 0)
        {
            if (monsterPrefab != null)
            {
                if (nowCooldown >= spawnCooldown)
                {
                    nowCooldown = 0f;

                    // Random.Range(int a, int b) : a 이상 b 미만
                    int random = Random.Range(0, spawnPoints.Count);
                    Instantiate(monsterPrefab, spawnPoints[random].transform.position, Quaternion.identity, rootObject.transform);
                }
                else
                {
                    nowCooldown += Time.deltaTime;
                }
            }
        }
    }
}
