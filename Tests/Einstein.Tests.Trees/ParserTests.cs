using System.Linq;
using Einstein.Trees;
using Einstein.Trees.Trees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Einstein.Tests.Trees
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void EmptyString_Test()
        {
            var tree = Parse("  \t ");
            Assert.IsFalse(tree.Classes.Any());
        }

        [TestMethod]
        public void Class_NoParameters_Test()
        {
            var tree = Parse("class Fish end ");
            var @class = tree.Classes.First();
            Assert.AreEqual("Fish", @class.Name);
            Assert.AreEqual(null, @class.SuperName);
            Assert.IsFalse(@class.Functions.Any());
            Assert.IsFalse(@class.Parameters.Any());
        }

        [TestMethod]
        public void Class_EmptyParameterList_Test()
        {
            var tree = Parse("class Bird (\r) end ");
            var @class = tree.Classes.First();
            Assert.AreEqual("Bird", @class.Name);
            Assert.IsFalse(@class.Functions.Any());
            Assert.IsFalse(@class.Parameters.Any());
        }

        [TestMethod]
        public void Class_OneParameter_Test()
        {
            var tree = Parse(" class Car(age: Integer) end ");
            var @class = tree.Classes.First();
            Assert.AreEqual("Car", @class.Name);
            Assert.IsFalse(@class.Functions.Any());
            Assert.IsTrue(@class.Parameters.Any());
            Assert.AreEqual("age", @class.Parameters.First().Name);
            Assert.AreEqual("Integer", @class.Parameters.First().TypeName);
        }

        [TestMethod]
        public void Class_TwoParameters_Test()
        {
            var tree = Parse(" class Car(age:Integer,  name: String)  end ");
            var parameters = tree.Classes.First().Parameters;
            Assert.AreEqual(2, parameters.Count());
            Assert.AreEqual("String", parameters.Last().TypeName);
        }

        [TestMethod]
        public void Class_SuperClass_Test()
        {
            var tree = Parse(" class Fish < Animal  end  class Animal end ");
            var firstClass = tree.Classes.First();
            var secondClass = tree.Classes.Last();
            Assert.AreEqual("Fish", firstClass.Name);
            Assert.AreEqual("Animal", firstClass.SuperName);
            Assert.AreEqual("Animal", secondClass.Name);
            Assert.IsNull(secondClass.SuperName);
        }

        [TestMethod]
        public void Method_NoParameterList_Test()
        {
            var tree = Parse("class Bear function eat end end ");
            var @class = tree.Classes.First();
            Assert.AreEqual("Bear", @class.Name);
            var function = @class.Functions.First();
            Assert.AreEqual("eat", function.Name);
            Assert.IsFalse(function.Parameters.Any());
        }

        [TestMethod]
        public void Method_NoParameters_Test()
        {
            var tree = Parse("class Bear function eat() end end ");
            var @class = tree.Classes.First();
            Assert.AreEqual("Bear", @class.Name);
            var function = @class.Functions.First();
            Assert.AreEqual("eat", function.Name);
            Assert.IsFalse(function.Parameters.Any());
        }

        [TestMethod]
        public void Method_OneParameters_Test()
        {
            var tree = Parse(" class Fish function swim(meters: Integer) end end ");
            var function = tree.Classes.First().Functions.First();
            Assert.AreEqual("swim", function.Name);
            var parameter = function.Parameters.First();
            Assert.AreEqual("meters", parameter.Name);
            Assert.AreEqual("Integer", parameter.TypeName);
        }

        [TestMethod]
        public void Method_MultipleParameters_Test()
        {
            var tree = Parse(" class Fish function swim(meters: Integer, x: Float, y: Double) end end ");
            var function = tree.Classes.First().Functions.First();
            Assert.AreEqual("swim", function.Name);
            var parameter = function.Parameters.First();
            Assert.AreEqual("meters", parameter.Name);
            Assert.AreEqual("Integer", parameter.TypeName);
            Assert.AreEqual("y", function.Parameters.Last().Name);
            Assert.AreEqual("Float", function.Parameters.Skip(1).First().TypeName);
        }

        private static CompilationUnitTree Parse(string source)
        {
            var parser = new Parser(source);
            return parser.ParseCompilationUnit();
        }
    }
}
