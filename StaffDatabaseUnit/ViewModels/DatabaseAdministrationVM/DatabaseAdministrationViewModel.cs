using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamworkDB;
using TeamworkDBEntity;
using System.Windows;
using System.Collections.ObjectModel;
using DelegateCommandNS;

namespace StaffDatabaseUnit
{
    public class DatabaseAdministrationViewModel : DatabaseAdministrationGlueCode
    {
        #region Fields and Constructor
        // Skills tab.
        private DelegateCommand bAddNewSkill;
        private DelegateCommand bDeleteSkill;
        private DelegateCommand cbPositionFilter;
        private DelegateCommand cbGroupFilter;
        private DelegateCommand bEditSkill;
        private DelegateCommand lbSkills; 

        // General tab.
        private DelegateCommand bAddNewElement;
        private DelegateCommand bDeleteElements;
        private DelegateCommand cbMainFilter;
        private DelegateCommand cbResidenceFilter;
        private DelegateCommand bEditElement;
        private DelegateCommand lbElements;

        // Both
        private DelegateCommand bRefreshData;

        public DatabaseAdministrationViewModel() { }
        #endregion


        #region General Tab

        #region Add new element
        public ICommand ButtonAddNewElement
        {
            get
            {
                if (bAddNewElement == null)
                {
                    bAddNewElement = new DelegateCommand(d => AddNewElement(), d => IsAddNewElementAvailable());
                }
                return bAddNewElement;
            }
        }

        private bool IsAddNewElementAvailable()
        {
            if (currentMainFilter == "Skills Groups" || currentMainFilter == "Cities")
            {
                if (isSupplementaryFilter && newElement != "")
                    return true;
            }
            else if (currentMainFilter != "Skills Groups" || currentMainFilter != "Cities")
            {
                if (newElement != "")
                    return true;
            }            
            return false;
        }

