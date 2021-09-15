using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandlers : MonoBehaviour, ISwipped, IDragged, ISpread, IRotated
{
    [SerializeField] private GameObject beam;
    [SerializeField] private GameObject image;
    [SerializeField] private Material[] typeColor = new Material[3];
    [SerializeField] private ParticleSystem particle;

   public Camera camera;

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

    void Start()
    {
        GestureManager.Instance.OnSwipe += OnSwipe;
        GestureManager.Instance.OnDrag += OnDrag;
        beamParticle = particle.main; // utilizes the main module of the particle system
        change.Add(Color.red);
        change.Add(Color.yellow);
        change.Add(Color.blue);
        beam.gameObject.GetComponent<MeshRenderer>().material = typeColor[2];
        beamParticle.startColor = change[2];
        holder = image.GetComponent<Image>().color;
        particle.Stop();
    }

    private void FixedUpdate()
    {
        float xValue = Input.acceleration.magnitude;
        if (!GestureManager.Instance.isDrag)
        {
            if (particle.isPlaying)
            {
                particle.Stop();
            }                
            onReload();
        }
        if(xValue >= 2.0f)
        {
            isUlt = true;
        }
    }

    public void OnSwipe(object sender, SwipeEventArgs args)
    {
        if (args.SwipeDirection == SwipeDirections.UP)
        {
            Debug.Log("Swiped Up");
            // checks if the current color of the beam matches any of the other colors
            // if it is the same, the color changes to the next color available
            if (holder == change[0])
            {
                image.GetComponent<Image>().color = change[1];
            }
            else if (holder == change[1])
            {
                image.GetComponent<Image>().color = change[2];
            }
            else if (holder == change[2])
            {
                image.GetComponent<Image>().color = change[0];
            }
            holder = image.GetComponent<Image>().color;
        }
        else if (args.SwipeDirection == SwipeDirections.DOWN)
        {
            Debug.Log("Swiped Down");
            if (holder == change[0])
            {
                image.GetComponent<Image>().color = change[2];
            }
            else if (holder == change[1])
            {
                image.GetComponent<Image>().color = change[0];
            }
            else if (holder == change[2])
            {
                image.GetComponent<Image>().color = change[1];
            }
            holder = image.GetComponent<Image>().color;
        }

        for (int i = 0; i < change.Count; i++)
        {
            if (holder == change[i])
            {
                beam.gameObject.GetComponent<MeshRenderer>().material = typeColor[i];
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
        }
        if (particle.isPlaying)
        {
            //checks if player has ult activated
            if (!isUlt)
            {
                if(beamParticle.startSpeedMultiplier > 0)
                {
                    beamParticle.startSpeedMultiplier -= .05f;
                }
                if( beamParticle.startSpeedMultiplier <= 0)
                {
                    beamParticle.startSpeedMultiplier = 0;
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
                }
            }
        }
    }

    public void onReload()
    {
        if (beamParticle.startSpeedMultiplier < 20)
        {
            beamParticle.startSpeedMultiplier += 0.5f;
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
