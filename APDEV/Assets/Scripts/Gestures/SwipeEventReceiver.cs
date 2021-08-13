using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeEventReceiver : MonoBehaviour
{
    [SerializeField] private Material[] typeColor = new Material[3];
    public GameObject ball;
    private List<Color> change = new List<Color>();
    private Color holder;
    public void Start()
    {
        GestureManager.Instance.OnSwipe += onSwipe;
        change.Add(Color.red);
        change.Add(Color.yellow);
        change.Add(Color.blue);
    }

    public void onSwipe(object sender, SwipeEventArgs args)
    {
        holder = this.GetComponent<Image>().color;
        changeColor(args.SwipeDirection);
    }

    public void changeColor(SwipeDirections dir)
    {
        if(dir == SwipeDirections.UP)
        {
            if(holder == change[0])
            {
                this.GetComponent<Image>().color = change[1];
            }
            else if (holder == change[1])
            {
                this.GetComponent<Image>().color = change[2];
            }
            else if (holder == change[2])
            {
                this.GetComponent<Image>().color = change[0];
            }
            holder = this.GetComponent<Image>().color;
        }
        else if(dir == SwipeDirections.DOWN)
        {
            if (holder == change[0])
            {
                this.GetComponent<Image>().color = change[2];
            }
            else if (holder == change[1])
            {
                this.GetComponent<Image>().color = change[0];
            }
            else if (holder == change[2])
            {
                this.GetComponent<Image>().color = change[1];
            }
            holder = this.GetComponent<Image>().color;
        }

        for (int i = 0; i < change.Count; i++)
        {
            if (holder == change[i])
            {
                ball.gameObject.GetComponent<MeshRenderer>().material = typeColor[i];
            }
        }
    }
}
