using UnityEditor;

public class BundleCreate 
{
   [MenuItem("Simple Bundles/Build")]
   static void BuildBundles()
   {
      string path = EditorUtility.SaveFolderPanel("Save Bundle", "", "");
      if (path.Length != 0)
      {
         BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.StandaloneWindows);
      }
   }
}
