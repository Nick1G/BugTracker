namespace BugTrackerTesting
{
    [TestClass]
    public class ProjectBLTests
    {
        private ProjectBusinessLogic projectBL;
        private List<Projects> allProjects;
        private ApplicationUser user;

        [TestInitialize]
        public void Initialize()
        {
            Projects mockProject1 = new Projects() { Id = 1, Name = "Project", };
            Projects mockProject2 = new Projects() { Id = 2, Name = "Project", };
            Projects mockProject3 = new Projects() { Id = 3, Name = "Project3", };
            ApplicationUser newUser = new ApplicationUser();

            ProjectUsers projUser1 = new ProjectUsers(mockProject1.Id, newUser.Id);
            ProjectUsers projUser2 = new ProjectUsers(mockProject3.Id, newUser.Id);

            allProjects = new List<Projects>() { mockProject1, mockProject2, mockProject3, };
            user = newUser;

            Mock<IRepository<Projects>> mockRepo = new Mock<IRepository<Projects>>();

            mockRepo.Setup(repo => repo.Get(It.Is<int>(id => id == 1))).Returns(mockProject1);
            mockRepo.Setup(repo => repo.Get(It.Is<int>(id => id == 2))).Returns(mockProject2);
            mockRepo.Setup(repo => repo.Get(It.Is<int>(id => id == 3))).Returns(mockProject3);

            mockRepo.Setup(repo => repo.GetAll()).Returns(allProjects);
            mockRepo.Setup(repo => repo.GetList(It.IsAny<Func<Projects, bool>>())).Returns<Func<Projects, bool>>((func) => allProjects.Where(func).ToList());

            mockRepo.Setup(repo => repo.Delete(It.Is<Projects>(project => project == mockProject1))).Callback(() => allProjects.Remove(mockProject1));
            mockRepo.Setup(repo => repo.Add(It.IsAny<Projects>())).Callback((Projects proj) => allProjects.Add(proj));

            projectBL = new ProjectBusinessLogic(mockRepo.Object);
        }

        [TestMethod]
        public void GetAllProjectsTest()
        {
            var expectedList = allProjects;
            var actualList = projectBL.AllProjects();

            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void GetProjectTest()
        {
            Assert.AreSame(allProjects.First(p => p.Id == 1), projectBL.GetProject(1));
            Assert.AreSame(allProjects.First(p => p.Id == 3), projectBL.GetProject(3));
        }

        [TestMethod]
        public void DeleteProjectTest()
        {
            var projectToDelete = projectBL.GetProject(1);
            projectBL.DeleteProject(projectToDelete);

            CollectionAssert.DoesNotContain(allProjects, projectToDelete);
        }

        [TestMethod]
        public void CreateProjectTest()
        {
            var testAdd = new Projects();
            projectBL.CreateProject(testAdd);
            CollectionAssert.Contains(allProjects, testAdd);
        }

        [TestMethod]
        public void GetProjectsListTest()
        {
            var proj1 = allProjects[0];
            var proj2 = allProjects[1];
            var proj3 = allProjects[2];

            var whereList = projectBL.GetProjectsList(project => project.Name == "Project");

            CollectionAssert.Contains(whereList, proj1);
            CollectionAssert.Contains(whereList, proj2);
            CollectionAssert.DoesNotContain(whereList, proj3);
        }

        [TestMethod]
        public void GetAssignedProjectsTest()
        {
            var proj1 = allProjects[0];
            var proj2 = allProjects[1];
            var proj3 = allProjects[2];

            var assignedList = projectBL.GetAssignedProjects(user);

            CollectionAssert.Contains(assignedList, proj1);
            CollectionAssert.Contains(assignedList, proj3);
            CollectionAssert.DoesNotContain(assignedList, proj2);
        }
    }
}