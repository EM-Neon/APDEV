using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class InputHandlers : MonoBehaviour, ISwipped, IDragged, ISpread, IRotated
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Material[] typeColor = new Material[3];
    [SerializeField] private ParticleSystem particle;
    private ParticleSystem.EmissionModule em;
    public AudioSource spraySFX;

    public Slider slider;
    private List<Color> change = new List<Color>();
    public List<Image> image;
    private Color holder;
    public float resizeSpeed = 5;
    public float rotateSpeed = 1;
    public float maxDistance = 1000;
    private string tagHolder;

    public float time = 0.0f;
    public bool isUlt = false;
    private bool hasTriggered = false;
    private Vector3 TargetPos = Vector3.zero;
    private Vector3 direction;
    ParticleSystem.MainModule beamParticle;

    public float minPressure = 0.0f;
    public float rechargeTime = 0.5f;
    public float minSpeed = 0.05f;
    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        GestureManager.Instance.OnSwipe += OnSwipe;
        GestureManager.Instance.OnDrag += OnDrag;
        beamParticle = particle.main; // utilizes the main module of the particle system
        change.Add(Color.red);  //0
        change.Add(Color.yellow); //1
        change.Add(Color.blue); //2
        change.Add(Color.white);
        beamParticle.startColor = change[2];
        particle.tag = "Regular";
        tagHolder = particle.tag;
        holder = image[0].GetComponent<Image>().color;
        slider.maxValue = 20;
        slider.value = 20;
        particle.Play();
        em = particle.emission;
        checkUpgrades();
    }

    private void FixedUpdate()
    {
        float xValue = Input.acceleration.magnitude;
        if (!GestureManager.Instance.isDrag)
        {
            em.enabled = false;
            onReload();
        }
        // checks accelerometer to see if ultimate is being activated
        if(xValue >= 2.0f && playerStats.canUlt)
        {
            isUlt = true;
            if (!hasTriggered)
            {
                tagHolder = particle.tag;
                holder = image[0].GetComponent<Image>().color;
                hasTriggered = true;
                playerStats.isUlting = true;
            }
            image[0].color = change[3];
            image[1].color = change[3];
        }
    }

    public void OnSwipe(object sender, SwipeEventArgs args)
    {
        if (args.SwipeDirection == SwipeDirections.UP)
        {
            // checks if the current color of the beam matches any of the other colors
            // if it is the same, the color changes to the next color available
            if (holder == change[0])
            {
                holder = change[1];
                particle.tag = "Gas";
            }
            else if (holder == change[1])
            {
                holder = change[2];
                particle.tag = "Regular";
            }
            else if (holder == change[2])
            {
                holder = change[0];
                particle.tag = "Electric";
            }
        }
        else if (args.SwipeDirection == SwipeDirections.DOWN)
        {
            if (holder == change[0])
            {
                holder = change[2];
                particle.tag = "Regular";
            }
            else if (holder == change[1])
            {
                holder = change[0];
                particle.tag = "Electric";
            }
            else if (holder == change[2])
            {
                holder = change[1];
                particle.tag = "Gas";
            }
        }
        beamParticle.startColor = holder;
        for(int i = 0; i < image.Count; i++)
        {
            image[i].GetComponent<Image>().color = holder;
        }
    }

    public void OnDrag(object sender, DragEventArgs args)
    {
        Ray r = Camera.main.ScreenPointToRay(args.TargetFinger.position);
        RaycastHit hit = new RaycastHit();

        Vector3 worldPoint = r.GetPoint(10);

        TargetPos = worldPoint;
        transform.position = worldPoint;
        
        if (Physics.Raycast(r.origin, r.direction, out hit, Mathf.Infinity))
        {
            //Aim(gameObject, hit.point);
            Vector3 pos = r.GetPoint(maxDistance);
            Aim(gameObject, pos);
        }
        else
        {
            Vector3 pos = r.GetPoint(maxDistance);
            Aim(gameObject, pos);
        }



        if (em.enabled == false)
        {
            em.enabled = true;
            spraySFX.Play();
        }

        if (em.enabled == true)
        {
            //checks if player does not have ult activated
            if (!isUlt)
            {
                if(beamParticle.startSpeedMultiplier > minPressure)
                {
                    beamParticle.startSpeedMultiplier -= minSpeed;
                    slider.value -= minSpeed;
                }
                if( beamParticle.startSpeedMultiplier <= minPressure)
                {
                    beamParticle.startSpeedMultiplier = minPressure;
                    slider.value = minPressure;
                }
            }
            else
            {
                time += Time.fixedDeltaTime;
                beamParticle.startSpeedMultiplier = 100;
                slider.value = 20;
                beamParticle.startColor = Color.white;
                beamParticle.gravityModifier = 0;
                playerStats.ultimateCount -= 0.04f;
                // checks if cheats is enabled
                if(time >= 10.0f && playerStats.unlimitedUlti == false)
                {
                    isUlt = false;
                    playerStats.canUlt = false;
                    playerStats.isUlting = false;
                    hasTriggered = false;
                    beamParticle.startSpeedMultiplier = 20;
                    particle.tag = tagHolder;
                    beamParticle.gravityModifier = 1;
                    beamParticle.startColor = holder;
                    image[0].color = holder;
                    image[1].color = holder;
                    time = 0;
                }
            }
        }
    }

    public void onReload()
    {
        if (spraySFX.isPlaying)
        {
            spraySFX.Pause();
        }
        if (beamParticle.startSpeedMultiplier < 20)
        {
            beamParticle.startSpeedMultiplier += rechargeTime;
            slider.value += rechargeTime;
        }
        if(beamParticle.startSpeedMultiplier >= 20)
        {
            beamParticle.startSpeedMultiplier = 20;
            slider.value = 20;
        }
    }

    public void onSubmit()
    {
        playerStats.ResetPoints();
    }
    private void Aim(GameObject obj, Vector3 destination)
    {
        direction = destination - obj.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        particle.transform.rotation = Quaternion.Lerp(particle.transform.rotation, rotation, 1);
    }

    public void checkUpgrades()
    {
        //pressure decrease upgrades
        switch (playerStats.holdLevel[0])
        {
            case 2: minSpeed = 0.009f; break;
            case 3: minSpeed = 0.008f; break;
            case 4: minSpeed = 0.007f; break;
            case 5: minSpeed = 0.006f; break;
            default: minSpeed = 0.01f; break;
        }
        // minimum pressure
        switch (playerStats.holdLevel[1])
        {
            case 2: minPressure = 1; break;
            case 3: minPressure = 3; break;
            case 4: minPressure = 5; break;
            case 5: minPressure = 10; break;
            default: minPressure = 0; break;
        }
        //recharge time
        switch (playerStats.holdLevel[2])
        {
            case 2: rechargeTime = 0.7f; break;
            case 3: rechargeTime = 0.8f; break;
            case 4: rechargeTime = 0.9f; break;
            case 5: rechargeTime = 0.10f; break;
            default: rechargeTime = 0.5f; break;
        }
    }

    public void OnSpread(SpreadEventArgs args)
    {
        float scale = (args.DistanceDelta / Screen.dpi) * resizeSpeed;
        Vector3 scaleVector = new Vector3(scale, scale, scale);
        transform.localScale += scaleVector;
    }

    public void OnRotate(RotateEventArgs args)
    {
        float angle = args.Angle * rotateSpeed;

        if(args.RotationDirection == RotationDirections.CW)
        {
            angle *= -1;
        }
        transform.Rotate(0, 0, angle);
    }
}
