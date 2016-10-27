app.service("SkillService", function ($http) {

    this.getAllSkills = function () {
        return $http.get("api/SkillDetail/GetAllSkills");
    }


    this.DeleteSkill = function (skillId) {
        
        return $http.get("api/SkillDetail/DeleteSkill?skillId=" + skillId);
    }

    this.SaveSkills = function (technologyId, skillNames) {
        return $http.get("api/SkillDetail/SaveSkills?technologyId=" + technologyId + "&skillNames=" + skillNames);
    }

});