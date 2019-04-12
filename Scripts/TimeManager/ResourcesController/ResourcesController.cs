using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeManager.Product;
using TimeManager.Tutor;

namespace TimeManager
{
    public class ResourcesController : MonoBehaviour
    {
        static ResourcesController instance;

        public ProductResources product_resources;
        public PrefabsResources prefabs_resources;

        public GameObject ProductionFieldUnitsConteiner;
        public GameObject ProvidersContainer;
        public GameObject ProductionUnitsContainer;
        public GameObject CustomersConteiner;

        public GameObject GoalItemViewsConteiner;

        public GameObject RestrictionViewConteiner;

        public List<Transform> product_provider_slots;
        public List<Transform> production_fuild_slots;
        public List<Transform> production_unit_slots;
        public List<Transform> customer_slots;

        int i = 0;
        List<int> indexes = new List<int>(3) { 0, 1, 2};
        public int get_customer_position_index()
        {
            var res = indexes[indexes.Count - 1];
            indexes.RemoveAt(indexes.Count - 1);
            return res;
        }

        public void AddIndex(int index)
        {
            indexes.Add(index);
        }

        public Vector3 GetPositionByIndexAndType(TutorAim aim, int index)
        {
            switch (aim)
            {
                case TutorAim.PRODUCT_PROVIDER:
                    return product_provider_slots[index].position;
                case TutorAim.CUSTOMER:
                    return customer_slots[index].position;
                case TutorAim.PRODUCTION_FIELD:
                    return production_fuild_slots[index].position;
                case TutorAim.PRODUCTION_UNIT:
                    return production_unit_slots[index].position;

            }

            return Vector3.zero;
        }

        public static ResourcesController get_instance()
        {
            if (instance == null)
                instance = GameObject.Find("Controllers").transform.Find("ResourcesController").GetComponent<ResourcesController>();

            return instance;
        }

    }
}