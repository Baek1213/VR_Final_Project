// PlayerControllerSimple.cs
using UnityEngine;

[RequireComponent(typeof(CharStats))]
public class cshPlayerController : MonoBehaviour
{
    [Header("래퍼런스")]
    public GameObject UI;
    public Skill skill;
    Animator animator;

    [Header("현재상황")]
    public float HP;
    public float skillTimer;
    public float speedMul;
    CharStats stats;

    void Awake()
    {
        stats = GetComponent<CharStats>();
        if (!UI) UI = GameObject.Find("UI");
        if (!animator) animator = GetComponent<Animator>();
    }

    void Start()
    {
        HP = stats.MaxHP;
        skillTimer = 0f;
        if (animator) animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        speedMul = 2f;                               // ★ 변경: 시작 기본값 세팅
        InvokeRepeating(nameof(RefreshSpeedMul), 4f, 4f); // ★ 변경: 4초 뒤부터 4초 간격
    }

    void Update()
    {
        float dt = Time.deltaTime;
        skillTimer += dt;

        if (Time.timeScale == 0)
        {
            // 승패 처리/총알 정리는 필요 시 별도 이벤트로 빼도 됨
        }

        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) dir += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) dir += Vector3.back;
        if (Input.GetKey(KeyCode.A)) dir += Vector3.left;
        if (Input.GetKey(KeyCode.D)) dir += Vector3.right;

        if (Input.GetKeyDown(KeyCode.Space) && skillTimer >= stats.SkillCooltime)
        {
            skillTimer = 0f;
            skill.Cast();
        }

        if (dir.sqrMagnitude > 1f) dir.Normalize();

        if (dir.sqrMagnitude > 0.01f)
        {
            var q = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, q, 0.1f);
        }
        transform.Translate(dir * dt * stats.MoveSpeed * speedMul, Space.World);

        if (animator && gameObject.CompareTag("hero"))
            animator.SetFloat("movement", dir.magnitude * speedMul);
    }

    void RefreshSpeedMul() { speedMul = 2f; }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            Destroy(col.gameObject);
            HP = Mathf.Max(0, HP - 1f);
        }
    }
}
