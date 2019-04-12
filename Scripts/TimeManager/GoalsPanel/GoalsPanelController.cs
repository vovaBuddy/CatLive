using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeManager.Level;
using Yaga.MessageBus;
using UnityEngine.UI;

namespace TimeManager.GoalsPanel
{
    public class GoalsPanelController : MonoBehaviour
    {
        public LevelsController levelsController;
        public TimeManager.Tutor.TutorController tutorController;
        public Transform goals_conteiner;
        public GameObject goals_panel;
        public Text header;

        // Use this for initialization
        void Start()
        {
            StartCoroutine(ShowPanel());
        }

        public IEnumerator ShowPanel()
        {
            yield return new WaitForEndOfFrame();

            if(levelsController.cur_level_conf.tutors.Count > 0)
            {
                if(levelsController.cur_level_conf.tutors[0].type == Tutor.TutorType.INFO)
                {
                    tutorController.ShowTutor2(levelsController.cur_level_conf.tutors[0]);
                }
            }            

            foreach (var goal in levelsController.cur_level_conf.non_product_goals)
            {
                var go = Instantiate(ResourcesController.get_instance().prefabs_resources.GoalItemView, goals_conteiner);
                var view = go.GetComponent<GoalItemView>();
                view.Init(GoalType.NON_PRODUCTS, goal.value, Product.ProductType.NONE, goal.goal);
            }

            foreach (var goal in levelsController.cur_level_conf.quantitative_goals)
            {
                var go = Instantiate(ResourcesController.get_instance().prefabs_resources.GoalItemView, goals_conteiner);
                GoalItemView view = go.GetComponent<GoalItemView>();
                view.Init(GoalType.PRODUCTS, goal.amount, goal.product, NonProductsGoal.NON);
            }

            header.text = "Level " + (levelsController.cur_level_conf.id + 1);
            goals_panel.SetActive(true);
        }

        public void StartGame()
        {
            goals_panel.SetActive(false);

            MessageBus.Instance.SendMessage(GameController.Messages.START_GAME);
        }
    }
}