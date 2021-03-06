﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace UnitTestProject1.Features
{
    using TechTalk.SpecFlow;

    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class TreeReorderingFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "TreeReordering.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "TreeReordering", "In order to find all the combinations of data entities\t\r\nI want a tree to be reor" +
                    "dered when a filter between two entities is specified", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((TechTalk.SpecFlow.FeatureContext.Current != null) 
                        && (TechTalk.SpecFlow.FeatureContext.Current.FeatureInfo.Title != "TreeReordering")))
            {
                FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Simple tree reorder")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "TreeReordering")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("treeReordering1")]
        public virtual void SimpleTreeReorder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Simple tree reorder", new string[] {
                        "treeReordering1"});
#line 6
this.ScenarioSetup(scenarioInfo);
#line 7
 testRunner.Given("insurance B is taken from insurancesSource", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
   testRunner.And("insurance B is created in year 2012", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 9
   testRunner.And("for insurance B exists an assignment B", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
   testRunner.And("assignment B has type \'Dependent\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
   testRunner.And("for insurance B exists a tax B", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
   testRunner.And("tax B has type \'Federal\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
   testRunner.And("assignment B is created at the same day as tax B", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.Then("insurance B name must contain \'@treeReordering1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 15
   testRunner.And("assignment B Id must be 12", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Two level tree reorder")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "TreeReordering")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("treeReordering2")]
        public virtual void TwoLevelTreeReorder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Two level tree reorder", new string[] {
                        "treeReordering2"});
#line 18
this.ScenarioSetup(scenarioInfo);
#line 19
 testRunner.Given("insurance B is taken from insurancesSource", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 20
   testRunner.And("insurance B is created in year 2012", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
   testRunner.And("for insurance B exists an assignment B", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
   testRunner.And("assignment B has type \'Dependent\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
   testRunner.And("insurance C is taken from insurancesSource", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
   testRunner.And("insurance C is created in year 2013", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
   testRunner.And("for insurance C exists an assignment C", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 26
   testRunner.And("assignment C has type \'Employee\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
   testRunner.And("assignment B is created at the same day as assignment C", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
 testRunner.Then("insurance B name must contain \'@treeReordering1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 29
   testRunner.And("insurance C name must contain \'@treeReordering2\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
   testRunner.And("assignment B Id must be 12", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
   testRunner.And("assignment C Id must be 15", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Two level tree reorder complexResolve")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "TreeReordering")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("treeReordering3")]
        public virtual void TwoLevelTreeReorderComplexResolve()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Two level tree reorder complexResolve", new string[] {
                        "treeReordering3"});
#line 34
this.ScenarioSetup(scenarioInfo);
#line 35
 testRunner.Given("insurance B is taken from insurancesSource", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 36
   testRunner.And("insurance B is created in year 2013", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 37
   testRunner.And("for insurance B exists an assignment B", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
   testRunner.And("assignment B has type \'Dependent\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
   testRunner.And("insurance C is taken from insurancesSource", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
   testRunner.And("insurance C is created in year 2014", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
   testRunner.And("for insurance C exists an assignment C", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
   testRunner.And("assignment C has type \'Employee\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
   testRunner.And("assignment B is created at the same day as assignment C", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.Then("insurance B name must contain \'@treeReordering3 first\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 45
   testRunner.And("insurance C name must contain \'@treeReordering3 second\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
   testRunner.And("for assignment C with id 24 there must be provided assignments B with ids 21,22", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 47
   testRunner.And("for assignment B with id 20 there must be provided assignments C with ids 23,25", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Simple tree reorder with collection filter")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "TreeReordering")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("treeReordering4")]
        public virtual void SimpleTreeReorderWithCollectionFilter()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Simple tree reorder with collection filter", new string[] {
                        "treeReordering4"});
#line 50
this.ScenarioSetup(scenarioInfo);
#line 51
 testRunner.Given("insurance B is taken from insurancesSource", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 52
   testRunner.And("insurance B is created in year 2012", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 53
   testRunner.And("for insurance B exists an assignment B", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
   testRunner.And("assignment B has type \'Employee\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
   testRunner.And("for insurance B exists a tax B", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
   testRunner.And("tax B has type \'Federal\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
   testRunner.And("assignment B is created at the same day as tax B", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
   testRunner.And("assignments B cover 100 people total", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
   testRunner.And("taxes B have total amount of 100$", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
 testRunner.Then("insurance B name must contain \'@treeReordering4\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Both singular and collection filters are assigned between branches")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "TreeReordering")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("treeReordering5")]
        public virtual void BothSingularAndCollectionFiltersAreAssignedBetweenBranches()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Both singular and collection filters are assigned between branches", new string[] {
                        "treeReordering5"});
#line 63
this.ScenarioSetup(scenarioInfo);
#line 64
 testRunner.Given("insurance B is taken from insurancesSource", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 65
   testRunner.And("insurance B is created in year 2015", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
   testRunner.And("for insurance B exists an assignment B", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
   testRunner.And("assignment B has type \'Dependent\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 68
   testRunner.And("for insurance B exists a tax B", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 69
   testRunner.And("tax B has type \'Federal\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 70
   testRunner.And("assignment B is created at the same day as tax B", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 71
   testRunner.And("average payment per person in assignments B, specified in taxes B is over 10$", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
 testRunner.Then("insurance B name must contain \'@treeReordering5\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
