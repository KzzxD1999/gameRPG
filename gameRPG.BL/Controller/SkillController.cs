using gameRPG.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameRPG.BL.Controller
{
    class SkillController : BaseContoller
    {

        public User CurrentUser { get; set; }
        
        public SkillController(User user)
        {
            CurrentUser = user;
        }


        public List<Skill> AddSkills(int raceId)
        {
            List<Skill> skills = null;
            switch (raceId)
            {
                case 0:
                    skills = new List<Skill>()
                    {
                        //TODO: Зробити АКТИВНЕ ВМІННЯ вибором
                        new Skill("Підняти броню", "Активне вміння",CurrentUser.Name, 0, 0, 4, 9, 0, 0, 15,false),
                        new Skill("Відновити HP", "Активне вміння",CurrentUser.Name, 0,0,0,0,35,0, 25,false )

                    };
                    break;
                case 1:
                    skills = new List<Skill>()
                    {
                        //TODO: Зробити АКТИВНЕ ВМІННЯ вибором
                        new Skill("Вогняний постріл", "Активне вміння",CurrentUser.Name, 12, 0, 0,0, 0, 0, 18,false),
                        new Skill("Підняти магічний захист", "Активне вміння",CurrentUser.Name,0,0,15,0,0,0,22,false )

                    };
                    break;
                case 2:
                     skills = new List<Skill>()
                    {
                        //TODO: Зробити АКТИВНЕ ВМІННЯ вибором
                        new Skill("Підняти атакау", "Активне вміння",CurrentUser.Name, 0, 18, 0,0, 0, 0, 22,false)
                        
                    };
                    break;
            }


             return skills;



        }


    }
}
