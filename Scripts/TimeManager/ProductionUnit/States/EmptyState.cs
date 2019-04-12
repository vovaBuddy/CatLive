using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;

namespace TimeManager.ProductionUnit
{
    using TimeManager.ProductionUnitAPI;
    public class EmptyState : ProductionUnitState
    {
        ProductionUnit unit;


        public EmptyState(ProductionUnit u)
        {
            unit = u;
        }

        public void EndState()
        {
        }

        public ProductionUnitStates GetCurStateName()
        {
            return ProductionUnitStates.EMPTY;
        }

        public void OnClick()
        {
            //copy - not good
            var tmp_recipe = new List<Product.ProductType>(unit.recipe);
            for (int i = 0; i < tmp_recipe.Count; ++i)
            {
                MessageBus.Instance.SendMessage(new Message(Messages.NEED_INGREDIENT,
                    new NeedIngredientParams(tmp_recipe[i], unit.gameObject)));
            }
        }

        public void StartState()
        {
            //unit.ready_icon.SetActive(false);

            unit.product_icon.SetActive(true);
            unit.product_icon.GetComponent<SpriteRenderer>().sprite =
                ResourcesController.get_instance().product_resources.get_lock_by_type(unit.result_product);

            foreach (var slot in unit.recipe_slots)
            {
                slot.SetActive(false);
            }

            unit.timer.SetActive(false);

            for(int i = 0; i < unit.recipe.Count; i++)
            {
                unit.recipe_slots[i].SetActive(true);
                unit.recipe_slot_icons[i].GetComponent<SpriteRenderer>().sprite =
                    ResourcesController.get_instance().product_resources.get_small_by_type(unit.recipe[i]);
            }
        }

        public void Update()
        {
        }
    }
}