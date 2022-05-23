namespace BugTrackerTesting
{
    [TestClass]
    public class ProjectBLTests
    {
        private ProjectBusinessLogic projectBL;
        private List<Projects> allProjects;

        [TestInitialize]
        public void Initialize()
        {
            Projects mockProject1 = new Projects() { Id = 1, Name = "Project1", };
            Projects mockProject2 = new Projects() { Id = 2, Name = "Project2", };
            Projects mockProject3 = new Projects() { Id = 3, Name = "Project3", };

            allProjects = new List<Projects>() { mockProject1, mockProject2, mockProject3, };

            Mock<IRepository<Projects>> mockRepo = new Mock<IRepository<Projects>>();

            mockRepo.Setup(repo => repo.Get(It.Is<int>(id => id == 1))).Returns(mockProject1);
            mockRepo.Setup(repo => repo.Get(It.Is<int>(id => id == 2))).Returns(mockProject2);
            mockRepo.Setup(repo => repo.Get(It.Is<int>(id => id == 3))).Returns(mockProject3);
            mockRepo.Setup(repo => repo.GetAll()).Returns(allProjects);

            projectBL = new ProjectBusinessLogic(mockRepo.Object);
        }

        [TestMethod]
        public void GetAllProjectsTest()
        {
            var expectedList = allProjects;
            var actualList = projectBL.AllProjects();

            CollectionAssert.AreEqual(expectedList, actualList);
        }
    }
}