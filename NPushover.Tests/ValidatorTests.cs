using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPushover.Validators;

namespace NPushover.Tests
{
    [TestClass]
    public class ValidatorTests
    {
        [TestMethod]
        public void AppKeyValidator_WorksCorrectly()
        {
            var target = new ApplicationKeyValidator();

            target.Validate("test", "azGDORePK8gMaC0QOYAMyEEuzJnyUi");  //Example app-key taken from https://pushover.net/api#registration
        }
    }
}
