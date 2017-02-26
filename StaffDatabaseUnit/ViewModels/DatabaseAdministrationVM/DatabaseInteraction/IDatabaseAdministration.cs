using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDB;
using TeamworkDBEntity;
using System.Collections.ObjectModel;

namespace StaffDatabaseUnit
{
    public interface IDatabaseAdministration
    {
        // Skills tab.
        void AddNewSkill(Skill skill);
        void DeleteSkills(List<Skill> skills);
        void GetPositions(ObservableCollection<Position> positions);
        void GetGroupsOfSpecificPosition(ObservableCollection<SkillsGroup> groups, Position currentPosition);
        void GetSpecificSkills(List<Skill> skills, SkillsGroup currentGroup);
        void EditSkill(string oldSkillName, string editedSkillName);

        // General tab.
        void AddNewElement(IElement categoryElement);
        void DeleteElements(List<IElement> categoryElements, IElement chosenCategory);
        void GetMainElements(List<IElement> categoryElements, IElement chosenCategory);
        void EditElement(IElement oldElement, string editedElementName);

        void GetCountries(ObservableCollection<IElement> supplementaryCategoryElements);
        void AddNewCity(IElement city, IElement country);
        void GetCitiesOfSpecificCountry(List<IElement> categoryElements, IElement country);
        void GetPositions(ObservableCollection<IElement> supplementaryCategoryElements);
        void AddNewGroup(IElement group, IElement position);
        void GetGroupsOfSpecificPosition(List<IElement> categoryElements, IElement position);
    }
}