        private void AddNewElement()
        {
            try
            {
                if (chosenElement != null)
                {
                    chosenElement.Name = newElement;

                    if (!isSupplementaryFilter)
                    {
                        database.AddNewElement(chosenElement);

                        // Renews skills tab position filter.
                        if (chosenElement is Position)
                            RenewPositionsList();
                    }
                    else
                    {
                        if (isCities)
                        {
                            database.AddNewCity(chosenElement, currentSupplementaryElement);
                        }
                        else if (isGroups)
                        {
                            database.AddNewGroup(chosenElement, currentSupplementaryElement);

                            // Renew skills tab group filter.
                            RenewGroupsList();
                        }
                    }

                    GeneralElement recentElement = new GeneralElement();
                    recentElement.CategoryElement = chosenElement;
                    generalElementsList.Add(recentElement);
                    chosenElement = elementCreator.CreateCategory();
                    NewElement = "";

                    InitiateAdministrationFulfilledEvent();
                }
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }
        #endregion

        #region Delete Element
        public ICommand ButtonDeleteElements
        {
            get
            {
                if (bDeleteElements == null)
                {
                    bDeleteElements = new DelegateCommand(d => DeleteElements(), d => IsDeleteElementsAvailable());
                }
                return bDeleteElements;
            }
        }

        private bool IsDeleteElementsAvailable()
        {
            foreach(GeneralElement element in generalElementsList)
            {
                if (element.IsChecked)
                    return true;
            }
            return false;
        }

        private void DeleteElements()
        {
            try
            {
                List<IElement> tempList = new List<IElement>();
                foreach (GeneralElement element in generalElementsList)
                {
                    if (element.IsChecked)
                        tempList.Add(element.CategoryElement);
                }
                database.DeleteElements(tempList, chosenElement);

                foreach (IElement tempElement in tempList)
                {
                    foreach (GeneralElement generalElement in generalElementsList)
                    {
                        if (generalElement.CategoryElement == tempElement)
                        {
                            generalElementsList.Remove(generalElement);
                            break;
                        }
                    }
                }
                RenewElementEditField();

                // Renew skills tab filters.
                if (chosenElement is Position)
                    RenewPositionsList();
                else if (chosenElement is SkillsGroup)
                    RenewGroupsList();

                InitiateAdministrationFulfilledEvent();
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }
        #endregion

        #region Choose main filter
        public ICommand ComboBoxMainFilter
        {
            get
            {
                if (cbMainFilter == null)
                {
                    cbMainFilter = new DelegateCommand(d => ChooseMainFilter(), d => true);
                }
                return cbMainFilter;
            }
        }

        private void ChooseMainFilter()
        {
            try
            {
                categoryElements.Clear();
                IsSupplementaryFilter = false;
                SupplementaryCategoryElements.Clear();
                RenewElementEditField();

                switch (currentMainFilter)
                {
                    case "Positions":
                        elementCreator = new PositionCreator();
                        break;
                    case "Companies":
                        elementCreator = new CompanyCreator();
                        break;
                    case "Education Facilities":
                        elementCreator = new EducationFacilityCreator();
                        break;
                    case "Education Specialties":
                        elementCreator = new EducationSpecialtyCreator();
                        break;
                    case "Entitling Documents":
                        elementCreator = new EducationEntitlingDocumentCreator();
                        break;
                    case "Languages":
                        elementCreator = new LanguageCreator();
                        break;
                    case "Language Proficiencies":
                        elementCreator = new LanguageProficiencyCreator();
                        break;
                    case "Skills Groups":
                        ChooseSkillGroup();
                        return;
                    case "Skill Proficiencies":
                        elementCreator = new SkillProficiencyCreator();
                        break;
                    case "Citizenships":
                        elementCreator = new CitizenshipCreator();
                        break;
                    case "Countries":
                        elementCreator = new CountryCreator();
                        break;
                    case "Cities":
                        ChooseCity();
                        return;
                    case "Web Services":
                        elementCreator = new WebServiceCreator();
                        break;
                    case "Access Levels":
                        elementCreator = new AccessLevelCreator();
                        break;
                }
                chosenElement = elementCreator.CreateCategory();
                database.GetMainElements(categoryElements, chosenElement);

                FillGeneralElementsList();
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private void ChooseCity()
        {
            isCities = true;
            isGroups = false;
            elementCreator = new CityCreator();
            chosenElement = elementCreator.CreateCategory();

            SupplementaryCategoryElements.Clear();
            database.GetCountries(supplementaryCategoryElements);

            if (supplementaryCategoryElements.Count > 0)
            {
                CurrentSupplementaryElement = supplementaryCategoryElements[0];
                IsSupplementaryFilter = true;
            }
        }

        private void ChooseSkillGroup()
        {
            isGroups = true;
            isCities = false;
            elementCreator = new GroupCreator();
            chosenElement = elementCreator.CreateCategory();

            SupplementaryCategoryElements.Clear();
            database.GetPositions(supplementaryCategoryElements);

            if (supplementaryCategoryElements.Count > 0)
            {
                CurrentSupplementaryElement = supplementaryCategoryElements[0];
                IsSupplementaryFilter = true;
            }
        }

        private void FillGeneralElementsList()
        {
            generalElementsList.Clear();
            for (int i = 0; i < categoryElements.Count; i++)
            {
                GeneralElement generalElement = new GeneralElement();
                generalElement.CategoryElement = categoryElements[i];
                generalElementsList.Add(generalElement);
            }
        }
        #endregion

        #region Choose supplementary filter
        public ICommand ComboBoxResidenceFilter
        {
            get
            {
                if (cbResidenceFilter == null)
                {
                    cbResidenceFilter = new DelegateCommand(d => ChooseSupplementaryFilter(), d => true);
                }
                return cbResidenceFilter;
            }
        }
        private void ChooseSupplementaryFilter()
        {
            try
            {
                RenewElementEditField();
                if (currentSupplementaryElement != null)
                {
                    categoryElements.Clear();
                    generalElementsList.Clear();

                    if (isCities)
                        database.GetCitiesOfSpecificCountry(categoryElements, currentSupplementaryElement);
                    else if (isGroups)
                        database.GetGroupsOfSpecificPosition(categoryElements, currentSupplementaryElement);

                    FillGeneralElementsList();
                }
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }
        #endregion

        #region Edit element
        public ICommand ButtonEditElement
        {
            get
            {
                if (bEditElement == null)
                {
                    bEditElement = new DelegateCommand(d => EditSelectedElement(), d => IsEditElementAvailable());
                }
                return bEditElement;
            }
        }

        private bool IsEditElementAvailable()
        {
            if (editElementName != "" && selectedElement != null)
                return true;
            return false;
        }

        private void EditSelectedElement()
        {
            try
            {
                database.EditElement(selectedElement.CategoryElement, editElementName);

                if (isSupplementaryFilter)
                    ChooseSupplementaryFilter();
                else
                    ChooseMainFilter();

                RenewElementEditField();

                // Renew skills tab.
                if (isSupplementaryFilter)
                    RenewGroupsList();
                else
                    RenewPositionsList();

                InitiateAdministrationFulfilledEvent();
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }           
        }

        private void RenewElementEditField()
        {
            EditElementName = "";
            IsNotEditableElement = true;
        }

        public ICommand ListBoxElements
        {
            get
            {
                if (lbElements == null)
                {
                    lbElements = new DelegateCommand(d => ElementSelectionNotification(), d => true);
                }
                return lbElements;
            }
        }

        private void ElementSelectionNotification()
        {
            try
            {
                if (generalElementsList.Count > 0 && selectedElement != null)
                {
                    EditElementName = SelectedElement.CategoryElement.Name;
                    IsNotEditableElement = false;
                }
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }           
        }
        #endregion


        #endregion


        #region Skills Tab

        #region Add new skill
        public ICommand ButtonAddNewSkill
        {
            get
            {
                if (bAddNewSkill == null)
                {
                    bAddNewSkill = new DelegateCommand(d => AddNewSkill(), d => IsAddNewSkillAvailable());
                }
                return bAddNewSkill;
            }
        }

        private bool IsAddNewSkillAvailable()
        {
            if (positions.Count > 0 && groups.Count > 0
                && newSkill.Name != "" && newSkill.Name != null)
                return true;
            else
                return false;
        }

        private void AddNewSkill()
        {
            try
            {
                currentGroup.Position_Id = currentPosition.Id;
                newSkill.SkillsGroup_Id = currentGroup.Id;
                database.AddNewSkill(newSkill);

                SkillElement element = new SkillElement();
                element.Skill = newSkill;
                chosenSkills.Add(element);
                NewSkill = new Skill();

                InitiateAdministrationFulfilledEvent();
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }
        #endregion

        #region Delete skill
        public ICommand ButtonDeleteSkill
        {
            get
            {
                if (bDeleteSkill == null)
                {
                    bDeleteSkill = new DelegateCommand(d => DeleteSkill(), d => IsDeleteSkillAvailable());
                }
                return bDeleteSkill;
            }
        }

        private bool IsDeleteSkillAvailable()
        {
            foreach (SkillElement element in chosenSkills)
            {
                if (element.IsChecked)
                    return true;
            }
            return false;
        }

        private void DeleteSkill()
        {
            try
            {
                List<Skill> skillsForRemoval = new List<Skill>();
                foreach (SkillElement element in chosenSkills)
                {
                    if (element.IsChecked)
                        skillsForRemoval.Add(element.Skill);
                }

                if (skillsForRemoval.Count > 0)
                {                   
                    database.DeleteSkills(skillsForRemoval);

                    foreach (Skill skill in skillsForRemoval)
                    {
                        foreach (SkillElement element in chosenSkills)
                        {
                            if (element.Skill == skill)
                            {
                                chosenSkills.Remove(element);
                                break;
                            }
                        }
                    }
                    RenewSkillEditField();

                    InitiateAdministrationFulfilledEvent();
                }
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }
        #endregion

        #region Choose position
        public ICommand ComboBoxPositionFilter
        {
            get
            {
                if (cbPositionFilter == null)
                {
                    cbPositionFilter = new DelegateCommand(d => ChoosePosition(), d => true);
                }
                return cbPositionFilter;
            }
        }

        private void ChoosePosition()
        {
            try
            {
                RenewGroupsList();
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private void RenewGroupsList()
        {
            if (currentPosition != null)
            {
                RenewSkillEditField();
                groups.Clear();
                database.GetGroupsOfSpecificPosition(groups, currentPosition);
                if (groups.Count > 0)
                    CurrentGroup = groups[0];
            }
        }

        private void RenewPositionsList()
        {
            RenewSkillEditField();
            positions.Clear();
            database.GetPositions(positions);
            if (positions.Count > 0)
                CurrentPosition = positions[0];
        }
        #endregion

        #region Choose group
        public ICommand ComboBoxGroupFilter
        {
            get
            {
                if (cbGroupFilter == null)
                {
                    cbGroupFilter = new DelegateCommand(d => ChooseGroup(), d => true);
                }
                return cbGroupFilter;
            }
        }

        private void ChooseGroup()
        {
            try
            {
                RenewSkillsList();
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private void RenewSkillsList()
        {
            RenewSkillEditField();
            chosenSkills.Clear();
            skills.Clear();
            if (currentGroup != null && currentPosition != null)
            {
                database.GetSpecificSkills(skills, currentGroup);
                FillSkillsList();
            }
        }

        private void FillSkillsList()
        {
            foreach (Skill skill in skills)
            {
                SkillElement element = new SkillElement();
                element.Skill = skill;
                chosenSkills.Add(element);
            }
        }
        #endregion

        #region Edit skill
        public ICommand ButtonEditSkill
        {
            get
            {
                if (bEditSkill == null)
                {
                    bEditSkill = new DelegateCommand(d => EditSelectedSkill(), d => IsEditSkillAvailable());
                }
                return bEditSkill;
            }
        }

        private bool IsEditSkillAvailable()
        {
            if (editSkill.Name != "" && editSkill.Name != null
                && selectedSkill != null)
                return true;
            return false;
        }

        private void EditSelectedSkill()
        {
            try
            {
                database.EditSkill(selectedSkill.Skill.Name, editSkill.Name);
                RenewSkillsList();
                RenewSkillEditField();

                InitiateAdministrationFulfilledEvent();
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }            
        }

        private void RenewSkillEditField()
        {
            EditSkill = new Skill();
            IsNotEditableSkill = true;
        }

        public ICommand ListBoxSkills
        {
            get
            {
                if (lbSkills == null)
                {
                    lbSkills = new DelegateCommand(d => SkillSelectionNotification(), d => true);
                }
                return lbSkills;
            }
        } 

        private void SkillSelectionNotification()
        {
            try
            {
                if (chosenSkills.Count > 0 && selectedSkill != null)
                {
                    EditSkill = new Skill(selectedSkill.Skill.Name, selectedSkill.Skill.SkillsGroup_Id);
                    IsNotEditableSkill = false;
                }
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }                     
        }
        #endregion
       
        #endregion


        #region Refresh data
        public ICommand ButtonRefreshData
        {
            get
            {
                if (bRefreshData == null)
                {
                    bRefreshData = new DelegateCommand(d => RefreshData(), d => IsRefreshDataAvailable());
                }
                return bRefreshData;
            }
        }

        private bool IsRefreshDataAvailable()
        {            
            return true;
        }

        private void RefreshData()
        {
            try
            {
                RenewPositionsList();
                ChooseMainFilter();

                InitiateAdministrationFulfilledEvent();
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }            
        }
        #endregion


        #region Initiation
        // Initiation.      
        public void GetInitialData()
        {
            try
            {
                // General data.               
                elementCreator = new PositionCreator();
                chosenElement = elementCreator.CreateCategory();
                database.GetMainElements(categoryElements, chosenElement);
                CurrentMainFilter = mainFilterList[0];
                FillGeneralElementsList();

                // Skills data.
                RenewPositionsList();
                RenewGroupsList();
                ChooseGroup();
                //SelectedSkill = skills[0];
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }
        #endregion
    }
}
