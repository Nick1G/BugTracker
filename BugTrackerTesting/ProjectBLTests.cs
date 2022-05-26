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
            mockProject1.Users.Add(newUser);
            mockProject3.Users.Add(newUser);
            newUser.Projects.Add(mockProject1);
            newUser.Projects.Add(mockProject3);

            allProjects = new List<Projects>() { mockProject1, mockProject2, mockProject3, };
            user = newUser;

            Mock<IRepository<Projects>> mockRepo = new Mock<IRepository<Projects>>();

            mockRepo.Setup(repo => repo.Get(It.IsAny<int>())).Returns<int>((num) => allProjects.First(project => project.Id == num));

            mockRepo.Setup(repo => repo.GetAll()).Returns(allProjects);
            mockRepo.Setup(repo => repo.GetList(It.IsAny<Func<Projects, bool>>())).Returns<Func<Projects, bool>>((func) => allProjects.Where(func).ToList());

            mockRepo.Setup(repo => repo.Delete(It.IsAny<Projects>())).Callback<Projects>((proj) => allProjects.Remove(proj));
            mockRepo.Setup(repo => repo.Add(It.IsAny<Projects>())).Callback<Projects>((proj) => allProjects.Add(proj));

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

    [TestClass]
    public class TicketBLTests
    {
        private TicketBusinessLogic ticketBL;
        private List<Tickets> allTickets;
        private ApplicationUser user;

        [TestInitialize]
        public void Initialize()
        {
            Tickets mockTicket1 = new Tickets() { Id = 1, Description = "Bop", Created = DateTime.Today, };
            Tickets mockTicket2 = new Tickets() { Id = 2, Description = "Jenga", Created = DateTime.Now, };
            Tickets mockTicket3 = new Tickets() { Id = 3, Description = "", Created = DateTime.MinValue, };
            ApplicationUser newUser = new ApplicationUser();
            mockTicket1.AssignedToUserId = newUser.Id;
            mockTicket2.AssignedToUserId = newUser.Id;
            mockTicket3.OwnerUserId = newUser.Id;
            newUser.AssignedToTickets.Add(mockTicket1);
            newUser.AssignedToTickets.Add(mockTicket2);
            newUser.OwnedTickets.Add(mockTicket3);

            allTickets = new List<Tickets>() { mockTicket1, mockTicket2, mockTicket3, };
            user = newUser;

            Mock<IRepository<Tickets>> mockRepo = new Mock<IRepository<Tickets>>();

            mockRepo.Setup(repo => repo.Get(It.IsAny<int>())).Returns<int>((num) => allTickets.First(ticket => ticket.Id == num));

            mockRepo.Setup(repo => repo.GetAll()).Returns(allTickets);
            mockRepo.Setup(repo => repo.GetList(It.IsAny<Func<Tickets, bool>>())).Returns<Func<Tickets, bool>>((func) => allTickets.Where(func).ToList());

            mockRepo.Setup(repo => repo.Add(It.IsAny<Tickets>())).Callback<Tickets>((ticket) => allTickets.Add(ticket));
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Tickets>())).Callback<Tickets>((ticket) => allTickets.Remove(ticket));

            ticketBL = new TicketBusinessLogic(mockRepo.Object);
        }

        [TestMethod]
        public void GetAllTicketsTest()
        {
            var tickets = ticketBL.AllTickets();
            CollectionAssert.AreEqual(tickets, allTickets);
        }

        [TestMethod]
        public void GetTicketTest()
        {
            var actualFirstTicket = ticketBL.GetTicket(1);
            var expectedFirstTicket = allTickets.First();

            Assert.AreSame(expectedFirstTicket, actualFirstTicket);

            var actualSecondTicket = ticketBL.GetTicket(2);
            var expectedSecondTicket = allTickets.First(t => t.Id == 2);

            Assert.AreSame(expectedSecondTicket, actualSecondTicket);
        }

        [TestMethod]
        public void GetTicketListTest()
        {
            var whereList = ticketBL.GetTicketsList(ticket => !String.IsNullOrWhiteSpace(ticket.Description));

            CollectionAssert.Contains(whereList, allTickets[0]);
            CollectionAssert.Contains(whereList, allTickets[1]);
            CollectionAssert.DoesNotContain(whereList, allTickets[2]);
        }

        [TestMethod]
        public void CreateTicketTest()
        {
            Tickets newTicket = new Tickets() { Id = 4, Description = "Bwong", Created = DateTime.Now, };
            ticketBL.CreateTicket(newTicket);
            CollectionAssert.Contains(allTickets, newTicket);
        }
    }
}