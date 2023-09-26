using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SceneClass
{
    public List<ObjClass> objects;

    public SceneClass()
    {
        objects = new List<ObjClass>();
    }
}
[Serializable]
public class ComponentClass
{

}
[Serializable]
public class ObjClass
{
    public string name;
    public int parentIndex;
    public string tag;
    public int layer;
    public List<int> childindexes;

    public float[] position;
    public float[] rotation;
    public float[] localScale;

    [SerializeField]
    public List<ComponentClass> components;

    public ObjClass(string name, string tag, int layer,float[] position, float[] rotation, float[] localscale, List<ComponentClass> components, int parentindex = -1, List<int> children = null)
    {
        this.name = name;
        this.tag = tag;
        this.layer = layer;
        this.parentIndex = parentindex;
        this.childindexes = children;
        this.position = position;
        this.rotation = rotation;
        this.localScale = localscale;
        this.components = components;
    }
}
[Serializable]
public class BoxColClass : ComponentClass
{
    public bool trigger;
    public int physicsMaterial;
    public float[] center;
    public float[] size;

    public BoxColClass(bool trigger, int material, float[] center, float[] size)
    {
        this.trigger = trigger;
        this.physicsMaterial = material;
        this.center = center;
        this.size = size;
    }
}
[Serializable]
public class RigidbodyClass : ComponentClass
{
    public float mass;
    public float drag;
    public float angulardrag;
    public bool gravity;
    public bool kinematic;

    public CollisionDetectionMode collisiondetection;
    public RigidbodyConstraints constraints;

    public RigidbodyClass(float mass, float drag, float angulardrag, bool gravity, bool kinematic, CollisionDetectionMode detection, RigidbodyConstraints constraints)
    {
        this.mass = mass;
        this.drag = drag;
        this.angulardrag = angulardrag;
        this.gravity = gravity;
        this.kinematic = kinematic;
        collisiondetection = detection;
        this.constraints = constraints;
    }
}
[Serializable]
public class PlayerControllerClass : ComponentClass
{
    public float speed;

    public PlayerControllerClass(float speed)
    {
        this.speed = speed;
    }
}
[Serializable]
public class SpriteRendererClass : ComponentClass
{
    public int spritepath;
    public float[] color;
    public bool flipx, flipy;
    public SpriteDrawMode drawmode;
    public SpriteMaskInteraction maskinteraction;
    public SpriteSortPoint sortpoint;
    public int materialpath;
    public LayerMask sortinglayer;
    public int orderinlayer;

    public SpriteRendererClass(int spritepath, float[] color, bool flipx, bool flipy, SpriteDrawMode drawmode, SpriteMaskInteraction maskinteraction, SpriteSortPoint sortpoint, int materialpath, LayerMask sortinglayer, int orderinlayer)
    {
        this.spritepath = spritepath;
        this.color = color;
        this.flipx = flipx;
        this.flipy = flipy;
        this.drawmode = drawmode;
        this.maskinteraction = maskinteraction;
        this.sortpoint = sortpoint;
        this.materialpath = materialpath;
        this.sortinglayer = sortinglayer;
        this.orderinlayer = orderinlayer;
    }
}
[Serializable]
public class AnimatorClass : ComponentClass
{
    public int controllerpath;

    public AnimatorClass(int controllerpath)
    {
        this.controllerpath = controllerpath;
    }
}
[Serializable]
public class CameraClass : ComponentClass
{
    public CameraClearFlags clearflags;
    public float[] background;
    public LayerMask culling;
    public bool orthographic;
    public float fieldofview;
    public float nearclip;
    public float farclip;

    public CameraClass(CameraClearFlags clearflags, float[] background, LayerMask culling, bool orthographic, float fieldofview, float nearclip, float farclip)
    {
        this.clearflags = clearflags;
        this.background = background;
        this.culling = culling;
        this.orthographic = orthographic;
        this.fieldofview = fieldofview;
        this.nearclip = nearclip;
        this.farclip = farclip;
    }
}
[Serializable]
public class AudioListenerClass : ComponentClass
{

}
[Serializable]
public class CamVisibilityClass : ComponentClass
{

}
[Serializable]
public class LightClass : ComponentClass
{
    public LightType type;
    public float range;
    public float[] color;
    public float spotangle;
    public LightmapBakeType mode;
    public float intensity;
    public float indirectmultiplier;
    public LightShadows shadowtype;
    public float shadowstrength;
    public LayerMask culling;

    public LightClass(LightType type, float range, float[] color, float spotangle, LightmapBakeType mode, float intensity, float indirectmultiplier, LightShadows shadowtype, float shadowstrength, LayerMask culling)
    {
        this.type = type;
        this.range = range;
        this.color = color;
        this.spotangle = spotangle;
        this.mode = mode;
        this.intensity = intensity;
        this.indirectmultiplier = indirectmultiplier;
        this.shadowtype = shadowtype;
        this.shadowstrength = shadowstrength;
        this.culling = culling;
    }
}
[Serializable]
public class MeshRendererClass : ComponentClass
{
    public List<int> materials;
    public MeshRendererClass(List<int> mats)
    {
        materials = mats;
    }
}
[Serializable]
public class MeshFilterClass : ComponentClass
{
    public int meshpath;
    public MeshFilterClass(int meshpath)
    {
        this.meshpath = meshpath;
    }
}
[Serializable]
public class MeshColliderClass : ComponentClass
{
    public bool convex;
    public bool trigger;
    public MeshColliderCookingOptions cookingoptions;
    public int physicsmaterialpath;
    public int meshpath;

    public MeshColliderClass(bool convex, bool trigger, MeshColliderCookingOptions cookingoptions, int physicsmaterialpath, int meshpath)
    {
        this.convex = convex;
        this.trigger = trigger;
        this.cookingoptions = cookingoptions;
        this.physicsmaterialpath = physicsmaterialpath;
        this.meshpath = meshpath;
    }
}