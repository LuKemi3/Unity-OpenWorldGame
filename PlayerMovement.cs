using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("移动参数")]
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("脚步声设置")]
    [SerializeField] private AudioSource audioSource;
    public AudioClip leftFootSound;
    public AudioClip rightFootSound;
    [Range(0f, 1f)]                    // 添加滑块范围
    public float footstepVolume = 0.5f; // 脚步声音量
    [Range(0.1f, 2f)]                  // 添加步频滑块范围
    public float footstepDelay = 0.5f;  // 脚步声间隔

    // 私有变量
    private bool isWalking = false;
    private float footstepTimer = 0f;
    private bool isLeftFoot = true;
    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        // 初始化AudioSource
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = footstepVolume; // 使用设定的音量
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // 处理走路音效
        if (isGrounded && (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f))
        {
            isWalking = true;
            if (footstepTimer <= 0)
            {
                PlayFootstepSound();
                footstepTimer = footstepDelay;
                isLeftFoot = !isLeftFoot;
            }
        }
        else
        {
            isWalking = false;
        }

        if (footstepTimer > 0)
        {
            footstepTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // 实时更新音量（如果在运行时在Inspector中调整了音量）
        audioSource.volume = footstepVolume;
    }

    private void PlayFootstepSound()
    {
        if (audioSource != null)
        {
            AudioClip currentStepSound = isLeftFoot ? leftFootSound : rightFootSound;
            if (currentStepSound != null)
            {
                audioSource.clip = currentStepSound;
                audioSource.Play();
            }
        }
    }
}