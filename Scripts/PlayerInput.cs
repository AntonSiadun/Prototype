using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NewScripts
{
    public class PlayerInput: MonoBehaviour
    {
        [SerializeField] public Button defendSkillButton;
        [SerializeField] public Button firstAttackingSkillButton;
        [SerializeField] public Button secondAttackingSkillButton;
        [SerializeField] public Joystick joystick;

        public void AddDefendingSkillListener(UnityAction skill)
        {
            defendSkillButton.onClick.AddListener(skill);
        }
    
        public void AddAttackingSkill_1_Listener(UnityAction skill)
        {
            firstAttackingSkillButton.onClick.AddListener(skill);
        }
    
        public void AddAttackingSkill_2_Listener(UnityAction skill)
        {
            secondAttackingSkillButton.onClick.AddListener(skill);
        }
    }
}
