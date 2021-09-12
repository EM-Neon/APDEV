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
    /*public float speed = 10;*/
    public float resizeSpeed = 5;
    public float rotateSpeed = 1;
    private Vector3 TargetPos = Vector3.zero;
    ParticleSystem.MainModule beamParticle;
    /*private void OnEnable()
    {
        TargetPos = transform.position;
    }*/

    void Start()
    {
        GestureManager.Instance.OnSwipe += OnSwipe;
        GestureManager.Instance.OnDrag += OnDrag;
        beamParticle = particle.main;
        change.Add(Color.red);
        change.Add(Color.yellow);
        change.Add(Color.blue);
        beam.gameObject.GetComponent<MeshRenderer>().material = typeColor[2];
        beamParticle.startColor = change[2];
        holder = image.GetComponent<Image>().color;
    }

    private void Update()
    {
        
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
        if(args.HitObject == gameObject)
        {
            Ray r = Camera.main.ScreenPointToRay(args.TargetFinger.position);
            Vector3 worldPoint = r.GetPoint(10);

            TargetPos = worldPoint;
            transform.position = worldPoint;
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
