﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Planner.Controllers;
using Planner.DataAccess;
using SmartPlan.ViewModels;
using TechiesWeb.TeamBins.ViewModels;


namespace SmartPlan.Controllers
{
    public class DashboardController : BaseController
    {        
        IRepositary repo;

        public DashboardController()
        {
            repo=new Repositary();
        }

        public ActionResult Index()
        {
            var vm = new DashBoardVM();

            var projectList = repo.GetProjects().Where(s => s.TeamID==TeamID).ToList();
            foreach (var project in projectList)
            {
                var projectVM = new ProjectVM { ID = project.ID, Name = project.Name, Description = project.Description };
                projectVM.IsProjectOwner = (project.CreatedByID == UserID);
                vm.Projects.Add(projectVM);
            }
            vm.RecentIssues = GetRecentIssues();
            vm.IssuesAssignedToMe = GetIssuesAssignedToMe();

            return View(vm);
        }
        private List<IssueVM> GetRecentIssues()
        {
            var listIssues = new List<IssueVM>();

            var issueList = repo.GetIssues().OrderByDescending(s => s.ID).Take(5);
            foreach (var issue in issueList)
            {
                var issueVM = new IssueVM { ID = issue.ID, Title = issue.Title };
                listIssues.Add(issueVM);
            }
            return listIssues;
        }

        private List<IssueVM> GetIssuesAssignedToMe()
        {
            //Gets the issues assigned to the current user
            var listIssues = new List<IssueVM>();

            var issueList = repo.GetIssues().Where(s=>s.IssueMembers.Any(f=>f.MemberID==UserID))
                .OrderByDescending(x=>x.ID).Take(5);
            foreach (var issue in issueList)
            {
                var issueVM = new IssueVM { ID = issue.ID, Title = issue.Title };
                listIssues.Add(issueVM);
            }
            return listIssues;
        }

    }
}
