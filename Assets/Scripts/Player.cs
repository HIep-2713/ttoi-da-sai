using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Grid Settings")]
    public float tileSize = 1f;

    [Header("Swipe Settings")]
    public float swipeThreshold = 50f;
    private Vector2 startTouchPos;

    [Header("Attack Effect")]
    public GameObject effectPrefab;
    public Vector2 effectOffset = Vector2.zero;

    [Header("Audio")]
    public AudioClip attackSfx;
    public AudioClip deadSfx;
    public AudioClip specialSfx;
    public float sfxVolume = 0.8f;

    [Header("Knockback")]
    public float flySpeed = 6f;
    public float knockbackForce = 8f;

    [Header("Mana System")]
    public ManaUI manaUI;
    public int manaPerHit = 20;

    [Header("Special Kill (Super)")]
    public GameObject shockwavePrefab;
    public GameObject superBulletPrefab;
    public float specialRange = 8f;

    private bool isDead = false;
    private bool superUsed = false;   // ✅ tránh spam super khi đầy mana
    private Animator anim;
    private Coroutine knockbackRoutine;
    public static Player Instance;

    void Awake()
    {
        anim = GetComponent<Animator>();
        Instance = this;
    }

    void Update()
    {
        if (!GS.Instance.isPlaying)
        {
            if (anim && !anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                anim.Play("Idle");
            return;
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            HandleMouseInput();
#elif UNITY_ANDROID || UNITY_IOS
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
            HandleTouchInput();
#endif

        // 🔹 Auto super khi đầy mana
        if (manaUI != null && manaUI.IsFull() && !superUsed)
        {
            superUsed = true;
            DoSpecialKill();
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
            startTouchPos = Input.mousePosition;
        else if (Input.GetMouseButtonUp(0))
            DetectSwipe(Input.mousePosition);
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                startTouchPos = touch.position;
            else if (touch.phase == TouchPhase.Ended)
                DetectSwipe(touch.position);
        }
    }

    void DetectSwipe(Vector2 endPos)
    {
        Vector2 delta = endPos - startTouchPos;
        if (delta.magnitude >= swipeThreshold)
        {
            Vector2Int dir = GetCardinal(delta);

            UpdateFacingAnimation(dir);
            DoAttack(dir);
        }
    }

    Vector2Int GetCardinal(Vector2 delta)
    {
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            return delta.x > 0 ? Vector2Int.right : Vector2Int.left;
        else
            return delta.y > 0 ? Vector2Int.up : Vector2Int.down;
    }

    void UpdateFacingAnimation(Vector2Int dir)
    {
        if (!anim) return;

        if (dir == Vector2Int.up) anim.SetTrigger("HitUp");
        else if (dir == Vector2Int.down) anim.SetTrigger("HitDown");
        else if (dir == Vector2Int.left) anim.SetTrigger("HitLeft");
        else if (dir == Vector2Int.right) anim.SetTrigger("HitRight");
    }

    void DoAttack(Vector2Int dir)
    {
        if (effectPrefab)
        {
            Vector3 spawn = transform.position + new Vector3(dir.x, dir.y, 0) * tileSize;
            spawn += (Vector3)effectOffset;

            GameObject fx = Instantiate(effectPrefab, spawn, Quaternion.identity);
            Animator fxAnim = fx.GetComponent<Animator>();
            if (fxAnim)
            {
                if (dir == Vector2Int.up) fxAnim.SetTrigger("AttackUp");
                else if (dir == Vector2Int.down) fxAnim.SetTrigger("AttackDown");
                else if (dir == Vector2Int.left) fxAnim.SetTrigger("AttackLeft");
                else if (dir == Vector2Int.right) fxAnim.SetTrigger("AttackRight");
            }
            Destroy(fx, 0.5f);
        }

        if (attackSfx)
            AudioSource.PlayClipAtPoint(attackSfx, transform.position, sfxVolume);

        if (anim)
            anim.SetTrigger("Punch");

        // ✅ Chỉ tăng mana khi trúng enemy
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            new Vector2(dir.x, dir.y),
            tileSize,
            LayerMask.GetMask("Enemy")
        );

        if (hit.collider != null)
        {
            if (manaUI != null)
            {
                manaUI.GainMana(manaPerHit);
                Debug.Log("🎯 Trúng enemy! Mana + " + manaPerHit +
                          " → Current Mana = " + manaUI.GetCurrentMana());
            }
        }
    }


    // === Special Kill (Super) ===
    void DoSpecialKill()
    {
        if (anim) anim.SetTrigger("Special");
        if (specialSfx) AudioSource.PlayClipAtPoint(specialSfx, transform.position, sfxVolume);

        manaUI.ResetMana();
        DoSuperHit();
    }

    public void DoSuperHit()
    {
        anim.Play("Special");
        StartCoroutine(SuperRoutine());
    }

    private IEnumerator SuperRoutine()
    {
        int waves = 3;
        float delay = 0.6f;
        float bulletLife = 0.5f;

        void KnockbackAllEnemies()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 dir = (enemy.transform.position - transform.position).normalized;
                    rb.AddForce(dir * (knockbackForce * 2f), ForceMode2D.Impulse);
                }
            }
        }

        for (int i = 0; i < waves; i++)
        {
            KnockbackAllEnemies();

            if (shockwavePrefab)
            {
                GameObject fx = Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
                Destroy(fx, bulletLife);
            }

            Vector2[] directions = {
                Vector2.up, Vector2.down, Vector2.left, Vector2.right,
                new Vector2(1,1).normalized, new Vector2(1,-1).normalized,
                new Vector2(-1,1).normalized, new Vector2(-1,-1).normalized
            };

            foreach (Vector2 dir in directions)
            {
                if (!superBulletPrefab) break;

                GameObject bullet = Instantiate(superBulletPrefab, transform.position, Quaternion.identity);
                var sb = bullet.GetComponent<SuperBullet>();
                if (sb != null)
                {
                    sb.knockbackForce = knockbackForce * 1.5f;
                    sb.speed = 8f;
                    sb.Fire(dir);
                }
                Destroy(bullet, bulletLife);
            }

            yield return new WaitForSeconds(delay);
        }

        anim.Play("Idle");
        OnSuperEnd();
    }

    // === Knockback khi player chết ===
    public void Knockback(Vector2 direction, float force)
    {
        if (knockbackRoutine != null) StopCoroutine(knockbackRoutine);
        knockbackRoutine = StartCoroutine(FlyOutRoutine(direction, force));
    }

    private IEnumerator FlyOutRoutine(Vector2 dir, float force)
    {
        if (isDead) yield break;
        isDead = true;

        if (anim) anim.SetTrigger("Dead");
        if (deadSfx) AudioSource.PlayClipAtPoint(deadSfx, transform.position, sfxVolume);

        Camera cam = Camera.main;
        Vector2 screenMin = cam.ViewportToWorldPoint(Vector2.zero);
        Vector2 screenMax = cam.ViewportToWorldPoint(Vector2.one);

        while (transform.position.x > screenMin.x - 1f &&
               transform.position.x < screenMax.x + 1f &&
               transform.position.y > screenMin.y - 1f &&
               transform.position.y < screenMax.y + 1f)
        {
            transform.position += (Vector3)(dir.normalized * force * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isDead)
        {
            Vector2 hitDir = (transform.position - collision.transform.position).normalized;
            Knockback(hitDir, knockbackForce);
            GS.Instance.PlayerDie();
        }
    }

    public void OnSuperEnd()
    {
        GameObject shockwave = GameObject.Find("Shockwave(Clone)");
        if (shockwave != null)
        {
            Destroy(shockwave);
        }

        if (anim)
        {
            anim.ResetTrigger("Special");
            anim.SetTrigger("Idle");
        }

        superUsed = false; // ✅ chỉ reset sau khi super kết thúc
    }
}
