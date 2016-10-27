app.service("UsersSkillService", function ($http) {

    this.GetAllUserSkill = function (userId) {
        return $http.get('api/UserSkills/GetUserSkills');
    }

    this.getSkillByTechnology = function (TechIds) {
        debugger;
        return $http.get('api/UserSkills/GetSkillByTechnology?TechIds=' + TechIds);
    }

    
});