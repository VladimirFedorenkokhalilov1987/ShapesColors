using System;
using UniRx;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
        private int secondsToChangeColor;
        public int ClickCount;
        private int MinClicksCount;
        private int MaxClicksCount;
        
        private GameObject _gameObject;
        private IDisposable _update;
    
        void Start()
        {
            transform.SetParent(FindObjectOfType<Admin>().transform);
            _gameObject = gameObject;
            ChangeNameTo(FindObjectOfType<AddShape>().ObjName);
            if (gameObject.name == "Cube")
            {
                secondsToChangeColor = FindObjectOfType<Admin>()._optionsForCube.ObservableTime;
                MinClicksCount = FindObjectOfType<Admin>()._optionsForCube.MinClicksCount;
                MaxClicksCount = FindObjectOfType<Admin>()._optionsForCube.MaxClicksCount;
            }
            if (gameObject.name == "Capsule")
            {
                secondsToChangeColor = FindObjectOfType<Admin>()._optionsForCapsule.ObservableTime;
                MinClicksCount = FindObjectOfType<Admin>()._optionsForCapsule.MinClicksCount;
                MaxClicksCount = FindObjectOfType<Admin>()._optionsForCapsule.MaxClicksCount;
            }
            if (gameObject.name == "Sphere")
            {
                secondsToChangeColor = FindObjectOfType<Admin>()._optionsForSphere.ObservableTime;
                MinClicksCount = FindObjectOfType<Admin>()._optionsForSphere.MinClicksCount;
                MaxClicksCount = FindObjectOfType<Admin>()._optionsForSphere.MaxClicksCount;
            }
            
            _update = Observable.Interval(TimeSpan.FromSeconds(secondsToChangeColor)).Subscribe(x => // x starts from 0 and is incremented everytime the stream pushes another item.
            {
                if(_gameObject.GetComponent<Renderer>())
                _gameObject.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
            });
        }

        void ChangeNameTo( String _newName)
        {
            gameObject.name = _newName;
        }

        public void AddClick()
        {
            ClickCount++;
        }

        private void OnDestroy()
        {
            _update.Dispose();
        }
}
