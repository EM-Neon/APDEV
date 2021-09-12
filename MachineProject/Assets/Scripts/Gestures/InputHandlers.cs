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
    
    private List<Color> change = new List<Color>();
    private Color holder;
    public float resizeSpeed = 5;
    public float rotateSpeed = 1;

    public float time = 0.0f;

    private Vector3 TargetPos = Vector3.zero;
    ParticleSystem.MainModule beamParticle;
    ParticleSystem.ShapeModule beamShape;

    /*private void OnEnable()
    {
        TargetPos = transform.position;
    }*/

    void Start()
    {
        GestureManager.Instance.OnSwipe += OnSwipe;
        GestureManager.Instance.OnDrag += OnDrag;
        beamParticle = particle.main; // utilizes the main module of the particle system
        beamShape = particle.shape; // utilizes the shape module and its attributes of the particle system
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
        if (!GestureManager.Instance.isDrag)
        {
            if (particle.isPlaying)
            {
                particle.Stop();
            }                
            onReload();
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

        /*Vector3 worldPoint = r.GetPoint(10);

        TargetPos = worldPoint;
        transform.position = worldPoint;*/

        if (Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            Vector3 theVector = (transform.position - hit.point).normalized;
            particle.transform.LookAt(theVector);
        }


        // should move particle but it no work

        /*particle.transform.position = Camera.main.ScreenToWorldPoint(args.TargetFinger.position) + (Camera.main.transform.forward * 5);*/
        /*beam.transform.position = Camera.main.ScreenToWorldPoint(args.TargetFinger.position) + (Camera.main.transform.forward * 5);*/
        /*Debug.Log($"Position: {args.TargetFinger.position}");*/
        if (particle.isStopped)
        {
            particle.Play();
        }
        if (particle.isPlaying)
        {
            if(beamParticle.startSpeedMultiplier > 0)
            {
                beamParticle.startSpeedMultiplier -= .01f;
            }
            if( beamParticle.startSpeedMultiplier <= 0)
            {
                beamParticle.startSpeedMultiplier = 0;
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
