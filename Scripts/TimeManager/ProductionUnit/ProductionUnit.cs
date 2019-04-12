using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager.ProductionUnit
{
    using TimeManager.Product;

    public class ProductionUnit : MonoBehaviour
    {
        ///
        public GameObject timer;
        public GameObject ready_icon;
        public GameObject product_icon;

        public GameObject[] recipe_slots;
        public GameObject[] recipe_slot_icons;

        public GameObject unit_sprite;

        /// 
        public List<ProductType> def_recipe;
        public List<ProductType> recipe;
        public ProductType result_product;
        public float process_time;

        /// 
        ProductionUnitState cur_state;

        public void Init(List<ProductType> rcp, ProductType res_product, float p_time)
        {
            recipe = rcp;
            def_recipe = new List<ProductType>(rcp);
            result_product = res_product;
            process_time = p_time;
            unit_sprite.GetComponent<SpriteRenderer>().sprite =
                ResourcesController.get_instance().product_resources.get_production_unit_sprite_by_type(res_product);
        }

        public void StartProcess()
        {
            cur_state = new ProcessState(this);
            cur_state.StartState();
        }

        public void BeReady()
        {
            cur_state = new ReadyState(this);
            cur_state.StartState();
        }

        public bool wait_img_anim = false;
        public bool IsReady()
        {
            return cur_state.GetCurStateName() == ProductionUnitStates.READY && !wait_img_anim;
        }

        public void GiveIngredient(ProductType type)
        {
            wait_img_anim = false;
            recipe.Remove(type);
            cur_state.StartState();            

            if (recipe.Count == 0)
            {
                cur_state = new ProcessState(this);
                cur_state.StartState();
            }
        }

        //ToDo: on tap 
        public void OnMouseDown()
        {
            cur_state.OnClick();
        }

        public void Start()
        {
            recipe = new List<ProductType>(def_recipe);
            cur_state = new EmptyState(this);
            cur_state.StartState();
        }

        public void Update()
        {
            cur_state.Update();
        }
    }
}