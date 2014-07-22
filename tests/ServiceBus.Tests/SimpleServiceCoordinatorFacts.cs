using System;
using System.Text;
using Xunit;

namespace ServiceBus.Tests
{
    public class SimpleServiceCoordinatorFacts
    {

        protected SimpleServiceCoordinator Coordinator = new SimpleServiceCoordinator();

        public class NoExecuteTestComponent : IComponent
        {
            public bool Parallel
            {
                get { return false; }
            }
        }

        public class TestComponent : NoExecuteTestComponent
        {
            [ComponentAction]
            public string Execute(string p1, int p2)
            {
                return null;
            }
        }

        public class InvalidTestState
        {
            public string P1 { get; set; }
        }

        public class TestState : InvalidTestState 
        {

            public int P2 { get; set; }
        }

        public class DetermineMethodFacts : SimpleServiceCoordinatorFacts
        {

            [Fact]
            public void NullComponentThrowsArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => Coordinator.DetermineMethod(null));
            }

            [Fact]
            public void NoExecuteMethodThrowsMissingMethodException()
            {
                Assert.Throws<MissingMethodException>(() => Coordinator.DetermineMethod(new NoExecuteTestComponent()));
            }

            [Fact]
            public void ReturnsExecuteMethodFromTestComponent()
            {
                TestComponent component = new TestComponent();
                var methodInfo = Coordinator.DetermineMethod(component);
                Assert.Equal("Execute", methodInfo.Name);
                Assert.Equal("TestComponent", methodInfo.DeclaringType.Name);
            }

        }

        public class DetermineArgumentsFacts : SimpleServiceCoordinatorFacts
        {

            [Fact]
            public void ReturnsExecuteMethodArgumentsFromTestComponent()
            {
                TestComponent component = new TestComponent();
                TestState state = new TestState();
                var methodInfo = Coordinator.DetermineMethod(component);
                Assert.Equal("Execute", methodInfo.Name);
                Assert.Equal("TestComponent", methodInfo.DeclaringType.Name);
                var arguments = Coordinator.DetermineArguments(methodInfo, state);
                Assert.Equal(2, arguments.Count);
                Assert.Equal(true, arguments.Keys.Contains("p1"));
                Assert.Equal(true, arguments.Keys.Contains("p2"));
            }
        }

    }
}
