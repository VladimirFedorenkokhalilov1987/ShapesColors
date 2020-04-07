using UnityEngine;

[CreateAssetMenu(fileName = "New options", menuName = "options", order = 52)] // 1
public class options : ScriptableObject
{
    public string ObjectType;//тип создаваемого объекта (куб, сфера, капсула)

    //Модель фигуры (GeometryObjectModel):
        public int ClickCount;
        public Color CubeColor;
        public Color SphereColor;
        public Color CapsuleColor;
       // Админка (ГД баланс для объекта) (GeometryObjectData: ScriptableObject):
  //  List<ClickColorData> ClicksData;

// Если текущее количество кликов у обьекта ObjectType находится в диапазоне
  //  MinClicksCount-MaxClicksCount,
// то цвет текущего обьекта меняется на Color
   // ClickColorData:
        public int MinClicksCount;
        public int MaxClicksCount;
        public Color Color;
   // Админка (ГД баланс самой игры) (GameData: ScriptableObject):
        public int ObservableTime;
}
