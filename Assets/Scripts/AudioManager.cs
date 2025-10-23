using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public enum AudioType
    {
        Music,
        SFX,
        Fighting,
        Menu, 
        Gameplay,
        Hit, 
        Fall, 
        GameOver,
        Victory, 
        Bonus,
        RoundStart, 
        FightStart, 
        Footsteps,
        Die
    }

    public enum AudioSourceType
    {
        Game,
        PlayerInputManager,
    }

    static AudioManager instance;

    public float masterVolume = 1.0f;
    public float musicVolume = 0.8f;
    public float sfxVolume = 0.8f;
    public float volume = 0.8f;
    private AudioSource gameSource;
    private AudioSource playerSource;

    public float AudioSourceVolume
    {
        get { return volume * masterVolume; }
    }

    // void Awake()
    // {
    //     if (instance == null)
    //     {
    //         instance = this;
    //         DontDestroyOnLoad(this.gameObject);
    //     }
    //     else
    //     {
    //         Destroy(this.gameObject);
    //     }
    // }

    public AudioClip GetAudioClip(AudioType audioType, AudioSourceType sourceType)
    {
        // Implement logic to retrieve the appropriate AudioClip based on audioType and sourceType
        return null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameSource = GetComponent<AudioSource>();
        gameSource.volume = AudioSourceVolume;
       // gameSource.clip = GetAudioClip(AudioType.Music, AudioSourceType.Game);
        playerSource = GetComponent<AudioSource>();
        playerSource.volume = AudioSourceVolume;
        //playerSource.clip = GetAudioClip(AudioType.Music, AudioSourceType.PlayerInputManager);
    }

    void PlaySound(AudioType audioType, AudioSourceType sourceType)
    {
        AudioClip clip = GetAudioClip(audioType, sourceType);
        if (clip == null) return;

        AudioSource source = (sourceType == AudioSourceType.Game) ? gameSource : playerSource;
        source.PlayOneShot(clip, AudioSourceVolume);

        if (sourceType == AudioSourceType.Game)
        {
            gameSource.PlayOneShot(clip, AudioSourceVolume);
        }
        else
        {
            playerSource.PlayOneShot(clip, AudioSourceVolume);
        }
    }

    public AudioClip GetClip(AudioType audioType)
    {
        return GetAudioClip(audioType, AudioSourceType.Game);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
