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
        public List<Skill> Skills { get; set; }
        
        public SkillController(User user)
        {
            CurrentUser = user;
        }


        public List<Skill> AddSkills(int raceId)
        {
            List<Skill> skills = null;
            switch (raceId)
            {
                case 1:
                    skills = new List<Skill>()
                    {
          
                        new Skill(1,"Підняти броню", "Активне вміння",CurrentUser.Name, 0, 0, 4, 9, 0, 0, 15,false,1, 0, 3),
                        new Skill(2,"Відновити HP", "Активне вміння",CurrentUser.Name, 0,0,0,0,35,0, 25,false,1,0, 5)

                    };
                    break;
                case 2:
                    skills = new List<Skill>()
                    {
                        
                        new Skill(1,"Вогняний постріл", "Активне вміння",CurrentUser.Name, 12, 0, 0,0, 0, 25, 18,false,1, 0, 2),
                        new Skill(2,"Підняти магічний захист", "Активне вміння", CurrentUser.Name,0,0,15,0,0,36,22,false,1,0,6 )

                    };
                    break;
                case 3:
                     skills = new List<Skill>()
                    {
       
                        new Skill(1,"Підняти атакау", "Активне вміння",CurrentUser.Name, 0, 18, 0,0, 0, 0, 22,false,1, 0, 7)
                        
                    };
                    break;
            }

            
             return skills;



        }


    }
}
