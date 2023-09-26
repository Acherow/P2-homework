using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    public List<Sprite> spriteassets;
    public List<RuntimeAnimatorController> animatorassets;
    public List<PhysicMaterial> physmats;
    public List<Material> mats;
    public List<Mesh> meshes;

    JsonSerializerSettings settings;

    private void Start()
    {
        SaveScene();

        settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            SaveScene();
        if (Input.GetKeyDown(KeyCode.X))
            DeleteScene();
        if (Input.GetKeyDown(KeyCode.C))
            LoadScene();
    }

    void SaveScene()
    {
        SceneClass scene = new SceneClass();

        List<GameObject> objects = FindObjectsOfType<GameObject>().ToList();
        objects.Remove(gameObject);

        foreach (GameObject go in objects)
        {
            ObjClass obj = new ObjClass(go.name, go.tag, go.layer,
                new float[3] { go.transform.localPosition.x, go.transform.localPosition.y, go.transform.localPosition.z},
                new float[3] { go.transform.eulerAngles.x, go.transform.eulerAngles.y, go.transform.eulerAngles.z},
                new float[3] { go.transform.localScale.x, go.transform.localScale.y, go.transform.localScale.z},
                GetComponents(go), 
                go.transform.parent == null? -1 : objects.IndexOf(go.transform.parent.gameObject),
                go.transform.childCount == 0?null:GetChildrenIndexes(objects, go));

            scene.objects.Add(obj);
        }
        File.WriteAllText(Application.persistentDataPath + "/save.json", JsonConvert.SerializeObject(scene, Formatting.Indented, settings));
    }

    void DeleteScene()
    {
        foreach(var go in FindObjectsOfType<GameObject>())
        {
            if (go != gameObject)
                Destroy(go);
        }
    }

    void LoadScene()
    {
        DeleteScene();

        SceneClass scene = JsonConvert.DeserializeObject<SceneClass>(File.ReadAllText(Application.persistentDataPath + "/save.json"), settings);

        List<GameObject> objs = new List<GameObject>();
        for (int i = 0; i < scene.objects.Count; i++)
        {
            GameObject go = new GameObject(scene.objects[i].name);
            objs.Add(go);
        }
        for (int i = 0; i < objs.Count; i++)
        {
            if(scene.objects[i].parentIndex != -1)
                objs[i].transform.parent = objs[scene.objects[i].parentIndex].transform;

            objs[i].tag = scene.objects[i].tag;
            objs[i].layer = scene.objects[i].layer;
            objs[i].transform.localPosition = new Vector3(scene.objects[i].position[0], scene.objects[i].position[1], scene.objects[i].position[2]);
            objs[i].transform.eulerAngles = new Vector3(scene.objects[i].rotation[0], scene.objects[i].rotation[1], scene.objects[i].rotation[2]);
            objs[i].transform.localScale = new Vector3(scene.objects[i].localScale[0], scene.objects[i].localScale[1], scene.objects[i].localScale[2]);

            foreach(var item in scene.objects[i].components)
            {
                if(item is PlayerControllerClass plc)
                {
                    var plcobj = objs[i].AddComponent<PlayerController>();
                    plcobj.speed = plc.speed;
                }
                else if(item is RigidbodyClass rbc)
                {
                    var rb = objs[i].AddComponent<Rigidbody>();
                    rb.mass = rbc.mass;
                    rb.drag = rbc.drag;
                    rb.angularDrag = rbc.angulardrag;
                    rb.collisionDetectionMode = rbc.collisiondetection;
                    rb.constraints = rbc.constraints;
                    rb.useGravity = rbc.gravity;
                    rb.isKinematic = rbc.kinematic;
                }
                else if (item is BoxColClass boxc)
                {
                    var box = objs[i].AddComponent<BoxCollider>();
                    box.size = new Vector3(boxc.size[0], boxc.size[1], boxc.size[2]);
                    box.center = new Vector3(boxc.center[0], boxc.center[1], boxc.center[2]);
                    box.isTrigger = boxc.trigger;
                    if(boxc.physicsMaterial != -1)
                        box.material = physmats[boxc.physicsMaterial];
                }
                else if (item is SpriteRendererClass sprc)
                {
                    var spr = objs[i].AddComponent<SpriteRenderer>();
                    spr.sprite = spriteassets[sprc.spritepath];
                    spr.sortingOrder = sprc.orderinlayer;
                    spr.color = new Color(sprc.color[0], sprc.color[1], sprc.color[2], sprc.color[3]);
                    spr.drawMode = sprc.drawmode;
                    spr.flipX = sprc.flipx;
                    spr.flipY = sprc.flipy;
                    spr.maskInteraction = sprc.maskinteraction;
                    if (sprc.materialpath != -1)
                        spr.material = mats[sprc.materialpath];
                    spr.spriteSortPoint = sprc.sortpoint;
                }
                else if (item is AnimatorClass animc)
                {
                    var anim = objs[i].AddComponent<Animator>();
                    anim.runtimeAnimatorController = animatorassets[animc.controllerpath];
                }
                else if (item is CameraClass camc)
                {
                    var cam = objs[i].AddComponent<Camera>();
                    cam.fieldOfView = camc.fieldofview;
                    cam.backgroundColor = new Color(camc.background[0], camc.background[1], camc.background[2], camc.background[3]);
                    cam.clearFlags = camc.clearflags;
                    cam.cullingMask = camc.culling;
                    cam.farClipPlane = camc.farclip;
                    cam.nearClipPlane = camc.nearclip;
                    cam.orthographic = camc.orthographic;
                }
                else if (item is AudioListenerClass audc)
                {
                    var aud = objs[i].AddComponent<AudioListener>();
                }
                else if (item is CamVisibilityClass camvisc)
                {
                    var camvis = objs[i].AddComponent<CamVisibility>();
                }
                else if (item is LightClass ligc)
                {
                    var lig = objs[i].AddComponent<Light>();
                    lig.type = ligc.type;
                    lig.color = new Color(ligc.color[0], ligc.color[1], ligc.color[2], ligc.color[3]);
                    lig.cullingMask = ligc.culling;
                    lig.bounceIntensity = ligc.indirectmultiplier;
                    lig.intensity = ligc.intensity;
                    lig.range = ligc.range;
                    lig.shadowStrength = ligc.shadowstrength;
                    lig.shadows = ligc.shadowtype;
                    lig.spotAngle = ligc.spotangle;
                    lig.lightmapBakeType = ligc.mode;
                }
                else if (item is MeshFilterClass filtc)
                {
                    var filt = objs[i].AddComponent<MeshFilter>();
                    if(filtc.meshpath != -1)
                        filt.mesh = meshes[filtc.meshpath];
                }
                else if (item is MeshRendererClass mshrndc)
                {
                    var mshrnd = objs[i].AddComponent<MeshRenderer>();
                    mshrnd.materials = GetMaterials(mshrndc.materials.ToArray()).ToArray();
                }
                else if (item is MeshColliderClass mshcolc)
                {
                    var mshcol = objs[i].AddComponent<MeshCollider>();
                    mshcol.convex = mshcolc.convex;
                    mshcol.isTrigger = mshcolc.trigger;
                    if (mshcolc.meshpath != -1)
                        mshcol.sharedMesh = meshes[mshcolc.meshpath];
                    mshcol.cookingOptions = mshcolc.cookingoptions;
                    if(mshcolc.physicsmaterialpath != -1)
                        mshcol.material = physmats[mshcolc.physicsmaterialpath];
                }
            }
        }
    }

    List<ComponentClass> GetComponents(GameObject go)
    {
        List<ComponentClass> comps = new List<ComponentClass>();

        foreach(var item in go.GetComponents<Component>())
        {
            if(item is PlayerController pc)
            {
                comps.Add(new PlayerControllerClass(pc.speed));
            }
            else if(item is Rigidbody rb)
            {
                comps.Add(new RigidbodyClass(rb.mass, rb.drag, rb.angularDrag, rb.useGravity, rb.isKinematic, rb.collisionDetectionMode, rb.constraints));
            }
            else if(item is BoxCollider box)
            {
                comps.Add(new BoxColClass(box.isTrigger, physmats.FindIndex((c) => { return c.name == Trim(box.material.name); }), new float[3] { box.center.x, box.center.y, box.center.z}, new float[3] { box.size.x, box.size.y, box.size.z}));
            }
            else if(item is SpriteRenderer spr)
            {
                comps.Add(new SpriteRendererClass(
                    spriteassets.IndexOf(spr.sprite), new float[4] { spr.color.r, spr.color.g, spr.color.b, spr.color.a}, spr.flipX, spr.flipY, 
                    spr.drawMode, spr.maskInteraction, spr.spriteSortPoint, mats.FindIndex((c) => { return c.name == Trim(spr.material.name); }), spr.sortingLayerID, spr.sortingOrder));
            }
            else if(item is Animator anim)
            {
                comps.Add(new AnimatorClass(animatorassets.IndexOf(anim.runtimeAnimatorController)));
            }
            else if (item is Camera cam)
            {
                comps.Add(new CameraClass(cam.clearFlags, 
                    new float[4] { cam.backgroundColor.r, cam.backgroundColor.g,cam.backgroundColor.b,cam.backgroundColor.a}, 
                    cam.cullingMask, cam.orthographic, cam.fieldOfView, cam.nearClipPlane, cam.farClipPlane));
            }
            else if(item is AudioListener aud)
            {
                comps.Add(new AudioListenerClass());
            }
            else if (item is CamVisibility camvis)
            {
                comps.Add(new CamVisibilityClass());
            }
            else if (item is Light lig)
            {
                comps.Add(new LightClass(lig.type, lig.range, new float[4] { lig.color.r, lig.color.g,lig.color.b,lig.color.a},
                lig.spotAngle,lig.lightmapBakeType,lig.intensity,lig.bounceIntensity,lig.shadows,lig.shadowStrength,lig.cullingMask));
            }
            else if(item is MeshFilter filt)
            {
                comps.Add(new MeshFilterClass(meshes.FindIndex((c) => {return c.name == Trim(filt.mesh.name); })));
            }
            else if(item is MeshRenderer mshrnd)
            {
                comps.Add(new MeshRendererClass(GetMaterials(mshrnd.materials)));
            }
            else if(item is MeshCollider mshcol)
            {
                comps.Add(new MeshColliderClass(mshcol.convex, mshcol.isTrigger, mshcol.cookingOptions, physmats.FindIndex((c) => { return c.name == Trim(mshcol.material.name); }), meshes.FindIndex((c) => { return c.name == Trim(mshcol.sharedMesh.name); })));
            }
        }

        return comps;
    }

    string Trim(string input)
    {
        return input.Replace("(Instance)", "").Replace("Instance", "").TrimEnd(' ');
    }

    List<int> GetMaterials(Material[] inputmats)
    {
        List<int> ret = new List<int>();
        foreach (var item in inputmats)
        {
            ret.Add(mats.FindIndex((c) => {return c.name == Trim(item.name); }));
        }
        return ret;
    }

    List<Material> GetMaterials(int[] inputmats)
    {
        List<Material> ret = new List<Material>();
        foreach (var item in inputmats)
        {
            if(item != -1)
            ret.Add(mats[item]);
        }
        return ret;
    }

    List<int> GetChildrenIndexes(List<GameObject> list, GameObject main)
    {
        List<int> ret = new List<int>();

        foreach (var item in main.transform.GetComponentsInChildren<Transform>())
        {
            ret.Add(list.IndexOf(item.gameObject));
        }

        return ret;
    }
}