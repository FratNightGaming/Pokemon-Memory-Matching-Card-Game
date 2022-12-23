using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer mr;

    public Vector2[] offsetPoints = new Vector2[5]
    {
      new Vector2(-1, -1), new Vector2(1, -1), new Vector2(-1, 1),
      new Vector2(1, 1), new Vector2(-1, -1),
    };

    public Vector2 mainTexOffset;

    public float moveSpeedX;
    public float moveSpeedY;

    //offsetPoints[0] = new Vector2(-1, -1);
    //offsetPoints[1] = new Vector2(1,0);
    //offsetPoints[2] = new Vector2(-1,1);
    //offsetPoints[3] = new Vector2(1,0);
    //offsetPoints[4] = new Vector2(-1,-1);




    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        Debug.Log(mr.material.mainTextureOffset);
        moveSpeedX = -.01f;
        moveSpeedY = Random.Range(-.01f, -.05f);

        int index = 0;
        foreach (var point in offsetPoints)
        {
            Debug.Log("Position" + index + ": " + offsetPoints[index]);
            index++;
        }
    }

    void Update()
    {
        //ParallaxFigure8();
        ParallaxScreenSaver();

    }

    private void ParallaxScreenSaver()
    {
        mainTexOffset = mr.material.mainTextureOffset;
        float randY = Random.Range(.01f, .05f);

        if (mainTexOffset.x <= -1)
        {
            moveSpeedX *= -1;
        }

        if (mainTexOffset.x >= 1)
        {
            moveSpeedX *= -1;
        }

        if (mainTexOffset.y >= 1)
        {
            moveSpeedY = randY * -1;
        }

        if (mainTexOffset.y <= -1)
        {
            moveSpeedY = randY;
        }

        mr.material.mainTextureOffset += new Vector2(moveSpeedX * Time.deltaTime, moveSpeedY * Time.deltaTime);
    }

    private void SetSpeedZero()
    {
        moveSpeedX = 0f;
        moveSpeedY = 0f;
    }

    private void ParallaxFigure8()
    {
        mainTexOffset = mr.material.mainTextureOffset;

        //Debug.Log(mainTexOffset);

        if (Vector2.Distance(mainTexOffset, offsetPoints[0]) < .01f)
        {
            moveSpeedX = .5f;
            moveSpeedY = 0f;
        }

        if (Vector2.Distance(mainTexOffset, offsetPoints[1]) < .01f)
        {
            moveSpeedX = -.5f;
            moveSpeedY = .5f;
        }

        if (Vector2.Distance(mainTexOffset, offsetPoints[2]) < .01f)
        {
            moveSpeedX = .5f;
            moveSpeedY = 0f;
        }

        if (Vector2.Distance(mainTexOffset, offsetPoints[3]) < .01f)
        {
            moveSpeedX = -.5f;
            moveSpeedY = -.5f;
        }

        mr.material.mainTextureOffset += new Vector2(moveSpeedX * Time.deltaTime, moveSpeedY * Time.deltaTime);
    }
}
