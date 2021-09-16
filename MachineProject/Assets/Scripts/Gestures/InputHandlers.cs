using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class InputHandlers : MonoBehaviour, ISwipped, IDragged, ISpread, IRotated
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject image;
    [SerializeField] private Material[] typeColor = new Material[3];
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AudioSource spraySFX;

    private List<Color> change = new List<Color>();
    private Color holder;
    public float resizeSpeed = 5;
    public float rotateSpeed = 1;
    public float maxDistance = 1000;

    public float time = 0.0f;
    private bool isUlt = false;
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
        beamParticle.startColor = change[2];
        particle.tag = "Regular";
        holder = image.GetComponent<Image>().color;
        particle.Stop();
    }

    private void FixedUpdate()
    {
        float xValue = Input.acceleration.magnitude;
        checkUpgrades();
        if (!GestureManager.Instance.isDrag)
        {
            if (particle.isPlaying)
            {
                particle.Stop();
            }                
            onReload();
        }
        // checks accelerometer to see if ultimate is being activated
        if(xValue >= 2.0f)
        {
            isUlt = true;
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
                image.GetComponent<Image>().color = change[1];
                particle.tag = "Gas";
            }
            else if (holder == change[1])
            {
                image.GetComponent<Image>().color = change[2];
                particle.tag = "Regular";
            }
            else if (holder == change[2])
            {
                image.GetComponent<Image>().color = change[0];
                particle.tag = "Electric";
            }
            holder = image.GetComponent<Image>().color;
        }
        else if (args.SwipeDirection == SwipeDirections.DOWN)
        {
            if (holder == change[0])
            {
                image.GetComponent<Image>().color = change[2];
                particle.tag = "Regular";
            }
            else if (holder == change[1])
            {
                image.GetComponent<Image>().color = change[0];
                particle.tag = "Electric";
            }
            else if (holder == change[2])
            {
                image.GetComponent<Image>().color = change[1];
                particle.tag = "Gas";
            }
            holder = image.GetComponent<Image>().color;
        }

        for (int i = 0; i < change.Count; i++)
        {
            if (holder == change[i])
            {
                beamParticle.startColor = change[i];
            }
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
            Aim(gameObject, hit.point);
        }
        else
        {
            Vector3 pos = r.GetPoint(maxDistance);
            Aim(gameObject, pos);
        }

        if (particle.isStopped)
        {
            particle.Play();
            spraySFX.Play();
        }
        if (particle.isPlaying)
        {
            //checks if player does not have ult activated
            if (!isUlt)
            {
                if(beamParticle.startSpeedMultiplier > minPressure)
                {
                    beamParticle.startSpeedMultiplier -= minSpeed;
                }
                if( beamParticle.startSpeedMultiplier <= minPressure)
                {
                    beamParticle.startSpeedMultiplier = minPressure;
                }
            }
            else
            {
                time += Time.fixedDeltaTime;
                beamParticle.startSpeedMultiplier = 100;
                beamParticle.startColor = Color.white;
                beamParticle.gravityModifier = 0;
                if(time >= 10.0f)
                {
                    isUlt = false;
                    beamParticle.startSpeedMultiplier = 20;
                    beamParticle.gravityModifier = 1;
                    beamParticle.startColor = holder;
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
        }
        if(beamParticle.startSpeedMultiplier >= 20)
        {
            beamParticle.startSpeedMultiplier = 20;
        }
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
            case 2: minSpeed = 0.04f; break;
            case 3: minSpeed = 0.03f; break;
            case 4: minSpeed = 0.02f; break;
            case 5: minSpeed = 0.01f; break;
            default: minSpeed = 0.05f; break;
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
