using UnityEngine;

public class RandomReplacer : MonoBehaviour
{
    [Header("교체할 Hero 프리팹 후보들")]
    [SerializeField] private GameObject[] heroCandidates;

    [Header("라인 이름별 스폰 위치 매핑 (line1→0, line2→1 ...)")]
    [SerializeField] private Transform[] lineSpawns;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Triggered by: {other.name}");
        string name = other.name;            
        int i = int.Parse(name.Replace("Line", "")) - 1;

        Transform player = transform.parent;                    // Player 보관
        var prefab = heroCandidates[Random.Range(0, heroCandidates.Length)];

        Destroy(gameObject);                                     // 나(Hero) 삭제
        var newHero = Instantiate(prefab, lineSpawns[i].position, lineSpawns[i].rotation, player);
        newHero.name = "Hero";                                   // 이름 유지
        FindObjectOfType<Follow1P>().target = newHero.transform;                     // 카메라 타깃 갱신
        var ui = GameObject.FindObjectOfType<cshUI>();
        if (ui) ui.player = newHero;
    }
}