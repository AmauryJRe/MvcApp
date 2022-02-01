using Microsoft.VisualStudio.TestTools.UnitTesting;
using mvcapp.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace mvcapptests;

[TestClass]
public class MvcAppTests
{
    [TestMethod]
    public void TestMethod1()
    {
        HomeController homeController = new HomeController();
        var result = homeController.Privacy();
        var view = result as ViewResult;
        var name = view.ViewName;
        System.Diagnostics.Debug.WriteLine($"View Name is: {name}");
        System.Type expectedType = typeof(ViewResult);
        Assert.IsInstanceOfType(result, expectedType);
        Assert.AreEqual("Privacy", name);
    }
}