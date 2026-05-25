using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    private static SoundsManager m_Instance;

    public AudioSource m_AudioSource;
    public AudioClip Coin;
    public AudioClip  Button;
    public AudioClip Slide;
    public AudioClip Hit;
    public AudioClip Jump;

    private void Awake()
    {
        m_Instance = this;
    }

    public static void SoundCoin()
    {
        m_Instance.m_AudioSource.PlayOneShot(m_Instance.Coin, 0.4f);  
    }

    public static void SoundButton()
    {
        m_Instance.m_AudioSource.PlayOneShot(m_Instance.Button);
    }

    public static void SoundHit()
    {
        m_Instance.m_AudioSource.PlayOneShot(m_Instance.Hit);
    }

    public static void SoundJump()
    {
        m_Instance.m_AudioSource.PlayOneShot(m_Instance.Jump);
    }

    public static void SoundSlide()
    {
        m_Instance.m_AudioSource.PlayOneShot(m_Instance.Slide);
    }


}