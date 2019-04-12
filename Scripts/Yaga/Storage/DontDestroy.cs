using UnityEngine;

namespace Yaga
{
    public interface Initable
    {
        void Init();
    }


    public class DontDestroy<T> : MonoBehaviour
        where T : MonoBehaviour, Initable
    {
        static T _instance;

        public static T instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<T>();
                return _instance;
            }
        }

        public static void Create()
        {
            string name = "Class_" + UnityEngine.Random.Range(0.01f, 1000.0f).ToString();
            //string name = "Class_CatsData";
            T data = new GameObject(name).AddComponent<T>();
            data.Init();
            DontDestroyOnLoad(data.gameObject);
        }
    }
}
