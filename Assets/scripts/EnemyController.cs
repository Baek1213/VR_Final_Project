using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharStats))]
public class EnemyController : MonoBehaviour
{
    [Header("스킬 & 스탯")]
    public Skill skill;
    [Header("현재 상황")]
    public float HP;
    public float skillTimer;
    public float speedMul;

    CharStats stats;
    Animator animator;

    [Header("AI 이동")]
    public Transform targetRedzone;

    void Awake()
    {
        stats = GetComponent<CharStats>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        HP = stats.MaxHP;
        skillTimer = 0;
        speedMul = 2f;
        InvokeRepeating(nameof(ResetSpeed), 4f, 4f);
    }

    void Update()
    {
        float dt = Time.deltaTime;
        skillTimer += dt;

        AutoSkillCast();
        AutoMoveRedzone(dt);

        if (animator)
            animator.SetFloat("movement", 1f);
    }

    void AutoSkillCast()
    {
        if (skillTimer >= stats.SkillCooltime)
        {
            skillTimer = 0;
            skill.Cast();
        }
    }

    void AutoMoveRedzone(float dt)
    {
        if (!targetRedzone)
        {
            GameObject r = GameObject.Find("Redzone");
            if (r) targetRedzone = r.transform;
            if (!targetRedzone) return;
        }

        Vector3 dir = (targetRedzone.position - transform.position);
        dir.y = 0;

        if (dir.sqrMagnitude > 0.01f)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.1f);
        }

        transform.Translate(dir.normalized * dt * stats.MoveSpeed * speedMul, Space.World);
    }

    void ResetSpeed() => speedMul = 2f;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            Destroy(col.gameObject);
            HP = Mathf.Max(HP - 1, 0);
        }
        if (col.gameObject.tag == "poison")
        {
            HP -= 2;
        }
    }
}
