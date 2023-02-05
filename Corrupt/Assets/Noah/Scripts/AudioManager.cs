using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public Sound[] sounds;

    public PlayerController pc;
    public ParticleSystem flame;
    public GameObject footsteps;
    
	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume = s.volume;
		}
	}

	void Start()
	{
        SceneManager.activeSceneChanged += OnSceneChanged;
		Play("MainMusic");
	}

    void OnSceneChanged(Scene current, Scene next)
    {
        pc = FindObjectOfType<PlayerController>();
        flame = GameObject.Find("Flamethrower_PS").GetComponent<ParticleSystem>();
        footsteps = GameObject.Find("Jeff");
    }

	public void Play(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		s.source.Play();
	}

	public void StopPlaying(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		
		if(s == null)
		{
			Debug.LogWarning("Sound:" + name + "not found!");
			return;
		}

		s.source.Stop();
	}

	public void Pause(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		
		if(s == null)
		{
			Debug.LogWarning("Sound:" + name + "not found!");
			return;
		}

		s.source.Pause();
	}

    void Update()
    {
        if (pc.fuel > 0 && !pc.recharging && flame.emission.rateOverTime.constant == 200) 
        {
            if (!flame.gameObject.GetComponent<AudioSource>().enabled)
            {
                flame.gameObject.GetComponent<AudioSource>().enabled = true;
            }

            if (flame.gameObject.GetComponent<AudioSource>().volume <= 0f)
            {
                flame.gameObject.GetComponent<AudioSource>().volume = 1f;
            }

            if (!flame.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                float randomPitch = UnityEngine.Random.Range(0.9f, 1.1f);
                flame.gameObject.GetComponent<AudioSource>().pitch = randomPitch;
                flame.gameObject.GetComponent<AudioSource>().time = 1f;
                flame.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else 
        {
            if (flame.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                flame.gameObject.GetComponent<AudioSource>().Stop();
            }
            if (flame.gameObject.GetComponent<AudioSource>().enabled)
            {
                flame.gameObject.GetComponent<AudioSource>().enabled = false;
            }
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (!footsteps.gameObject.GetComponent<AudioSource>().enabled)
            {
                footsteps.gameObject.GetComponent<AudioSource>().enabled = true;
            }

            if (footsteps.gameObject.GetComponent<AudioSource>().volume <= 0f)
            {
                footsteps.gameObject.GetComponent<AudioSource>().volume = 1f;
            }

            if (!footsteps.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                float randomPitch = UnityEngine.Random.Range(0.9f, 1.1f);
                footsteps.gameObject.GetComponent<AudioSource>().pitch = randomPitch;
                footsteps.gameObject.GetComponent<AudioSource>().time = 0.3f;
                footsteps.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            if (footsteps.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                footsteps.gameObject.GetComponent<AudioSource>().Stop();
            }
            if (footsteps.gameObject.GetComponent<AudioSource>().enabled)
            {
                footsteps.gameObject.GetComponent<AudioSource>().enabled = false;
            }
        }
    }
}