using System;
using System.Collections;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class AddShape : MonoBehaviour
{
    ObjectName data;
    public String ObjName;
    private string json;
    private GameObject _myPrefab;
    IEnumerator LoadAssetBundle(string url)
     {
         WWW www = WWW.LoadFromCacheOrDownload(url, 2);
         print("Loading www");
     
         // Wait for download to complete
         yield return www;
         if (!string.IsNullOrEmpty(www.error))
         {
             Debug.LogError(www.error);
             yield return null;
         }
         data = JsonUtility.FromJson<ObjectName>(json);

         ObjName = data.Names[Random.Range(0, data.Names.Length)];
         AssetBundle bundle = www.assetBundle;
         AssetBundleRequest request = bundle.LoadAssetAsync(ObjName, typeof(GameObject));
     
         // Wait for completion
         yield return request;
         
         _myPrefab =  request.asset as GameObject;
        
         var mousePos = Input.mousePosition;
         mousePos.z = 10;
         var objectPos = Camera.main.ScreenToWorldPoint(mousePos);
         var go = _myPrefab.AddComponent<ChangeColor>();

         Instantiate(go, objectPos, Quaternion.identity);

         bundle.Unload(false);
         www.Dispose();
     }

  private void Update()
  {
      if (Input.GetMouseButtonDown(0))
      {
          RaycastHit hit;
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          if (Physics.Raycast(ray, out hit,
              Mathf.Infinity))
          {
              hit.collider.gameObject.GetComponent<ChangeColor>().AddClick();
              if (hit.collider.GetComponent<ChangeColor>().ClickCount <
                  FindObjectOfType<Admin>()._optionsForCube.MinClicksCount && hit.collider.gameObject.name == "Cube")
              {
                      hit.collider.gameObject.GetComponent<Renderer>().material.color = FindObjectOfType<Admin>()._optionsForCube.CubeColor;
              }
              if (hit.collider.GetComponent<ChangeColor>().ClickCount <
                  FindObjectOfType<Admin>()._optionsForCapsule.MinClicksCount && hit.collider.gameObject.name == "Capsule")
              {
                  hit.collider.gameObject.GetComponent<Renderer>().material.color = FindObjectOfType<Admin>()._optionsForCapsule.CapsuleColor;
              } 
              if (hit.collider.GetComponent<ChangeColor>().ClickCount <
                    FindObjectOfType<Admin>()._optionsForSphere.MinClicksCount && hit.collider.gameObject.name == "Sphere")
              {
                  hit.collider.gameObject.GetComponent<Renderer>().material.color = FindObjectOfType<Admin>()._optionsForSphere.SphereColor;
              }
              
              
              if (hit.collider.GetComponent<ChangeColor>().ClickCount >
                  FindObjectOfType<Admin>()._optionsForCube.MinClicksCount && hit.collider.GetComponent<ChangeColor>().ClickCount <
                  FindObjectOfType<Admin>()._optionsForCube.MaxClicksCount && hit.collider.gameObject.name == "Cube")
              {
                  hit.collider.gameObject.GetComponent<Renderer>().material.color = FindObjectOfType<Admin>()._optionsForCube.Color;
              }
              if (hit.collider.GetComponent<ChangeColor>().ClickCount >
                  FindObjectOfType<Admin>()._optionsForCapsule.MinClicksCount && hit.collider.GetComponent<ChangeColor>().ClickCount <
                  FindObjectOfType<Admin>()._optionsForCapsule.MaxClicksCount && hit.collider.gameObject.name == "Capsule")
              {
                  hit.collider.gameObject.GetComponent<Renderer>().material.color = FindObjectOfType<Admin>()._optionsForCapsule.Color;
              } 
              if (hit.collider.GetComponent<ChangeColor>().ClickCount >
                  FindObjectOfType<Admin>()._optionsForSphere.MinClicksCount && hit.collider.GetComponent<ChangeColor>().ClickCount <
                  FindObjectOfType<Admin>()._optionsForSphere.MaxClicksCount && hit.collider.gameObject.name == "Sphere")
              {
                  hit.collider.gameObject.GetComponent<Renderer>().material.color = FindObjectOfType<Admin>()._optionsForSphere.Color;
              }
          }
          else if(hit.collider==null)
          {
              json = File.ReadAllText(Application.streamingAssetsPath + "/ObjectsNames.json");
              StartCoroutine(LoadAssetBundle("https://www.dropbox.com/s/18qwrk11d7x61ct/myab?dl=1"));
          }
      }

      if (Input.GetKeyDown(KeyCode.Escape))
      {
          Application.Quit();
      }
  }
  
  [System.Serializable]
  public class ObjectName
  {
      public string [] Names;
  }
}
