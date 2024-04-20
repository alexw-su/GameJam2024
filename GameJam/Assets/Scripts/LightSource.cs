using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightSource
{
    Vector3 pos, direction;
    GameObject lightObj;
    LineRenderer light;
    List<Vector3> lightIndices = new List<Vector3>();
    public float startWidth = 0.3f;
    public float endWidth = 0.2f;

    public LightSource(Vector3 pos, Vector3 direction, Material material)
    {
        this.light = new LineRenderer();
        this.lightObj = new GameObject();
        this.lightObj.name = "Light Source";
        this.pos = pos;
        this.direction = direction;

        this.light = this.lightObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.light.startWidth = startWidth;
        this.light.endWidth = endWidth;
        this.light.material = material;
        this.light.startColor = Color.yellow;
        this.light.endColor = Color.yellow;
        CastRay(pos, direction, light);
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer light)
    {
        lightIndices.Add(pos);
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 30, 1))
        {
            //lightIndices.Add(hit.point);
            //UpdateLight();
            CheckHit(hit, dir, light);
        }
        else
        {
            lightIndices.Add(ray.GetPoint(30));
            UpdateLight();
        }

    }
    void UpdateLight()
    {
        int count = 0;
        light.positionCount = lightIndices.Count;
        foreach (Vector3 index in lightIndices)
        {
            light.SetPosition(count, index);
            count++;
        }
    }
    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer light)
    {
        if (hitInfo.collider.gameObject.tag == "Mirror")
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            CastRay(pos, dir, light);
        }
        else
        {
            lightIndices.Add(hitInfo.point);
            UpdateLight();
        }
    }
}
