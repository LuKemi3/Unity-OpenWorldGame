using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("�ƶ�����")]
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("�Ų�������")]
    [SerializeField] private AudioSource audioSource;
    public AudioClip leftFootSound;
    public AudioClip rightFootSound;
    [Range(0f, 1f)]                    // ��ӻ��鷶Χ
    public float footstepVolume = 0.5f; // �Ų�������
    [Range(0.1f, 2f)]                  // ��Ӳ�Ƶ���鷶Χ
    public float footstepDelay = 0.5f;  // �Ų������

    // ˽�б���
    private bool isWalking = false;
    private float footstepTimer = 0f;
    private bool isLeftFoot = true;
    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        // ��ʼ��AudioSource
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = footstepVolume; // ʹ���趨������
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

        // ������·��Ч
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

        // ʵʱ�������������������ʱ��Inspector�е�����������
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