using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames
{

    public class DresserInitializer : MonoBehaviour
    {
        public Dresser dresser;
        public GameObject garden;

        public void Init(GameObject cat)
        {
            cat = cat.transform.GetChild(0).gameObject;

            dresser.skin = cat.transform.Find("cat_body").gameObject;

            Transform chest_bone = cat.transform.Find("Armature").
                Find("main_Bone").Find("chest_Bone");

            dresser.collar = chest_bone.Find("collar_001").
                gameObject;

            Transform head_Bone = chest_bone.Find("head_bone");

            dresser.head_cat = head_Bone.Find("cap_001").
                gameObject;

            dresser.head_bow = head_Bone.Find("bat").
                gameObject;

            dresser.glasses = head_Bone.Find("glasses_001").
                gameObject;

            dresser.eye_1 = head_Bone.Find("eye_1").
                gameObject;

            dresser.eye_2 = head_Bone.Find("eye_2").
                gameObject;

            garden.SetActive(true);
        }

    }
}