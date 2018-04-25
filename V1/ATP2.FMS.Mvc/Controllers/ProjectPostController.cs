using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FMS_Entities;
using FMS_Framework;
using FMS_Model;
using FMS_Repository;
using FMS_Web_Framework.Base;

namespace ATP2.FMS.Mvc.Controllers
{
    public class ProjectPostController : BaseController
    {
        // GET: ProjectPost
        public ActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProject(PostProjectModel PostProjectModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateProject", PostProjectModel);
            }

            try
            {
                var postAProject=new PostAProject();
                postAProject.WUserId = CurrentUser.User.UserId;
                postAProject.ProjectName = PostProjectModel.ProjectName;
                postAProject.Description = PostProjectModel.Description;
                postAProject.Price = PostProjectModel.Price;
                postAProject.StartTime = PostProjectModel.StartTime;
                postAProject.EndTime = PostProjectModel.EndTime;
                postAProject.Members = PostProjectModel.Members;
                var result = postProjectDao.Save(postAProject);

                foreach (var x in PostProjectModel.SectionName)
                {
                    var projectsection = new ProjectSection();
                    projectsection.SectionName = x;
                    var result1 = projectSectionDao.Save(projectsection);
                }

                foreach (var skillid in PostProjectModel.SkillId)
                {
                    var projectskill = new ProjectSkills();
                    projectskill.SkillID = skillid;
                    var result2 = projectSkillDao.Save(projectskill);
                }
              

              
                
                
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("CreateProject");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("ProjectList","Owner");
        }

        public ActionResult ProjectDetails(int? id)
        {
            //postid
            var result = postProjectDao.GetByID(1);
            PostProjectModel postProjectModel= new PostProjectModel();

            postProjectModel.ProjectName = result.Data.ProjectName;
            postProjectModel.Description = result.Data.Description;
            postProjectModel.Price = result.Data.Price;
            postProjectModel.StartTime = result.Data.StartTime;
            postProjectModel.EndTime = result.Data.EndTime;
            postProjectModel.WUserId = result.Data.WUserId;
            postProjectModel.PostId = result.Data.PostId;

            var result2 = projectSkillDao.GetAll(result.Data.PostId);
            foreach (var skillid in result2)
            {
                var result3 = skillsDao.GetByID(skillid.SkillID);

                postProjectModel.SkillName.Add(result3.Data.SkillName);
                  
            }

            var result4 = userDao.GetById(result.Data.WUserId);
            postProjectModel.UFirstName = result4.Data.FristName;
            postProjectModel.ULastName = result4.Data.LastName;


            return View(postProjectModel);
        }

        [HttpPost]
        public ActionResult ProjectDetails(PostProjectModel PostProjectModel)
        {
          

           

            try
            {

                ResponseToaJob responseto = new ResponseToaJob();
                responseto.PostId = PostProjectModel.PostId;
                responseto.WUserId = CurrentUser.User.UserId;
                var result = response.Save(responseto);

                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("ProjectDetails", PostProjectModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("ProjectList","Owner");
        }

        public ActionResult RequestedMember(int id)
        {
            RequestedMemberModel requested = getall(id);
            return View(requested);
        }

        [HttpPost]
        public ActionResult RequestedMember(SelectedWorker selected)
        {

           
            try
            {

                var result = selectedWorkerDao.Save(selected);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("RequestedMember");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("OwnerProfile","Owner");
        }

        public ActionResult WorkProgress(int id)
        {
            WorkProgressModel work = new WorkProgressModel();

          //  work.RequestedMemberModel= getall();
            var result = projectSectionDao.GetAll(id);
            foreach (var section in result)
            {
                work.USectionName.Add(section.SectionName+"1");
            }
            work.ProjectSection = result;
            var result2 = selectedWorkerDao.GetAll(id);
            work.SelectedWorkers = result2;
            foreach (var p in result2)
            {
                var result3 = userDao.GetById(p.UserId);
                work.Name.Add(result3.Data.FristName + " " + result3.Data.LastName);
              
            }
           

            return View(work);
        }

        [HttpPost]
        public ActionResult WorkProgress(ProjectSection projectSection)
        {

            try
            {

                var result = projectSectionDao.Save(projectSection);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("WorkProgress");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("WorkProgress");
        }

        [HttpPost]
        public ActionResult CommentInsert(COMMENTSEC commentsec)
        {

            try
            {

                var result = CommunicationDao.Save(commentsec);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("WorkProgress");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("WorkProgress");
        }

        private RequestedMemberModel getall(int id)
        {
            RequestedMemberModel requested = new RequestedMemberModel();
            var result = response.GetAll(id);

            var result2 = postProjectDao.GetByID(id);
            requested.ProjectName = result2.Data.ProjectName;
            requested.Description = result2.Data.Description;
            requested.PostId = result2.Data.PostId;
            foreach (var user in result)
            {
                var result3 = userDao.GetById(user.WUserId);
                requested.UserInfo.Add(result3.Data);

            }
            return requested;
        }
    }
}