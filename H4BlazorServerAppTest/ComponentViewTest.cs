

namespace H4BlazorServerAppTest
{
    public class ComponentViewTest
    {
        [Fact]
        public void TestAuthorize()
        {
            // Arrange
            using var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("");

            // Act
            var cut = ctx.RenderComponent<Page3>();

            // Assert
            cut.MarkupMatches(@"<h3>Page3</h3><div>Du er <b>logged-in</b>!</div>");
        }

        [Fact]
        public void TestNotAuthorize()
        {
            // Arrange
            using var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetNotAuthorized();

            // Act
            var cut = ctx.RenderComponent<Page3>();

            // Assert
            cut.MarkupMatches(@"<h3>Page3</h3><div>Du er <b>ikke</b> Logged-in</div>");
        }

        [Fact]
        public void TestAdminAuthorize()
        {
            // Arrange
            using var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetRoles("Admin");

            // Act
            var cut = ctx.RenderComponent<Page4>();

            // Assert
            cut.MarkupMatches(@"<div><h3>Adgang tilladt fordi du er admin.</h3></div><h3>Page4</h3>");
        }

        [Fact]
        public void TestAuthorizeInCode()
        {
            // Arrange
            using var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("");

            // Act
            var cut = ctx.RenderComponent<Page2>();
            var instance = cut.Instance;
            bool isAuthorized = instance.IsAuthenticated;

            // Assert
            Assert.True(isAuthorized);
        }

        [Fact]
        public void TestNotAuthorizeInCode()
        {
            // Arrange
            using var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("");

            // Act
            var cut = ctx.RenderComponent<Page2>();
            var instance = cut.Instance;
            bool isAuthorized = instance.IsAuthorized;

            // Assert
            Assert.False(isAuthorized);
        }
    }
}
